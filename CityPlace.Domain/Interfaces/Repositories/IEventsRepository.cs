// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: IEventsRepository.cs
// 
// Created by: ykors_000 at 20.03.2014 12:03
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;

namespace CityPlace.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Абстрактный репозиторий событий
    /// </summary>
    public interface IEventsRepository: IBaseRepository<Event>
    {
         
    }
}