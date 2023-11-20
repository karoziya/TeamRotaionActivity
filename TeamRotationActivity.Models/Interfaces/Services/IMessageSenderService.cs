using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRotationActivity.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис отправки сообщений.
    /// </summary>
    public interface IMessageSenderService
    {
        /// <summary>
        /// Отправить сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void SendMessage(string message);
    }
}
