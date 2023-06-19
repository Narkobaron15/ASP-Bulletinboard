using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Business_Logic.Interfaces;
using Business_Logic.DTO;

namespace Bulletinboard.Controllers;

public class HomeController : Controller
{
    private IReadDataService<Bulletin, BulletinDTO> BulletinService { get; }

    public HomeController(IBulletinService BulletinService)
    {
        this.BulletinService = BulletinService;
    }
    
    // Home page
    public async Task<IActionResult> Index()
    {
        var items = await BulletinService.GetAllAsync(b => b.Pictures!);
        return View(items);
    }
}
