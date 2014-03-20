// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: EventsRepository.cs
// 
// Created by: ykors_000 at 20.03.2014 12:04
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;
using CityPlace.Domain.Interfaces.Repositories;

namespace CityPlace.Domain.DAL.Repositories
{
    /// <summary>
    /// СУБД реализация репозитория событий
    /// </summary>
    public class EventsRepository: BaseRepository<Event>, IEventsRepository
    {
        /// <summary>
        /// Инициализирует новый инстанс абстрактного репозитория для указанного типа
        /// </summary>
        /// <param name="dataContext"></param>
        public EventsRepository(CityPlaceDataContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// Загружает указанную сущность по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Сущность с указанным идентификатором или Null</returns>
        public override Event Load(long id)
        {
            return Find(e => e.Id == id);
        }
    }
}