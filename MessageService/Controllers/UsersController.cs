using MessageService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace MessageService.Controllers
{
    /// <summary>
    /// Контроллер для запросов на действия со списком пользователей.
    /// </summary>
    [Route("api/message-service/users")]
    [ApiController]
    public class UsersController : MessageServiceController
    {
        /// <summary>
        /// Получение информации о пользователе по идентификатору.
        /// </summary>
        /// <param name="email">Идентификатор пользователя.</param>
        /// <returns>Результат поиска.</returns>
        [HttpGet("by-email")]
        public IActionResult GetUser(string email)
        {
            User user = _users.SingleOrDefault(p => p.Email == email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Получение всего списка пользователей.
        /// </summary>
        /// <returns>Список всех пользователей.</returns>
        [HttpGet()]
        public IActionResult GetUsers()
        {
            return Ok(_users);
        }

        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        /// <param name="userName">Имя.</param>
        /// <param name="email">Идентификатор.</param>
        /// <returns>Результат добавления.</returns>
        [HttpPost("add")]
        public IActionResult PostUser(string userName, string email)
        {
            if (_users.Any(user => user.Email == email) || email == null)
            {
                return NotFound();
            }
            _users.Add(new User
            {
                UserName = userName,
                Email = email
            });
            return Ok();
        }

        /// <summary>
        /// Получение выборки пользователей.
        /// </summary>
        /// <param name="limit">Потолок выборки.</param>
        /// <param name="offset">Количество пропущенных с начала пользователей.</param>
        /// <returns>Выборка пользователей.</returns>
        [HttpGet("by-limit-and-offset")]
        public IActionResult GetUsersLimitOffset(int limit, int offset)
        {
            if (limit <= 0 || offset < 0)
            {
                return BadRequest();
            }
            return Ok(_users.Skip(offset).Take(limit));
        }
    }
}
