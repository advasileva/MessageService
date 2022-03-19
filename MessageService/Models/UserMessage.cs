using System.Collections.Generic;

namespace MessageService.Models
{
    /// <summary>
    /// Модель, описывающая сообщение.
    /// </summary>
    public class UserMessage
    {
        /// <summary>
        /// Тема сообщения.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Текстовое содержание сообщения.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Идентификатор отправителя.
        /// </summary>
        public string SenderId { get; set; }

        /// <summary>
        /// Идентификатор получателя.
        /// </summary>
        public string ReceiverId { get; set; }
    }
}