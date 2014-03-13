using System.Web.Mvc;

namespace CityPlace.Web.Controllers
{
    /// <summary>
    /// Контроллер отображения сводки
    /// </summary>
    public class DashboardController : BaseController
    {
        /// <summary>
        /// Главная страница системы, которую видят все пользователи
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PushNavigationItem("Сводка","/Dashboard");

            return View();
        }

    }
}
