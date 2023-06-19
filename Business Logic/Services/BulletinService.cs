using AutoMapper;
using Business_Logic.DTO;
using Business_Logic.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repository.Interfaces;
using System.Data;
using System.Security.Claims;

namespace Business_Logic.Services;

public class BulletinService : BasicDataService<Bulletin, BulletinDTO>, IBulletinService
{
    private UserManager<User> UManager { get; }
    private IFileService PictureService { get; }

    public BulletinService(
        IGenericUnitOfWork genericUnitOfWork,
        IMapper mapper,
        UserManager<User> UManager,
        IFileService PictureService
        ) 
        : base(genericUnitOfWork, mapper) 
    {
        this.UManager = UManager;
        this.PictureService = PictureService;
    }

    // Miscellaneous
    private static bool IsAdmin(ClaimsPrincipal? User)
        => User?.IsInRole("Administrator") == true ||
           User?.IsInRole("Moderator") == true;
    private async Task<User?> GetUser(ClaimsPrincipal? user)
        => user == null ? null : await UManager.GetUserAsync(user);
    private async Task AddPicsToBulletin(BulletinDTO dto, IFormFileCollection? pictures = null)
    {
        if (pictures != null)
        {
            var pic_dtos = await PictureService.UploadFiles(pictures, dto.Id);
            dto.Pictures = pic_dtos.ToList();
        }
    }

    // Read
    public override BulletinDTO? GetById(int id)
        => GetAllAsync(b => b.Pictures!).Result.FirstOrDefault(b => b.Id == id);

    // Create
    public async Task Add(BulletinDTO dto, ClaimsPrincipal? owner, IFormFileCollection? pictures = null)
    {
        // get owner
        var user = await GetUser(owner) ?? throw new UnauthorizedAccessException();
        // create an entity
        var bulletin = Mapper.Map<Bulletin>(dto);
        // assign the entity ownership
        bulletin.OwnerId = user.Id;
        // add to db
        await Repo.AddAsync(bulletin);
        // assign id for the dto for proper future use
        dto.Id = bulletin.Id;
        // add pictures
        await AddPicsToBulletin(dto, pictures);
    }

    // Update
    public async Task Update(BulletinDTO dto, ClaimsPrincipal? owner, IFormFileCollection? pictures = null)
    {
        // get owner
        var user = await GetUser(owner) ?? throw new UnauthorizedAccessException();
        // create an entity
        var bulletin = Repo.FindById(dto.Id) ?? throw new KeyNotFoundException("This object should be added, not updated.");
        // map it
        Mapper.Map(dto, bulletin);
        // check the ownership
        if (bulletin.OwnerId != user.Id && !IsAdmin(owner))
            throw new UnauthorizedAccessException();
        // add pictures
        await AddPicsToBulletin(dto, pictures);
        // update
        await Repo.UpdateAsync(bulletin);
    }

    // Delete
    public async Task Remove(BulletinDTO dto, ClaimsPrincipal? owner)
        => await Remove(dto.Id, owner);

    // Delete
    public async Task Remove(int bulletinId, ClaimsPrincipal? owner)
    {
        // get owner
        var user = await GetUser(owner) ?? throw new UnauthorizedAccessException();
        // get the entity
        var bulletin = await Repo.FindByIdAsync(bulletinId) ?? throw new KeyNotFoundException();
        // check the ownership
        if (bulletin.OwnerId != user.Id && !IsAdmin(owner))
            throw new UnauthorizedAccessException();
        // remove
        await Repo.RemoveAsync(bulletin);
    }

    // Ownership checks for bulletins
    public User? GetOwner(BulletinDTO bulletin)
        => GetOwnerAsync(bulletin.Id).Result;
    public User? GetOwner(int bulletinId)
        => GetOwnerAsync(bulletinId).Result;
    public async Task<User?> GetOwnerAsync(BulletinDTO bulletin)
        => await GetOwnerAsync(bulletin.Id);
    public async Task<User?> GetOwnerAsync(int bulletinId)
    {
        var user = (await Repo.GetWithInclude(x => x.Owner!))
                       .FirstOrDefault(x => x.Id == bulletinId)?
                       .Owner;
        return user;
    }
    public IEnumerable<BulletinDTO?> GetByOwner(ClaimsPrincipal? owner)
        => GetByOwnerAsync(owner).Result;
    public async Task<IEnumerable<BulletinDTO?>> GetByOwnerAsync(ClaimsPrincipal? owner)
    {
        var ownerId = GetUser(owner)?.Result?.Id;
        var allItems = await Repo.GetWithInclude(x => x.Pictures!);
        var ownedItems = allItems.Where(x => x.OwnerId == ownerId);

        return ToDTOS(ownedItems);
    }

    public bool IsOwner(BulletinDTO? dto, ClaimsPrincipal? user) => IsOwner(dto?.Id ?? 0, user);
    public bool IsOwner(int bulletinId, ClaimsPrincipal? user)
    {
        var owner = GetUser(user).Result;
        var bulletin = Repo.FindById(bulletinId);

        if (bulletin == null || owner == null)
            return false;

        return bulletin.OwnerId == owner.Id;
    }
}
