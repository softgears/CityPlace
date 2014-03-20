// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: ProductType.cs
// 
// Created by: ykors_000 at 20.03.2014 12:38
// 
// Property of SoftGears
// 
// ========
namespace CityPlace.Domain.Enums
{
    /// <summary>
    /// Типы продукции
    /// </summary>
    public enum ProductType: short
    {
        /// <summary>
        /// Услуга
        /// </summary>
        [EnumDescription("Услуга")]
        Service = 1,

        /// <summary>
        /// Товар
        /// </summary>
        [EnumDescription("Товар")]
        Product = 2,

        /// <summary>
        /// Блюдо или напиток из меню
        /// </summary>
        [EnumDescription("Элемент меню")]
        MenuItem = 3
    }
}