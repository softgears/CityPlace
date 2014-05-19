// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: CitiesRepository.cs
// 
// Created by: ykors_000 at 19.05.2014 16:42
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;
using CityPlace.Domain.Interfaces.Repositories;

namespace CityPlace.Domain.DAL.Repositories
{
    /// <summary>
    /// СУБД реализация репозитория городов
    /// </summary>
    public class CitiesRepository: BaseRepository<City>, ICitiesRepository
    {
        /// <summary>
        /// Инициализирует новый инстанс абстрактного репозитория для указанного типа
        /// </summary>
        /// <param name="dataContext"></param>
        public CitiesRepository(CityPlaceDataContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// Загружает указанную сущность по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Сущность с указанным идентификатором или Null</returns>
        public override City Load(long id)
        {
            return Find(c => c.Id == id);
        }
    }
}