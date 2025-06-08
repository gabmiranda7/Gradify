using System.Diagnostics;
using Gradify.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gradify.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
