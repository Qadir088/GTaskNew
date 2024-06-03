using Microsoft.AspNetCore.Mvc;

namespace BigOnApp.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
