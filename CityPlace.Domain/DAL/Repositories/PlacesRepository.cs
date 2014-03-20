// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: PlacesRepository.cs
// 
// Created by: ykors_000 at 20.03.2014 10:59
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;
using CityPlace.Domain.Interfaces.Repositories;

namespace CityPlace.Domain.DAL.Repositories
{
    /// <summary>
    /// СУБД реализация репозитория мест
    /// </summary>
    public class PlacesRepository: BaseRepository<Place>, IPlacesRepository
    {
        /// <summary>
        /// Инициализирует новый инстанс абстрактного репозитория для указанного типа
        /// </summary>
        /// <param name="dataContext"></param>
        public PlacesRepository(CityPlaceDataContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// Загружает указанную сущность по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Сущность с указанным идентификатором или Null</returns>
        public override Place Load(long id)
        {
            return Find(p => p.Id == id);
        }
    }
}