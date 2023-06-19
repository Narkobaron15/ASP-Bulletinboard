using Bulletinboard.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace Bulletinboard.Controllers;

// Controller for handling errors
public class ErrorController : Controller
{
    public IActionResult Index()
    {
        string requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

        // logging
        // ...

        var handler = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var error = handler!.Error;
        ErrorViewModel? vm = null;

        if (error is HttpRequestException httpEx)
        {
            Response.StatusCode = (int?)httpEx.StatusCode ?? 400;
            vm = ErrorViewModel.GetByStatusCode(httpEx.StatusCode);
        }
        else if (error is UnauthorizedAccessException)
        {
            Response.StatusCode = 401;
            vm = ErrorViewModel.Unauthorized;
        }
        else if (error is KeyNotFoundException)
        {
            Response.StatusCode = 404;
            vm = ErrorViewModel.NotFound;
        }
        // else
        Response.StatusCode = 500;
        vm ??= new ErrorViewModel($"{error.GetType().Name}: {error.Message}", HttpStatusCode.InternalServerError, requestId);

        return View(vm);
    }
}
