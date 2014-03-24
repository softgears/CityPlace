// 
// 
// Solution: CityPlace
// Project: CityPlace.Web
// File: CategoryModel.cs
// 
// Created by: ykors_000 at 21.03.2014 15:50
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;

namespace CityPlace.Web.Models.Api
{
    /// <summary>
    /// Модель категории
    /// </summary>
    public class CategoryModel: BaseJsonModel
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="category">Категория</param>
        public CategoryModel(Category category)
        {
            id = category.Id;
            title = category.Title;
            img = category.Image;
            objType = "category";
        }

        /// <summary>
        /// Ссылка на изображение
        /// </summary>
        public string img { get; set; }

        /// <summary>
        /// Название мероприятия
        /// </summary>
        public string title { get; set; }
    }
}