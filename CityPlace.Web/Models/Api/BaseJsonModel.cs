namespace CityPlace.Web.Models.Api
{
    /// <summary>
    /// Базовый класс для всех JSON моделей
    /// </summary>
    public abstract class BaseJsonModel
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// Тип объекта
        /// </summary>
        public string objType { get; set; }
    }
}