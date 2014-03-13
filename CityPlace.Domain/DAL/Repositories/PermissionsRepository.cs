// ============================================================
// 
// 	Asgard
// 	Asgard.Domain 
// 	PermissionsRepository.cs
// 
// 	Created by: ykorshev 
// 	 at 03.08.2013 10:33
// 
// ============================================================

using CityPlace.Domain.Entities;
using CityPlace.Domain.Interfaces.Repositories;

namespace CityPlace.Domain.DAL.Repositories
{
    /// <summary>
    /// СУБД реализация репозитория пермишеннов
    /// </summary>
    public class PermissionsRepository: BaseRepository<Permission>, IPermissionsRepository
    {
        /// <summary>
        /// Инициализирует новый инстанс абстрактного репозитория для указанного типа
        /// </summary>
        /// <param name="dataContext"></param>
        public PermissionsRepository(CityPlaceDataContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Загружает указанную сущность по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Сущность с указанным идентификатором или Null</returns>
        public override Permission Load(long id)
        {
            return Find(p => p.Id == id);
        }
    }
}