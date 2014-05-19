// 
// 
// Solution: SeoLight
// Project: SeoLight.Domain
// File: DataAccessLayer.cs
// 
// Created by: ykors_000 at 10.03.2014 13:11
// 
// Property of SoftGears
// 
// ========

using Autofac;
using CityPlace.Domain.DAL.Repositories;
using CityPlace.Domain.Entities;
using CityPlace.Domain.Interfaces.Repositories;

namespace CityPlace.Domain.DAL
{
    /// <summary>
    /// Модуль регистрации зависимостей DAL слоя
    /// </summary>
    public class DataAccessLayer: Autofac.Module
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
            builder.RegisterType<UsersRepository>().As<IUsersRepository>();
            builder.RegisterType<PermissionsRepository>().As<IPermissionsRepository>();
            builder.RegisterType<RolesRepository>().As<IRolesRepository>();
            builder.RegisterType<MailNotificationMessagesRepository>().As<IMailNotificationMessagesRepository>();
            builder.RegisterType<CategoriesRepository>().As<ICategoriesRepository>();
            builder.RegisterType<PlacesRepository>().As<IPlacesRepository>();
            builder.RegisterType<EventsRepository>().As<IEventsRepository>();
            builder.RegisterType<ProductsRepository>().As<IProductsRepository>();
            builder.RegisterType<PublicationsRepository>().As<IPublicationsRepository>();
            builder.RegisterType<CitiesRepository>().As<ICitiesRepository>();
        }
    }
}