﻿// 
// 
// Solution: PartDesk
// Project: PartDesk.Domain
// File: DomainLayer.cs
// 
// Created by: ykors_000 at 15.11.2013 14:06
// 
// Property of SoftGears
// 
// ========

using Autofac;
using CityPlace.Domain.Interfaces.Notifications;
using CityPlace.Domain.Notifications.Mailing;

namespace CityPlace.Domain.Utils
{
    /// <summary>
    /// Модуль регистрации зависимостей инфраструктурного слоя
    /// </summary>
    public class DomainLayer: Autofac.Module
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
            builder.RegisterType<UniSenderMailNotificationManager>().As<IMailNotificationManager>().SingleInstance();
        }
    }
}