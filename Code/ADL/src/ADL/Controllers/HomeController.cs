using Microsoft.AspNetCore.Mvc;
using System;

namespace ADL.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Time()
        {
            return View(DateTime.Now);
        }
    }
}