// 
// 
// Solution: CityPlace
// Project: CityPlace.Web
// File: EventModel.cs
// 
// Created by: ykors_000 at 21.03.2014 15:28
// 
// Property of SoftGears
// 
// ========

using System.Data.Common;
using System.Web.UI.WebControls;
using CityPlace.Domain.Entities;

namespace CityPlace.Web.Models.Api
{
    /// <summary>
    /// Модель события
    /// </summary>
    public class EventModel
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="event"></param>
        public EventModel(Event @event)
        {
            id = @event.Id;
            title = @event.Title;
            img = @event.Image;
        }

        /// <summary>
        /// Ссылка на изображение
        /// </summary>
        public string img { get; set; }

        /// <summary>
        /// Название мероприятия
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public long id { get; set; }
    }
}