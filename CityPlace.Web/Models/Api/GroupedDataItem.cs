// 
// 
// Solution: CityPlace
// Project: CityPlace.Web
// File: HomeViewDataItem.cs
// 
// Created by: ykors_000 at 21.03.2014 14:50
// 
// Property of SoftGears
// 
// ========

using System.Collections.Generic;

namespace CityPlace.Web.Models.Api
{
    /// <summary>
    /// Модель элемента данных главной страницы
    /// </summary>
    public class GroupedDataItem
    {
        /// <summary>
        /// Ключ группировки
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// Содержимое пунктов группировки
        /// </summary>
        public IEnumerable<object>  items { get; set; }
    }
}