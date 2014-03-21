// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: PublicationsRepository.cs
// 
// Created by: ykors_000 at 21.03.2014 12:08
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;
using CityPlace.Domain.Interfaces.Repositories;

namespace CityPlace.Domain.DAL.Repositories
{
    /// <summary>
    /// СУБД реализация репозитория публикаций
    /// </summary>
    public class PublicationsRepository: BaseRepository<Publication>, IPublicationsRepository
    {
        /// <summary>
        /// Инициализирует новый инстанс абстрактного репозитория для указанного типа
        /// </summary>
        /// <param name="dataContext"></param>
        public PublicationsRepository(CityPlaceDataContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// Загружает указанную сущность по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Сущность с указанным идентификатором или Null</returns>
        public override Publication Load(long id)
        {
            return Find(p => p.Id == id);
        }
    }
}