using Business_Logic.DTO;
using Business_Logic.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bulletinboard.Controllers;

[Authorize(Roles = "Administrator,Moderator")]
public class AdminController : Controller
{
    private IBulletinService BulletinService { get; }
    private IDataService<Category, CategoryDTO> CategoriesService { get; }

    public AdminController(
        IBulletinService BulletinService, 
        IDataService<Category, CategoryDTO> CategoriesService
        )
    {
        this.BulletinService = BulletinService;
        this.CategoriesService = CategoriesService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(BulletinService.GetAll());
    }

    public IActionResult Categories()
    {
        return View(CategoriesService.GetAll());
    }

    public IActionResult CreateCategory()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(CategoryDTO category)
    {
        // Validation
        if (ModelState.IsValid) CategoriesService.Add(category);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        CategoriesService.Remove(id);
        return RedirectToAction("Index");
    }
}