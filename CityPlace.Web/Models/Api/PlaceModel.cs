// 
// 
// Solution: CityPlace
// Project: CityPlace.Web
// File: PlaceModel.cs
// 
// Created by: ykors_000 at 24.03.2014 10:27
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;

namespace CityPlace.Web.Models.Api
{
    /// <summary>
    /// JSOn модель заведения для API
    /// </summary>
    public class PlaceModel
    {
        /// <summary>
        /// Идентификтаор
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Логотип
        /// </summary>
        public string img { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public PlaceModel(Place model)
        {
            id = model.Id;
            title = model.Title;
            address = model.Address;
            img = model.Image;
        }
    }
}