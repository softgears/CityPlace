// 
// 
// Solution: PartDesk
// Project: PartDesk.Web
// File: WebLayer.cs
// 
// Created by: ykors_000 at 15.11.2013 14:14
// 
// Property of SoftGears
// 
// ========

using Autofac;
using Autofac.Integration.Mvc;
using CityPlace.Domain.DAL;
using CityPlace.Domain.Interfaces.Notifications;
using CityPlace.Web.Classes.Notifications.UI;

namespace CityPlace.Web.Classes
{
    /// <summary>
    /// Модуль регистрации зависимостей Web слоя
    /// </summary>
    public class WebLayer: Autofac.Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        ///             registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CityPlaceDataContext>().AsSelf().InstancePerHttpRequest();
            builder.RegisterType<UINotificationManager>().As<IUINotificationManager>();
        }
    }
}