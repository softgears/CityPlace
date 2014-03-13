// 
// 
// Solution: PartDesk
// Project: PartDesk.Web
// File: IoCConfig.cs
// 
// Created by: ykors_000 at 15.11.2013 14:13
// 
// Property of SoftGears
// 
// ========

using CityPlace.Domain.DAL;
using CityPlace.Domain.Interfaces.Notifications;
using CityPlace.Domain.IoC;
using CityPlace.Domain.Utils;
using CityPlace.Web.Classes;

namespace CityPlace.Web.App_Start
{
    /// <summary>
    /// Конфигуратор IoC контейнера
    /// </summary>
    public static class IoCConfig
    {
        /// <summary>
        /// Инициализирует IoC контейнер
        /// </summary>
        public static void Init()
        {
            Locator.Init(new DataAccessLayer(), new DomainLayer(), new WebLayer());
            Locator.GetService<IMailNotificationManager>().Init();
        }
    }
}