using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CityPlace.Domain.Enums;
using CityPlace.Domain.Interfaces.Repositories;
using CityPlace.Domain.IoC;
using CityPlace.Domain.Routing;
using CityPlace.Web.Classes.Security;
using PushSharp;
using PushSharp.Apple;

namespace CityPlace.Web.Controllers
{
	/// <summary>
	/// Контроллер обработки пуш уведомлений
	/// </summary>
    public class ManagePushController : BaseController
    {
		/// <summary>
		/// Отображает страницу рассылки пуш уведомлений
		/// </summary>
		/// <returns></returns>
        [Route("manage/push")][AuthorizationCheck]
        public ActionResult Index()
        {
			PushNavigationItem("Пуш уведомления", "/manage/push");
			PushNavigationItem("Новая рассылка", "#");

            return View();
        }

		/// <summary>
		/// Обрабатывает PUSH рассылку
		/// </summary>
		/// <param name="txt">Текст рассылки</param>
		/// <returns></returns>
		[Route("manage/push/send")][HttpPost]
		public ActionResult Send(string txt)
		{
			// Берем девайся
			var rep = Locator.GetService<IDeviceRepository>();
			var devices = rep.FindAll();

			// менеджер рассылок
			var push = Locator.GetService<PushBroker>();

			var enqued = 0;
			foreach (var device in devices)
			{
				switch ((MobilePlatform)device.Platform)
				{
					case MobilePlatform.iOS:
						push.QueueNotification(new AppleNotification()
						   .ForDeviceToken("DEVICE TOKEN HERE")
						   .WithAlert("Hello World!")
						   .WithBadge(1)
						   .WithSound("sound.caf"));
						enqued++;
						break;
					case MobilePlatform.Android:
						break;
					case MobilePlatform.WP8:
						break;
					case MobilePlatform.Generic:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			ShowSuccess(string.Format("Рассылка для {0} устройств была добавлена в очередь", enqued));

			return RedirectToAction("Index");
		}

    }
}
