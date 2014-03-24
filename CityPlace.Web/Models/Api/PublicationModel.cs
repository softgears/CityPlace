// 
// 
// Solution: CityPlace
// Project: CityPlace.Web
// File: PublicationModel.cs
// 
// Created by: ykors_000 at 21.03.2014 15:31
// 
// Property of SoftGears
// 
// ========

using System.Web.UI.WebControls;
using CityPlace.Domain.Entities;
using CityPlace.Web.Classes.Ext;

namespace CityPlace.Web.Models.Api
{
    /// <summary>
    /// Модель публикации
    /// </summary>
    public class PublicationModel: BaseJsonModel
    {
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="publication">Публикация</param>
        public PublicationModel(Publication publication)
        {
            id = publication.Id;
            title = publication.Title;
            img = publication.Image;
            annotation = publication.Annotation;
            pdate = publication.PublicationDate.FormatDate();
            objType = "publication";
        }

        /// <summary>
        /// Аннотация
        /// </summary>
        public string annotation { get; set; }

        /// <summary>
        /// Ссылка на изображение
        /// </summary>
        public string img { get; set; }

        /// <summary>
        /// Название мероприятия
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Дата публикации
        /// </summary>
        public string pdate { get; set; }
    }
}