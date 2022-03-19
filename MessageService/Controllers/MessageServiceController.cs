using MessageService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MessageService.Controllers
{
    /// <summary>
    /// Основной контроллер сервиса сообщений.
    /// </summary>
    [Route("api/message-service")]
    [ApiController]
    public class MessageServiceController : Controller
    {
        /// <summary>
        /// Свойство для получения актуального списка пользователей.
        /// </summary>
        protected static List<User> _users
        {
            get => DataStore.ReadUsers();
            set => DataStore.UpdateUsers(value);
        }

        /// <summary>
        /// Свойство для получения актуального списка сообщений.
        /// </summary>
        protected List<UserMessage> _messages
        {
            get => DataStore.ReadMessages();
            set => DataStore.UpdateMessages(value);
        }

        /// <summary>
        /// Инициализация списка пользователей и списка сообщений.
        /// </summary>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        public IActionResult Post()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _users = Generator.GenerateUsers();
            _messages = Generator.GenerateMessages();
            return Ok();
        }
    }
}