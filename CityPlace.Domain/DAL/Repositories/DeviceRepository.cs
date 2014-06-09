// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: DeviceRepository.cs
// 
// Created by: ykors_000 at 09.06.2014 13:14
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Entities;
using CityPlace.Domain.Interfaces.Repositories;

namespace CityPlace.Domain.DAL.Repositories
{
	/// <summary>
	/// СУЦБД реализация репозитория устройств
	/// </summary>
	public class DeviceRepository: BaseRepository<Device>, IDeviceRepository
	{
		/// <summary>
		/// Инициализирует новый инстанс абстрактного репозитория для указанного типа
		/// </summary>
		/// <param name="dataContext"></param>
		public DeviceRepository(CityPlaceDataContext dataContext) : base(dataContext)
		{
		}

		/// <summary>
		/// Загружает указанную сущность по ее идентификатору
		/// </summary>
		/// <param name="id">Идентификатор сущности</param>
		/// <returns>Сущность с указанным идентификатором или Null</returns>
		public override Device Load(long id)
		{
			return Find(d => d.Id == id);
		}
	}
}