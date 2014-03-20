// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: IPlacesRepository.cs
// 
// Created by: ykors_000 at 20.03.2014 10:58
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;

namespace CityPlace.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Абстрактный репозиторий мест
    /// </summary>
    public interface IPlacesRepository: IBaseRepository<Place>
    {
         
    }
}