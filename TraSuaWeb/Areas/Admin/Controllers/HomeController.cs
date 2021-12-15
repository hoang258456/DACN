using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bantrasua.Areas.Admin.Controllers
{
    //[Route("Admin/Home/index")]
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "AdministratorAuth")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           // var a = User.Identity.Name;
            return View();
        }
    }
}
