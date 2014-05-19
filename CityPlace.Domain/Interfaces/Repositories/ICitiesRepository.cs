// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: ICitiesRepository.cs
// 
// Created by: ykors_000 at 19.05.2014 16:41
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;

namespace CityPlace.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Абстрактный репозиторий городов
    /// </summary>
    public interface ICitiesRepository: IBaseRepository<City>
    {
         
    }
}