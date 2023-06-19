using AutoMapper;
using Business_Logic.DTO;
using Business_Logic.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Repository.Interfaces;
using System.Security.Claims;

namespace Business_Logic.Services;

public class FavoritesService : IFavoritesService
{
    public static readonly string CookieKey = "Favorites";

    private ISession Session { get; }
    private IDataService<Favorite, FavoriteDTO> FavDataServiceImpl { get; }
    private IReadDataService<Bulletin, BulletinDTO> BulletinDataService { get; }
    private UserManager<User> UserManager { get; }

    public FavoritesService(
        IGenericUnitOfWork GenericUnitOfWork,
        IMapper Mapper,
        IHttpContextAccessor accessor,
        UserManager<User> UserManager,
        IBulletinService bulletinService
        )
    {
        this.Session = accessor.HttpContext.Session;
        this.BulletinDataService = bulletinService;
        this.UserManager = UserManager;

        this.FavDataServiceImpl = new DataService<Favorite, FavoriteDTO>(GenericUnitOfWork, Mapper);
    }

    private async Task<User?> GetUser(string UID)
        => await UserManager.FindByIdAsync(UID);
    private async Task<User?> GetUser(ClaimsPrincipal? user)
        => user == null ? null : await UserManager.GetUserAsync(user);

    public async Task AddRecord(FavoriteDTO favorite)
    {
        var user = await GetUser(favorite.UserId);

        if (user != null) await FavDataServiceImpl.Add(favorite);
        else
        {
            var favlist = GetCookieFavorites();
            favlist.Add(favorite.BulletinId);
            Session.SetString(CookieKey, JsonConvert.SerializeObject(favlist));
        }
    }

    public async Task RemoveRecord(int BulletinId) => await RemoveRecord(new FavoriteDTO() { BulletinId = BulletinId });
    public async Task RemoveRecord(FavoriteDTO favorite)
    {
        var user = await GetUser(favorite.UserId);

        if (user != null)
            await FavDataServiceImpl.Remove(favorite);
        else
        {
            var favlist = GetCookieFavorites();
            favlist.Remove(favorite.BulletinId);
            Session.SetString(CookieKey, JsonConvert.SerializeObject(favlist));
        }
    }

    public async Task ClearRecords(ClaimsPrincipal? user)
    {
        var userEntity = await GetUser(user);

        if (userEntity != null) 
            await FavDataServiceImpl.RemoveRange(FavDataServiceImpl.GetAll().Where(x => x.UserId == userEntity.Id));
        else Session.Remove(CookieKey);
    }

    public IEnumerable<BulletinDTO> GetFavorites(ClaimsPrincipal? user)
        => GetFavoritesAsync(user).Result;
    public async Task<IEnumerable<BulletinDTO>> GetFavoritesAsync(ClaimsPrincipal? user)
    {
        var userEntity = await GetUser(user);

        if (userEntity != null)
            return (await FavDataServiceImpl.GetAllAsync())
                                 .Where(fav => fav.UserId == userEntity.Id)
                                 .Select(fav => BulletinDataService.GetById(fav.BulletinId))
                                 .Where(b => b != null)!;

        var ids = GetCookieFavorites();
        var buls = ids.Select(BulletinDataService.GetById).Where(b => b != null);

        return buls!;
    }

    public bool IsInFavs(FavoriteDTO favorite)
        => FavDataServiceImpl.Contains(favorite) || GetCookieFavorites().Contains(favorite.BulletinId);

    public bool IsInFavs(BulletinDTO? bulletin, ClaimsPrincipal? user)
    {
        var userEntity = GetUser(user).Result;
        return IsInFavs(new FavoriteDTO() { BulletinId = bulletin!.Id, UserId = userEntity?.Id! });
    }

    private List<int> GetCookieFavorites()
    {
        string cookies = Session.GetString(CookieKey);
        List<int>? favorites = null;

        if (!string.IsNullOrEmpty(cookies))
            favorites = JsonConvert.DeserializeObject<List<int>>(cookies);
        favorites ??= new();

        return favorites;
    }

    public bool Any(ClaimsPrincipal? user, Func<BulletinDTO, bool> pred)
        => GetFavorites(user).Any(pred);
    public bool Any(ClaimsPrincipal? user)
        => GetFavorites(user).Any();

    public async Task ChangeFavStatus(FavoriteDTO favorite)
    {
        if (!BulletinDataService.Contains(favorite.BulletinId))
            throw new KeyNotFoundException(); // NotFound

        if (this.IsInFavs(favorite))
            await this.RemoveRecord(favorite);
        else
            await this.AddRecord(favorite);
    }
    public async Task ChangeFavStatus(int bulletinId, ClaimsPrincipal? user)
    {
        var userEntity = await GetUser(user);
        var favorite = new FavoriteDTO() { BulletinId = bulletinId, UserId = userEntity?.Id! };

        await ChangeFavStatus(favorite);
    }

    public FavoriteDTO? GetFavorite(BulletinDTO? bulletin, ClaimsPrincipal? user)
    {
        var userEntity = GetUser(user).Result;
        return GetFavorite(bulletin?.Id, userEntity?.Id);
    }

    public FavoriteDTO? GetFavorite(int? BulletinId, string? UserId)
    {
        var fav = FavDataServiceImpl.GetAll().FirstOrDefault(x => x.BulletinId == BulletinId && x.UserId == UserId);
        return fav;
    }
}
