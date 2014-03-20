// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: ProductsRepository.cs
// 
// Created by: ykors_000 at 20.03.2014 12:41
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;
using CityPlace.Domain.Interfaces.Repositories;

namespace CityPlace.Domain.DAL.Repositories
{
    /// <summary>
    /// СУБД реализация репозитория товаров
    /// </summary>
    public class ProductsRepository: BaseRepository<Product>, IProductsRepository
    {
        /// <summary>
        /// Инициализирует новый инстанс абстрактного репозитория для указанного типа
        /// </summary>
        /// <param name="dataContext"></param>
        public ProductsRepository(CityPlaceDataContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// Загружает указанную сущность по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Сущность с указанным идентификатором или Null</returns>
        public override Product Load(long id)
        {
            return Find(p => p.Id == id);
        }
    }
}