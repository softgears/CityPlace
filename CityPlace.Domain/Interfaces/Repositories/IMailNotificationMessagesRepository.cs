using System.Collections.Generic;
using CityPlace.Domain.Entities;

namespace CityPlace.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Абстрактный интерфейс очереди нотифицируемых сообщений
    /// </summary>
    public interface IMailNotificationMessagesRepository: IBaseRepository<MailNotificationMessage>
    {
        /// <summary>
        /// Возвращает список сообщений, находящихся в очереди на отправку
        /// </summary>
        /// <returns></returns>
        IEnumerable<MailNotificationMessage> GetEnqueuedMessages();
    }
}