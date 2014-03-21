// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: IPublicationsRepository.cs
// 
// Created by: ykors_000 at 21.03.2014 12:07
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;

namespace CityPlace.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Абстрактный репозиторий публикаций
    /// </summary>
    public interface IPublicationsRepository: IBaseRepository<Publication>
    {
         
    }
}