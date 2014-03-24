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
using CityPlace.Web.Classes.Ext;

namespace CityPlace.Web.Models.Api
{
    /// <summary>
    /// Модель события
    /// </summary>
    public class EventModel: BaseJsonModel
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
            event_start = @event.StartDateTime.FormatDateTime();
            objType = "event";
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
        /// Дата и время начала события
        /// </summary>
        public string event_start { get; set; }
    }
}