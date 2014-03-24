// 
// 
// Solution: CityPlace
// Project: CityPlace.Web
// File: PublicationDetailsModel.cs
// 
// Created by: ykors_000 at 24.03.2014 10:39
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;

namespace CityPlace.Web.Models.Api
{
    /// <summary>
    /// Расширенная модель публикации
    /// </summary>
    public class PublicationDetailsModel: PublicationModel
    {
        /// <summary>
        /// Полное содержание статьи
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="publication">Публикация</param>
        public PublicationDetailsModel(Publication publication) : base(publication)
        {
            content = publication.Content;
        }
    }
}