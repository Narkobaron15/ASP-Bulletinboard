using Business_Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bulletinboard.Controllers;

public class FavoritesController : Controller
{
    private IFavoritesService FavoritesService { get; }

    public FavoritesController(IFavoritesService FavoritesService)
    {
        this.FavoritesService = FavoritesService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await FavoritesService.GetFavoritesAsync(User));
    }

    public async Task<IActionResult> Favorite(int id, string? returnUrl = null)
    {
        await FavoritesService.ChangeFavStatus(id, User);
        return Redirect(returnUrl ?? "/");
    }
}
