// 
// 
// Solution: CityPlace
// Project: CityPlace.Domain
// File: MobilePlatform.cs
// 
// Created by: ykors_000 at 09.06.2014 13:20
// 
// Property of SoftGears
// 
// ========
namespace CityPlace.Domain.Enums
{
	/// <summary>
	/// Типы мобильных платформ
	/// </summary>
	public enum MobilePlatform: short
	{
		[EnumDescription("iOS")]
		iOS = 1,

		[EnumDescription("Android")]
		Android = 2,

		[EnumDescription("Windows Phone 8")]
		WP8 = 3,

		[EnumDescription("Общая")]
		Generic = 0
	}
}