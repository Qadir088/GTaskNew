using Microsoft.AspNetCore.Mvc;

namespace BigOnApp.Controllers;

public class ShopController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
