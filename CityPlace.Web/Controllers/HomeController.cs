using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityPlace.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
		/// <summary>
		/// Отображает страницу заглушку
		/// </summary>
		/// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}
