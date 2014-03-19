// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: ICategoriesRepository.cs
// 
// Created by: ykors_000 at 19.03.2014 16:26
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;

namespace CityPlace.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Абстрактный репозиторий категорий
    /// </summary>
    public interface ICategoriesRepository: IBaseRepository<Category>
    {
         
    }
}