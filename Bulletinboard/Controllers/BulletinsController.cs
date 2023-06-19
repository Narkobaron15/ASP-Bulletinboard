using Bulletinboard.Helpers;
using Business_Logic.DTO;
using Business_Logic.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bulletinboard.Controllers;

[Authorize]
public class BulletinsController : Controller
{
    private IBulletinService BulletinService { get; }
    private IReadDataService<Category, CategoryDTO> CategoryService { get; }
    private IReadDataService<City, CityDTO> CityService { get; }
    private IFileService PictureService { get; }

    public BulletinsController(
        IBulletinService BulletinService,
        IDataService<Category, CategoryDTO> CategoryService,
        IDataService<City, CityDTO> CityService,
        IFileService PictureService
        )
    {
        this.BulletinService = BulletinService;
        this.CategoryService = CategoryService;
        this.CityService = CityService;
        this.PictureService = PictureService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Controller = "Bulletins";
        return View(await BulletinService.GetByOwnerAsync(User));
    }

    // Add properties to the ViewBag dictionary
    // when performing Create or Update actions
    private void LoadProperties()
    {
        ViewBag.Categories = new SelectList(CategoryService.GetAll(), "Id", "Name");
        ViewBag.Cities = new SelectList(CityService.GetAll(), "Id", "Name");
        ViewBag.Types = new SelectList(Enum.GetValues<BulletinType>());
    }

    public IActionResult Create()
    {
        LoadProperties();
        return View(new BulletinDTO());
    }

    public IActionResult Update(int id)
    {
        var bulletin = BulletinService.GetById(id);

        if (!BulletinService.IsOwner(bulletin, User) 
            && !User.IsInRole(Roles.Administrator.ToString()) 
            && !User.IsInRole(Roles.Moderator.ToString()))
            return Forbid();

        LoadProperties();
        return View(bulletin);
    }

    [HttpPost] public async Task<IActionResult> Create(BulletinDTO dto)
    {
        // Validation
        if (!ModelState.IsValid)
        {
            LoadProperties();
            return View("Create");
        }

        await BulletinService.Add(dto, User, Request.Form.Files); // Adding to database
        return RedirectToAction("Details", new { dto.Id });
    }

    [HttpPost] public async Task<IActionResult> Update(BulletinDTO dto)
    {
        // Validation
        if (!ModelState.IsValid)
        {
            LoadProperties();
            return View("Update", dto);
        }

        await BulletinService.Update(dto, User, Request.Form.Files); // Updating
        return RedirectToAction("Details", new { dto.Id });
    }

    [AllowAnonymous] public IActionResult Details(int Id, string? returnUrl = null)
    {
        var item = BulletinService.GetById(Id);
        if (item == null)
            return NotFound();

        ViewBag.ReturnUrl = returnUrl ?? "/";
        ViewBag.FavReturnUrl = "/Bulletins/Details/" + Id;

        return View(item);
    }

    [HttpPost] public async Task<IActionResult> Delete(int id)
    {
        await BulletinService.Remove(id, User);
        return RedirectToAction("Index");
    }
}
