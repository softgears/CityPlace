using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CityPlace.Domain.Entities;
using CityPlace.Domain.Interfaces.Repositories;
using CityPlace.Domain.IoC;
using CityPlace.Domain.Routing;
using CityPlace.Web.Classes.Security;
using CityPlace.Web.Classes.Utils;

namespace CityPlace.Web.Controllers
{
    /// <summary>
    /// Контроллер управления публикациями
    /// </summary>
    public class ManagePublicationsController : BaseController
    {
        /// <summary>
        /// Репозиторий публикаций
        /// </summary>
        public IPublicationsRepository Repository { get; private set; }

        public ManagePublicationsController()
        {
            Repository = Locator.GetService<IPublicationsRepository>();
        }

        /// <summary>
        /// Отображает страницу со списком всех публикаций, отсортированных по дате публикации
        /// </summary>
        /// <returns></returns>
        [AuthorizationCheck]
        [Route("publications")]
        public ActionResult Index()
        {
	        var cities = CurrentUser.GetAvailableCities().Select(c => c.Id).ToArray();
            var pubs =
                Repository.Search(c => cities.Contains(c.CityId)).OrderByDescending(p => p.PublicationDate).ThenByDescending(p => p.DateModified)
                    .ThenByDescending(p => p.DateCreated)
                    .ToList();

            PushNavigationItem("Панель управления", "/dashboard");
            PushNavigationItem("Публикации", "/publications");
            PushNavigationItem("Список публикаций", "#");

            return View(pubs);
        }

        /// <summary>
        /// Отображает страницу добавления новой публикации
        /// </summary>
        /// <returns></returns>
        [AuthorizationCheck]
        [Route("publications/add")]
        public ActionResult Add()
        {
            PushNavigationItem("Панель управления", "/dashboard");
            PushNavigationItem("Публикации", "/publications");
            PushNavigationItem("Новая публикация", "#");

            return View("Edit", new Publication()
            {
                PublicationDate = DateTime.Now
            });
        }

        /// <summary>
        /// Отображает форму редактирования указанной публикации
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorizationCheck]
        [Route("publications/{id}/edit")]
        public ActionResult Edit(long id)
        {
            // Ищем
            var publication = Repository.Load(id);
            if (publication == null)
            {
                ShowError("Такая публикация не найдена");
                return RedirectToAction("Index");
            }

            PushNavigationItem("Панель управления", "/dashboard");
            PushNavigationItem("Публикации", "/publications");
            PushNavigationItem("Редактирование публикации", "#");

            return View(publication);
        }

        /// <summary>
        /// Обрабатывает сохранение данных публикации или создание новой
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AuthorizationCheck]
        [HttpPost]
        [ValidateInput(false)]
        [Route("publications/save")]
        public ActionResult Save(Publication model)
        {
            var file = Request.Files["Image"];
            string imageUrl = null;
            if (file != null && file.ContentLength > 0 && file.ContentType.ToLower().Contains("image"))
            {
                var fileName = Path.ChangeExtension(Path.GetRandomFileName(), ".jpg");
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Publications");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fullPath = Path.Combine(path, fileName);
                ImageHelper.SaveAs(file, fullPath);
                imageUrl = "/Files/Publications/" + fileName;
            }

            if (model.Id <= 0)
            {
                model.DateCreated = DateTime.Now;
                model.Image = imageUrl;
                Repository.Add(model);
                Repository.SubmitChanges();
                ShowSuccess("Публикация успешно добавлена");
            }
            else
            {
                // Ищем
                var publication = Repository.Load(model.Id);
                if (publication == null)
                {
                    ShowError("Такая публикация не найдена");
                    return RedirectToAction("Index");
                }

                // Пытаемся обновить
                var oldImage = publication.Image;
                TryUpdateModel(publication);
                publication.DateModified = DateTime.Now;
                publication.Image = imageUrl ?? oldImage;
	            if (publication.City.Id != model.CityId)
	            {
		            publication.City.Publications.Remove(publication);
					Locator.GetService<ICitiesRepository>().Load(model.CityId).Publications.Add(publication);
	            }
                Repository.SubmitChanges();

                ShowSuccess("Публикация успешно отредактирована");
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Обрабатывает удаление указанной публикации
        /// </summary>
        /// <param name="id">Идентификатор публикации</param>
        /// <returns></returns>
        [AuthorizationCheck]
        [Route("publications/{id}/delete")]
        public ActionResult Delete(long id)
        {
            // Ищем
            var cat = Repository.Load(id);
            if (cat == null)
            {
                ShowError("Такая публикация не найдена");
                return RedirectToAction("Index");
            }

            Repository.Delete(cat);
            Repository.SubmitChanges();

            ShowSuccess("Публикация успешно удалена");

            return RedirectToAction("Index");
        }
    }
}
