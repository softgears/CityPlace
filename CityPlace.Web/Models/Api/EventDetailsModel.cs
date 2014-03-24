// 
// 
// Solution: CityPlace
// Project: CityPlace.Web
// File: EventDetailsModel.cs
// 
// Created by: ykors_000 at 24.03.2014 10:36
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;
using CityPlace.Web.Classes.Ext;

namespace CityPlace.Web.Models.Api
{
    /// <summary>
    /// Детальная модель события
    /// </summary>
    public class EventDetailsModel: EventModel
    {
        /// <summary>
        /// Расширенное описание события
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Дата и время конца мероприятия
        /// </summary>
        public string event_end { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="event"></param>
        public EventDetailsModel(Event @event) : base(@event)
        {
            description = @event.Description;
            event_end = @event.EndDateTime.FormatDateTime();
        }
    }
}