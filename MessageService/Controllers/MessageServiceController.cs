using MessageService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MessageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageServiceController : Controller
    {
        private List<User> _users
        {
            get => DataStore.ReadUsers();
            set => DataStore.UpdateUsers(value);
        }

        private List<UserMessage> _messages
        {
            get => DataStore.ReadMessages();
            set => DataStore.UpdateMessages(value);
        }

        /// <summary>
        /// Инициализация списка пользователей и списка сообщений.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Получение информации о пользователе по идентификатору.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("Users/ByEmail")]
        public IActionResult GetUser(string email)
        {
            User? user = _users.SingleOrDefault(p => p.Email == email);
            System.Diagnostics.Debug.WriteLine(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Получение всего списка пользователей.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Users")]
        public IEnumerable<User> Get()
        {
            return _users;
        }

        /// <summary>
        /// Получения списка сообщений по идентификаторам отправителя и получателя.
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="receiverId"></param>
        /// <returns></returns>
        [HttpGet("Messages/BySenderReceiver")]
        public IActionResult GetMessagesSenderReceiver(string senderId, string receiverId)
        {
            List<UserMessage> selectedMessages = _messages.Where(message =>
                message.SenderId == senderId && message.ReceiverId == receiverId).ToList();
            return Ok(selectedMessages);
        }

        /// <summary>
        /// Получения списка сообщений по идентификатору отправителя.
        /// </summary>
        /// <param name="senderId"></param>
        /// <returns></returns>
        [HttpGet("Messages/BySender")]
        public IActionResult GetMessagesSender(string senderId)
        {
            List<UserMessage> selectedMessages = _messages.Where(message =>
                message.SenderId == senderId).ToList();
            return Ok(selectedMessages);
        }

        /// <summary>
        /// Получения списка сообщений по идентификатору получателя.
        /// </summary>
        /// <param name="receiverId"></param>
        /// <returns></returns>
        [HttpGet("Messages/ByReceiver")]
        public IActionResult GetMessagesReceiver(string receiverId)
        {
            List<UserMessage> selectedMessages = _messages.Where(message =>
                message.ReceiverId == receiverId).ToList();
            return Ok(selectedMessages);
        }

        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        [HttpPost("Users/Add")]
        public IActionResult PostUser(string UserName, string Email)
        {
            if (_users.Any(user => user.Email == Email))
            {
                // Подобрать более точный код
                return Forbid();
            }
            // Вынести в отдельный метод
            _users.Add(new User
            {
                UserName = UserName,
                Email = Email
            });
            return Ok();
        }
    }
}
