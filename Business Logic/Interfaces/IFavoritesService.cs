using Business_Logic.DTO;
using System.Security.Claims;

namespace Business_Logic.Interfaces;

public interface IFavoritesService
{
    public Task ChangeFavStatus(FavoriteDTO favorite);
    public Task ChangeFavStatus(int bulletinId, ClaimsPrincipal? user);

    public Task AddRecord(FavoriteDTO favorite);

    public Task RemoveRecord(int RecordId);
    public Task RemoveRecord(FavoriteDTO favorite);

    public Task ClearRecords(ClaimsPrincipal? user);
    public IEnumerable<BulletinDTO> GetFavorites(ClaimsPrincipal? user);
    public Task<IEnumerable<BulletinDTO>> GetFavoritesAsync(ClaimsPrincipal? user);
    public FavoriteDTO? GetFavorite(BulletinDTO? bulletin, ClaimsPrincipal? user);
    public FavoriteDTO? GetFavorite(int? BulletinId, string? UserId);

    public bool IsInFavs(FavoriteDTO favorite);
    public bool IsInFavs(BulletinDTO bulletin, ClaimsPrincipal user);

    public bool Any(ClaimsPrincipal? user, Func<BulletinDTO, bool> pred)
        => GetFavorites(user).Any(pred);
    public bool Any(ClaimsPrincipal? user)
        => GetFavorites(user).Any();
}
