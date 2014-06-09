// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: PlatformUtils.cs
// 
// Created by: ykors_000 at 09.06.2014 13:19
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.Enums;

namespace CityPlace.Domain.Utils
{
	/// <summary>
	/// Статический класс со вспомомагательными методами для работы с идентификаторами платформ
	/// </summary>
	public static class PlatformUtils
	{
		/// <summary>
		/// Выполняет парсинг типа платформы из названия
		/// </summary>
		/// <param name="platform">Тип платформы в виде строки</param>
		/// <returns></returns>
		public static MobilePlatform Parse(string platform)
		{
			switch (platform)
			{
				case "ios":
					return MobilePlatform.iOS;
					break;
				case "android":
					return MobilePlatform.Android;
					break;
				case "wp8":
					return MobilePlatform.WP8;;
					break;
				default:
					return MobilePlatform.Generic;
					break;
			}
		}
	}
}