// 
// 
// Solution: CityPlace
// Project: CityPlace.Web
// File: CityModel.cs
// 
// Created by: ykors_000 at 19.05.2014 16:43
// 
// Property of SoftGears
// 
// ========

using System.Linq.Expressions;
using System.Web.Mvc.Html;
using CityPlace.Domain.Entities;

namespace CityPlace.Web.Models.Api
{
    /// <summary>
    /// JSON модель города
    /// </summary>
    public class CityModel: BaseJsonModel
    {
        public CityModel(City city)
        {
            id = city.Id;
            name = city.Name;
            lat = city.CenterLat;
            lon = city.CenterLon;
            objType = "city";
        }

        /// <summary>
        /// Имя города
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        public double? lat { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double? lon { get; set; }
    }
}