using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using CityPlace.Domain.Entities;
using CityPlace.Domain.Interfaces.Repositories;
using CityPlace.Domain.IoC;
using CityPlace.Domain.Routing;
using CityPlace.Web.Classes.Ext;
using CityPlace.Web.Models.Api;

namespace CityPlace.Web.Controllers
{
    /// <summary>
    /// Контроллер взаимодействия с мобильным API
    /// </summary>
    public class MobileApiController : BaseController
    {
        /// <summary>
        /// Репозиторий событий
        /// </summary>
        public IEventsRepository EventsRepository { get; private set; }

        /// <summary>
        /// Репозиторий новостей
        /// </summary>
        public IPublicationsRepository PublicationsRepository { get; private set; }

        /// <summary>
        /// Репозиторий категорий
        /// </summary>
        public ICategoriesRepository CategoriesRepository { get; private set; }

        /// <summary>
        /// Репозиторий заведений
        /// </summary>
        public IPlacesRepository PlacesRepository { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Web.Http.ApiController"/> class.
        /// </summary>
        public MobileApiController()
        {
            EventsRepository = Locator.GetService<IEventsRepository>();
            PublicationsRepository = Locator.GetService<IPublicationsRepository>();
            CategoriesRepository = Locator.GetService<ICategoriesRepository>();
            PlacesRepository = Locator.GetService<IPlacesRepository>();
        }


        /// <summary>
        /// Возвращает основные данные для главной страницы
        /// </summary>
        /// <returns></returns>
        [Route("mobile-api/home-data")]
        public JsonResult GetHomeData()
        {
            var result = new List<GroupedDataItem>
            {
                new GroupedDataItem()
                {
                    key = "Мероприятия",
                    items =
                        EventsRepository.Search(e => !e.Hidden && e.StartDateTime >= DateTime.Now.Date)
                            .OrderBy(e => e.StartDateTime)
                            .Take(5).Select(e => new EventModel(e))
                            .ToList()
                },
                new GroupedDataItem()
                {
                    key = "Новости",
                    items =
                        PublicationsRepository.Search(p => !p.Hidden && p.PublicationDate <= DateTime.Now.Date)
                            .OrderByDescending(p => p.PublicationDate)
                            .Take(5).Select(p => new PublicationModel(p))
                            .ToList()
                },
                new GroupedDataItem()
                {
                    key = "Последние заведения",
                    items =
                        PlacesRepository.Search(p => !p.Hidden)
                            .OrderByDescending(p => p.DateModified).ThenBy(p => p.DateCreated)
                            .Take(5).Select(p => new PlaceModel(p))
                            .ToList()
                }
            };
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Возвращает данные о ближайших событиях
        /// </summary>
        /// <returns></returns>
        [Route("mobile-api/events")]
        public JsonResult GetEvents()
        {
            var groupedEvents =
                EventsRepository.Search(e => !e.Hidden && e.StartDateTime >= DateTime.Now.Date)
                    .GroupBy(g => g.StartDateTime.Date)
                    .OrderBy(g => g.Key)
                    .ToList();
            var result = new List<GroupedDataItem>();
            foreach (var group in groupedEvents)
            {
                string keyName;
                if (group.Key == DateTime.Now.Date)
                {
                    keyName = "Сегодня";
                } else if (group.Key == DateTime.Now.Date.AddDays(1))
                {
                    keyName = "Завтра";
                }
                else
                {
                    keyName = group.Key.FormatDate();
                }
                result.Add(new GroupedDataItem()
                {
                    key = keyName,
                    items = group.Select(e => new EventModel(e))
                });
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Возвращает последние 20 новостей
        /// </summary>
        /// <returns></returns>
        [Route("mobile-api/publications")]
        public JsonResult GetPublications()
        {
            var pubs = PublicationsRepository.Search(p => !p.Hidden && p.PublicationDate <= DateTime.Now.Date)
                .OrderByDescending(p => p.PublicationDate).Select(p => new PublicationModel(p))
                .ToList();

            return Json(pubs, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Возвращает все категории заведений
        /// </summary>
        /// <returns></returns>
        [Route("mobile-api/categories")]
        public ActionResult GetCategories()
        {
            var cats = CategoriesRepository.Search(c => !c.Hidden).OrderBy(c => c.Title).Select(c => new CategoryModel(c)).ToList();

            return Json(cats, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Возращает все заведения в указанной категории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("mobile-api/places/{id}")]
        public ActionResult GetPlaces(long id)
        {
            var cat = CategoriesRepository.Load(id);
            if (cat == null)
            {
                return Json(Enumerable.Empty<PlaceModel>(),JsonRequestBehavior.AllowGet);
            }

            return Json(cat.Places.Where(p => !p.Hidden).OrderBy(p => p.Title).Select(p => new PlaceModel(p)).ToList(),JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Возвращает подробности об указанном событии
        /// </summary>
        /// <param name="id">Идентификатор события</param>
        /// <returns></returns>
        [Route("mobile-api/event/{id}")]
        public ActionResult GetEventInfo(long id)
        {
            var ev = EventsRepository.Load(id);
            if (ev == null)
            {
                return Json(new EventDetailsModel(new Event()
                {
                    Title = "Не найдено"
                }), JsonRequestBehavior.AllowGet);
            }

            return Json(new EventDetailsModel(ev), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Возвращает подробности об указанной новости
        /// </summary>
        /// <param name="id">Идентификатор новости</param>
        /// <returns></returns>
        [Route("mobile-api/publication/{id}")]
        public ActionResult GetPublicationInfo(long id)
        {
            var pub = PublicationsRepository.Load(id);
            if (pub == null)
            {
                return Json(new PublicationDetailsModel(new Publication()
                {
                    Title = "Не найдено"
                }), JsonRequestBehavior.AllowGet);
            }

            return Json(new PublicationDetailsModel(pub), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Возвращает подробности об указанном месте
        /// </summary>
        /// <param name="id">Идентификатор места</param>
        /// <returns></returns>
        [Route("mobile-api/place/{id}")]
        public ActionResult GetPlaceInfo(long id)
        {
            var place = PlacesRepository.Load(id);
            if (place == null)
            {
                return Json(new PlaceDetailsModel(new Place()
                {
                    Title = "Не найдено"
                }), JsonRequestBehavior.AllowGet);
            }

            return Json(new PlaceDetailsModel(place), JsonRequestBehavior.AllowGet);
        }
    }
}
