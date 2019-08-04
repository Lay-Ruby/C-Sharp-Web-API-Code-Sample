using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dispatcherservice.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.Title = "C# Web API Code Sample";

            return View();
        }
    }
}
