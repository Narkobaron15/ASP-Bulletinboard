using Business_Logic.DTO;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Business_Logic.Interfaces;

public interface IBulletinService : IReadDataService<Bulletin, BulletinDTO>
{
    Task Add(BulletinDTO dto, ClaimsPrincipal? owner, IFormFileCollection? pictures = null);
    Task Update(BulletinDTO dto, ClaimsPrincipal? owner, IFormFileCollection? pictures = null);
    Task Remove(BulletinDTO dto, ClaimsPrincipal? owner);
    Task Remove(int bulletinId, ClaimsPrincipal? owner);

    bool IsOwner(BulletinDTO? dto, ClaimsPrincipal? user);
    bool IsOwner(int bulletinId, ClaimsPrincipal? user);

    User? GetOwner(BulletinDTO bulletin);
    User? GetOwner(int bulletinId);

    Task<User?> GetOwnerAsync(BulletinDTO bulletin);
    Task<User?> GetOwnerAsync(int bulletinId);

    IEnumerable<BulletinDTO?> GetByOwner(ClaimsPrincipal? owner);
    Task<IEnumerable<BulletinDTO?>> GetByOwnerAsync(ClaimsPrincipal? owner);
}
