using Microsoft.AspNetCore.Mvc;

namespace StorkEngineApi.Controllers
{
    public class UCI : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
