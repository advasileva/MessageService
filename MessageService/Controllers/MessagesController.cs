using MessageService.Models;
using MessageService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MessageService.Controllers
{
    /// <summary>
    /// Контроллер для запросов на действия со списком сообщений.
    /// </summary>
    [Route("api/message-service/messages")]
    [ApiController]
    public class MessagesController : MessageServiceController
    {
        /// <summary>
        /// Конструктор вспомогательного контроллера.
        /// </summary>
        /// <param name="dataStore">Экземпляр хранилища данных.</param>
        public MessagesController(DataStore dataStore) : base(dataStore) 
        { }

        /// <summary>
        /// Получения списка сообщений по идентификаторам отправителя и получателя.
        /// </summary>
        /// <param name="senderId">Идентификатор отправителя.</param>
        /// <param name="receiverId">Идентификатор получателя.</param>
        /// <returns>Выборка сообщений.</returns>
        [HttpGet("messages/by-sender-and-receiver")]
        public IActionResult GetMessagesSenderReceiver(string senderId, string receiverId)
        {
            List<UserMessage> selectedMessages = _messages.Where(message =>
                message.SenderId == senderId && message.ReceiverId == receiverId).ToList();
            return Ok(selectedMessages);
        }

        /// <summary>
        /// Получения списка сообщений по идентификатору отправителя.
        /// </summary>
        /// <param name="senderId">Идентификатор отправителя.</param>
        /// <returns>Выборка сообщений.</returns>
        [HttpGet("messages/by-sender")]
        public IActionResult GetMessagesSender(string senderId)
        {
            List<UserMessage> selectedMessages = _messages.Where(message =>
                message.SenderId == senderId).ToList();
            return Ok(selectedMessages);
        }

        /// <summary>
        /// Получения списка сообщений по идентификатору получателя.
        /// </summary>
        /// <param name="receiverId">Идентификатор получателя.</param>
        /// <returns>Выборка сообщений.</returns>
        [HttpGet("messages/by-receiver")]
        public IActionResult GetMessagesReceiver(string receiverId)
        {
            List<UserMessage> selectedMessages = _messages.Where(message =>
                message.ReceiverId == receiverId).ToList();
            return Ok(selectedMessages);
        }

        /// <summary>
        /// Добавление нового сообщения.
        /// </summary>
        /// <param name="subject">Тема сообщения.</param>
        /// <param name="message">Текстовое содержание сообщения.</param>
        /// <param name="senderId">Идентификатор отправителя.</param>
        /// <param name="receiverId">Идентификатор получателя.</param>
        /// <returns>Результат добавления.</returns>
        [HttpPost("messages/add")]
        public IActionResult PostMessage(string subject, string message, string senderId, string receiverId)
        {
            if (!_users.Any(user => user.Email == senderId) || !_users.Any(user => user.Email == receiverId))
            {
                return NotFound("Не найден получатель или отправитель");
            }
            _messages.Add(new UserMessage
            {
                Subject = subject,
                Message = message,
                SenderId = senderId,
                ReceiverId = receiverId
            });
            return Ok("Сообщение отправлено");
        }
    }
}
