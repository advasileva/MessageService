using System.Collections.Generic;

namespace MessageService.Models
{
    /// <summary>
    /// Модель, описывающая пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string Email { get; set; }
    }
}
