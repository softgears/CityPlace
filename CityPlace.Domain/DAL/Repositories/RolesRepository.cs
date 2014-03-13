// ============================================================
// 
// 	Asgard
// 	Asgard.Domain 
// 	RolesRepository.cs
// 
// 	Created by: ykorshev 
// 	 at 18.09.2013 0:36
// 
// ============================================================

using CityPlace.Domain.Entities;
using CityPlace.Domain.Interfaces.Repositories;

namespace CityPlace.Domain.DAL.Repositories
{
    /// <summary>
    /// СУБД реализация репозитория ролей
    /// </summary>
    public class RolesRepository: BaseRepository<Role>, IRolesRepository
    {
        /// <summary>
        /// Инициализирует новый инстанс абстрактного репозитория для указанного типа
        /// </summary>
        /// <param name="dataContext"></param>
        public RolesRepository(CityPlaceDataContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Загружает указанную сущность по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Сущность с указанным идентификатором или Null</returns>
        public override Role Load(long id)
        {
            return Find(r => r.Id == id);
        }
    }
}