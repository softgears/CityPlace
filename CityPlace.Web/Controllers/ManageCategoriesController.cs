using System;
using System.Collections.Generic;
using System.Data.Linq;
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
    /// Контроллер управления категориями
    /// </summary>
    public class ManageCategoriesController : BaseController
    {
        /// <summary>
        /// Репозиторий категорий
        /// </summary>
        public ICategoriesRepository Repository { get; private set; }

        public ManageCategoriesController()
        {
            Repository = Locator.GetService<ICategoriesRepository>();
        }

        /// <summary>
        /// Отображает страницу со списком категорий, отсортированных по дате изменения
        /// </summary>
        /// <returns></returns>
        [AuthorizationCheck][Route("categories")]
        public ActionResult Index()
        {
            var cats =
                Repository.FindAll().OrderByDescending(p => p.DateModified)
                    .ThenByDescending(p => p.DateCreated)
                    .ToList();

            PushNavigationItem("Панель управления", "/ControlPanel");
            PushNavigationItem("Категории", "/ControlPanel/ManageCategories");
            PushNavigationItem("Список категорий", "#");

            return View(cats);
        }

        /// <summary>
        /// Отображает страницу добавления новой публикации
        /// </summary>
        /// <returns></returns>
        [AuthorizationCheck][Route("categories/add")]
        public ActionResult Add()
        {
            PushNavigationItem("Панель управления", "/ControlPanel");
            PushNavigationItem("Категории", "/ControlPanel/ManageCategories");
            PushNavigationItem("Новая категория", "#");

            return View("Edit", new Category()
            {
                
            });
        }

        /// <summary>
        /// Отображает форму редактирования указанной публикации
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorizationCheck][Route("categories/{id}/edit")]
        public ActionResult Edit(long id)
        {
            // Ищем
            var cat = Repository.Load(id);
            if (cat == null)
            {
                ShowError("Такая категория не найдена");
                return RedirectToAction("Index");
            }

            PushNavigationItem("Панель управления", "/ControlPanel");
            PushNavigationItem("Категории", "/ControlPanel/ManageCategories");
            PushNavigationItem("Редактирование категории", "#");

            return View(cat);
        }

        /// <summary>
        /// Обрабатывает сохранение данных публикации или создание новой
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AuthorizationCheck]
        [HttpPost]
        [ValidateInput(false)][Route("categories/save")]
        public ActionResult Save(Category model)
        {
            var file = Request.Files["Image"];
            string imageUrl = null;
            if (file != null && file.ContentLength > 0 && file.ContentType.ToLower().Contains("image"))
            {
                var fileName = Path.ChangeExtension(Path.GetRandomFileName(), ".jpg");
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Categories");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fullPath = Path.Combine(path, fileName);
                ImageHelper.SaveAs(file, fullPath);
                imageUrl = "/Files/Categories/" + fileName;
            }

            if (model.Id <= 0)
            {
                model.DateCreated = DateTime.Now;
                model.Image = imageUrl;
                Repository.Add(model);
                Repository.SubmitChanges();
                ShowSuccess("Категория успешно добавлена");
            }
            else
            {
                // Ищем
                var cat = Repository.Load(model.Id);
                if (cat == null)
                {
                    ShowError("Такая категория не найдена");
                    return RedirectToAction("Index");
                }

                // Пытаемся обновить
                var oldImage = cat.Image;
                TryUpdateModel(cat);
                cat.DateModified = DateTime.Now;
                cat.Image = imageUrl ?? oldImage;
                Repository.SubmitChanges();

                ShowSuccess("Категория успешно отредактирована");
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Обрабатывает удаление указанной публикации
        /// </summary>
        /// <param name="id">Идентификатор публикации</param>
        /// <returns></returns>
        [AuthorizationCheck][Route("categories/{id}/delete")]
        public ActionResult Delete(long id)
        {
            // Ищем
            var cat = Repository.Load(id);
            if (cat == null)
            {
                ShowError("Такая категория не найдена");
                return RedirectToAction("Index");
            }

            Repository.Delete(cat);
            Repository.SubmitChanges();

            ShowSuccess("Категория успешно удалена");

            return RedirectToAction("Index");
        }

    }
}
