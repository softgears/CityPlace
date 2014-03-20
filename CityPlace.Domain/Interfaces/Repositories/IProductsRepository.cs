// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: IProductsRepository.cs
// 
// Created by: ykors_000 at 20.03.2014 12:41
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;

namespace CityPlace.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Абстрактный репозиторий продуктов
    /// </summary>
    public interface IProductsRepository: IBaseRepository<Product>
    {
         
    }
}