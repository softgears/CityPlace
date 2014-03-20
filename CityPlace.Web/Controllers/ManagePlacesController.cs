using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CityPlace.Domain.Entities;
using CityPlace.Domain.Interfaces.Repositories;
using CityPlace.Domain.IoC;
using CityPlace.Domain.Routing;
using CityPlace.Web.Classes.Ext;
using CityPlace.Web.Classes.Security;
using CityPlace.Web.Classes.Utils;

namespace CityPlace.Web.Controllers
{
    /// <summary>
    /// Контроллер управления заведениями
    /// </summary>
    public class ManagePlacesController : BaseController
    {
        /// <summary>
        /// Репозиторий заведений
        /// </summary>
        public IPlacesRepository Repository { get; private set; }

        /// <summary>
        /// Репозиторий событий
        /// </summary>
        public IEventsRepository EventsRepository { get; private set; }

        /// <summary>
        /// Репозиторий товаров
        /// </summary>
        public IProductsRepository ProductsRepository { get; private set; }

        public ManagePlacesController()
        {
            Repository = Locator.GetService<IPlacesRepository>();
            EventsRepository = Locator.GetService<IEventsRepository>();
            ProductsRepository = Locator.GetService<IProductsRepository>();
        }

        #region Заведения

        /// <summary>
        /// Отображает страницу со списком заведени, отсортированных по алфавиту
        /// </summary>
        /// <returns></returns>
        [AuthorizationCheck][Route("places")]
        public ActionResult Index()
        {
            var places =
                Repository.FindAll().OrderBy(p => p.Title).ThenByDescending(p => p.DateModified)
                    .ThenByDescending(p => p.DateCreated)
                    .ToList();

            PushNavigationItem("Панель управления", "/dashboard");
            PushNavigationItem("Заведения", "/places");
            PushNavigationItem("Список заведений", "#");

            return View(places);
        }

        /// <summary>
        /// Отображает страницу добавления нового заведения
        /// </summary>
        /// <returns></returns>
        [AuthorizationCheck][Route("places/add")]
        public ActionResult Add()
        {
            PushNavigationItem("Панель управления", "/dashboard");
            PushNavigationItem("Заведения", "/places");
            PushNavigationItem("Новое заведение", "#");

            return View("Edit", new Place()
            {
                
            });
        }

        /// <summary>
        /// Отображает форму редактирования указанного заведения
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorizationCheck][Route("places/{id}/edit")]
        public ActionResult Edit(long id)
        {
            // Ищем
            var place = Repository.Load(id);
            if (place == null)
            {
                ShowError("Такое заведение не найдено");
                return RedirectToAction("Index");
            }

            PushNavigationItem("Панель управления", "/dashboard");
            PushNavigationItem("Заведения", "/places");
            PushNavigationItem("Редактирование заведения", "#");

            return View(place);
        }

        /// <summary>
        /// Обрабатывает сохранение данных заведения или создание нового
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AuthorizationCheck]
        [HttpPost]
        [ValidateInput(false)][Route("places/save")]
        public ActionResult Save(Place model)
        {
            var file = Request.Files["Image"];
            string imageUrl = null;
            if (file != null && file.ContentLength > 0 && file.ContentType.ToLower().Contains("image"))
            {
                var fileName = Path.ChangeExtension(Path.GetRandomFileName(), ".jpg");
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Places");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fullPath = Path.Combine(path, fileName);
                ImageHelper.SaveAs(file, fullPath);
                imageUrl = "/Files/Places/" + fileName;
            }

            if (model.Id <= 0)
            {
                model.DateCreated = DateTime.Now;
                model.Image = imageUrl;
                Repository.Add(model);
                Repository.SubmitChanges();
                ShowSuccess("Заведение успешно добавлено");
            }
            else
            {
                // Ищем
                var place = Repository.Load(model.Id);
                if (place == null)
                {
                    ShowError("Такое заведение не найдено");
                    return RedirectToAction("Index");
                }

                // Пытаемся обновить
                var oldImage = place.Image;
                var oldCityId = place.CityId;
                var oldCategoryId = place.CategoryId;
                TryUpdateModel(place);
                if (oldCityId != place.CityId)
                {
                    // TODO: нужно реализовать репозиторий категорий
                }
                if (oldCategoryId != place.CategoryId)
                {
                    var newCategory = Locator.GetService<ICategoriesRepository>().Load(place.CategoryId);
                    place.Category.Places.Remove(place);
                    if (newCategory != null)
                    {
                        newCategory.Places.Add(place);
                    }
                }
                place.DateModified = DateTime.Now;
                place.Image = imageUrl ?? oldImage;
                Repository.SubmitChanges();

                ShowSuccess("Заведение успешно отредактировано");
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Обрабатывает удаление указанного заведения
        /// </summary>
        /// <param name="id">Идентификатор публикации</param>
        /// <returns></returns>
        [AuthorizationCheck][Route("places/{id}/delete")]
        public ActionResult Delete(long id)
        {
            // Ищем
            var place = Repository.Load(id);
            if (place == null)
            {
                ShowError("Такое заведение не найдено");
                return RedirectToAction("Index");
            }

            Repository.Delete(place);
            Repository.SubmitChanges();

            ShowSuccess("Заведение успешно удалено");

            return RedirectToAction("Index");
        }

        #endregion

        #region События

        /// <summary>
        /// Отображает список событий в данном заведении, отсортированный по дате убывания
        /// </summary>
        /// <param name="id">Идентификатор заведения</param>
        /// <returns></returns>
        [AuthorizationCheck]
        [Route("places/{id}/events")]
        public ActionResult Events(long id)
        {
            // Ищем
            var place = Repository.Load(id);
            if (place == null)
            {
                ShowError("Такое заведение не найдено");
                return RedirectToAction("Index");
            }

            PushNavigationItem("Панель управления", "/dashboard");
            PushNavigationItem("Заведения", "/places");
            PushNavigationItem(place.Title, string.Format("/places/{0}/edit", place.Id));
            PushNavigationItem("События", "#");

            ViewBag.place = place;

            return View(place.Events.OrderByDescending(e => e.StartDateTime).ToList());
        }

        /// <summary>
        /// Отображает страницу добавления нового события
        /// </summary>
        /// <returns></returns>
        [AuthorizationCheck]
        [Route("places/events/add/{id}")]
        public ActionResult AddEvent(long id)
        {
            PushNavigationItem("Панель управления", "/dashboard");
            PushNavigationItem("Заведения", "/places");
            PushNavigationItem("Новое событие", "#");

            return View("EditEvent", new Event()
            {
                Id = 0,
                PlaceId = id,
                StartDateTime = DateTime.Now.Date.AddHours(22),
                EndDateTime = DateTime.Now.Date.AddHours(22).AddHours(8)
            });
        }

        /// <summary>
        /// Отображает форму редактирования указанного события
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorizationCheck]
        [Route("places/events/{id}/edit")]
        public ActionResult EditEvent(long id)
        {
            // Ищем
            var @event = EventsRepository.Load(id);
            if (@event == null)
            {
                ShowError("Такое событие не найдено");
                return RedirectToAction("Index");
            }

            PushNavigationItem("Панель управления", "/dashboard");
            PushNavigationItem("Заведения", "/places");
            PushNavigationItem(@event.Place.Title, string.Format("/places/{0}/edit", @event.Place.Id));
            PushNavigationItem("Редактирование события", "#");

            return View(@event);
        }

        /// <summary>
        /// Обрабатывает сохранение данных события или создание нового события
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AuthorizationCheck]
        [HttpPost]
        [ValidateInput(false)]
        [Route("places/events/save")]
        public ActionResult SaveEvent(Event model,string startTime, string endTime)
        {
            var file = Request.Files["Image"];
            string imageUrl = null;
            if (file != null && file.ContentLength > 0 && file.ContentType.ToLower().Contains("image"))
            {
                var fileName = Path.ChangeExtension(Path.GetRandomFileName(), ".jpg");
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Events");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fullPath = Path.Combine(path, fileName);
                ImageHelper.SaveAs(file, fullPath);
                imageUrl = "/Files/Events/" + fileName;
            }

            // Устанавливаем время событий
            //DateUtils.SetTime(model.StartDateTime, startTime);
            //DateUtils.SetTime(model.EndDateTime, endTime);

            if (model.Id <= 0)
            {
                model.DateCreated = DateTime.Now;
                model.Image = imageUrl;
                EventsRepository.Add(model);
                EventsRepository.SubmitChanges();
                ShowSuccess("Событие успешно добавлено");
            }
            else
            {
                // Ищем
                var @event = EventsRepository.Load(model.Id);
                if (@event == null)
                {
                    ShowError("Такое заведение не найдено");
                    return RedirectToAction("Index");
                }

                // Пытаемся обновить
                var oldImage = @event.Image;
                TryUpdateModel(@event);
                @event.StartDateTime = model.StartDateTime;
                @event.EndDateTime = model.EndDateTime;
                @event.DateModified = DateTime.Now;
                @event.Image = imageUrl ?? oldImage;
                Repository.SubmitChanges();

                ShowSuccess("Событие успешно отредактировано");
            }

            return RedirectToAction("Events",new {id = model.PlaceId});
        }

        /// <summary>
        /// Обрабатывает удаление указанного события
        /// </summary>
        /// <param name="id">Идентификатор события</param>
        /// <returns></returns>
        [AuthorizationCheck]
        [Route("places/events/{id}/delete")]
        public ActionResult DeleteEvents(long id)
        {
            // Ищем
            var @event = EventsRepository.Load(id);
            if (@event == null)
            {
                ShowError("Такое событие не найдено");
                return RedirectToAction("Index");
            }

            var pid = @event.PlaceId;
            EventsRepository.Delete(@event);
            EventsRepository.SubmitChanges();

            ShowSuccess("Событие успешно удалено");

            return RedirectToAction("Events",new {id = pid});
        }

        #endregion

        #region Товары и услуги

        /// <summary>
        /// Отображает список товаров и услуг и элементов меню в данном заведении
        /// </summary>
        /// <param name="id">Идентификатор заведения</param>
        /// <returns></returns>
        [AuthorizationCheck]
        [Route("places/{id}/products")]
        public ActionResult Products(long id)
        {
            // Ищем
            var place = Repository.Load(id);
            if (place == null)
            {
                ShowError("Такое заведение не найдено");
                return RedirectToAction("Index");
            }

            PushNavigationItem("Панель управления", "/dashboard");
            PushNavigationItem("Заведения", "/places");
            PushNavigationItem(place.Title, string.Format("/places/{0}/edit", place.Id));
            PushNavigationItem("Товары, услуги, меню", "#");

            ViewBag.place = place;

            return View(place.Products.OrderBy(p => p.Title).ToList());
        }

        /// <summary>
        /// Отображает страницу добавления нового товара или услуги
        /// </summary>
        /// <returns></returns>
        [AuthorizationCheck]
        [Route("places/products/add/{id}")]
        public ActionResult AddProduct(long id, string category = null, short productType = 0)
        {
            PushNavigationItem("Панель управления", "/dashboard");
            PushNavigationItem("Заведения", "/places");
            PushNavigationItem("Новый товар, услуга, элемент меню", "#");

            return View("EditProduct", new Product()
            {
                Id = 0,
                PlaceId = id,
                Category = category,
                Type = productType
            });
        }

        /// <summary>
        /// Отображает форму редактирования указанного товара, услуги, пункта меню
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorizationCheck]
        [Route("places/products/{id}/edit")]
        public ActionResult EditProduct(long id)
        {
            // Ищем
            var product = ProductsRepository.Load(id);
            if (product == null)
            {
                ShowError("Такой товар не найден");
                return RedirectToAction("Index");
            }

            PushNavigationItem("Панель управления", "/dashboard");
            PushNavigationItem("Заведения", "/places");
            PushNavigationItem(product.Place.Title, string.Format("/places/{0}/edit", product.Place.Id));
            PushNavigationItem("Редактирование товара или услуги", "#");

            return View(product);
        }

        /// <summary>
        /// Обрабатывает сохранение данных товара или услуги, или создание нового товара или услуги
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AuthorizationCheck]
        [HttpPost]
        [ValidateInput(false)]
        [Route("places/products/save")]
        public ActionResult SaveProduct(Product model, string nextAction = null)
        {
            var file = Request.Files["Image"];
            string imageUrl = null;
            if (file != null && file.ContentLength > 0 && file.ContentType.ToLower().Contains("image"))
            {
                var fileName = Path.ChangeExtension(Path.GetRandomFileName(), ".jpg");
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Products");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fullPath = Path.Combine(path, fileName);
                ImageHelper.SaveAs(file, fullPath);
                imageUrl = "/Files/Products/" + fileName;
            }

            if (model.Id <= 0)
            {
                model.DateCreated = DateTime.Now;
                model.Image = imageUrl;
                ProductsRepository.Add(model);
                ProductsRepository.SubmitChanges();
                ShowSuccess("Продукт или товар успешно добавлен");
            }
            else
            {
                // Ищем
                var product = ProductsRepository.Load(model.Id);
                if (product == null)
                {
                    ShowError("Такой продукт не найден");
                    return RedirectToAction("Index");
                }

                // Пытаемся обновить
                var oldImage = product.Image;
                TryUpdateModel(product);
                product.DateModified = DateTime.Now;
                product.Image = imageUrl ?? oldImage;
                Repository.SubmitChanges();

                ShowSuccess("Товар или продукт успешно отредактирован");
            }

            if (!String.IsNullOrEmpty(nextAction))
            {
                return RedirectToAction("AddProduct",
                    new {id = model.PlaceId, category = model.Category, productType = model.Type});
            }

            return RedirectToAction("Products", new { id = model.PlaceId });
        }

        /// <summary>
        /// Обрабатывает удаление указанного товара или услуги
        /// </summary>
        /// <param name="id">Идентификатор события</param>
        /// <returns></returns>
        [AuthorizationCheck]
        [Route("places/products/{id}/delete")]
        public ActionResult DeleteProduct(long id)
        {
            // Ищем
            var product = ProductsRepository.Load(id);
            if (product == null)
            {
                ShowError("Такой товар не найден");
                return RedirectToAction("Index");
            }

            var pid = product.PlaceId;
            ProductsRepository.Delete(product);
            ProductsRepository.SubmitChanges();

            ShowSuccess("Товар успешно удален");

            return RedirectToAction("Products", new { id = pid });
        }

        #endregion
    }
}
