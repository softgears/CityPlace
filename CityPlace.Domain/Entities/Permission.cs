// 
// 
// Solution: PartDesk
// Project: PartDesk.Domain
// File: Permission.cs
// 
// Created by: ykors_000 at 18.11.2013 13:23
// 
// Property of SoftGears
// 
// ========
namespace CityPlace.Domain.Entities
{
    /// <summary>
    /// Разрешение на выполнение определенного действия или группы действий или доступа к указанному разделу
    /// </summary>
    public partial class Permission
    {
        /// <summary>
        /// Управление пользователями
        /// </summary>
        public const long ManageUsers = 1;

        /// <summary>
        /// Управление ролями
        /// </summary>
        public const long ManageRoles = 2;

        /// <summary>
        /// Управление настройками работы системы
        /// </summary>
        public const long ManageSettings = 3;

        /// <summary>
        /// Просмотр событий аудита системы
        /// </summary>
        public const long ManageAudit = 4;

        /// <summary>
        /// Работа с категориями заведений
        /// </summary>
        public const long Places = 5;

        /// <summary>
        /// Работа с заведениями и событиями в них
        /// </summary>
        public const long Categories = 6;
    }
}