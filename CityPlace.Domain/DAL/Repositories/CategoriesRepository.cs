// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: CategoriesRepository.cs
// 
// Created by: ykors_000 at 19.03.2014 16:26
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;
using CityPlace.Domain.Interfaces.Repositories;

namespace CityPlace.Domain.DAL.Repositories
{
    /// <summary>
    /// СУБД реализация репозитория категорий заведений
    /// </summary>
    public class CategoriesRepository: BaseRepository<Category>, ICategoriesRepository
    {
        /// <summary>
        /// Инициализирует новый инстанс абстрактного репозитория для указанного типа
        /// </summary>
        /// <param name="dataContext"></param>
        public CategoriesRepository(CityPlaceDataContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// Загружает указанную сущность по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Сущность с указанным идентификатором или Null</returns>
        public override Category Load(long id)
        {
            return Find(c => c.Id == id);
        }
    }
}