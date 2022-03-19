using MessageService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MessageService.Services;

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
        /// Явно определённый конструктор для подписки на события изменения коллекций.
        /// </summary>
        public MessageServiceController()
        {
            _users.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                DataStore.UpdateUsers(_users.ToList());
                _users = new(DataStore.ReadUsers());
            };
            _messages.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                DataStore.UpdateMessages(_messages.ToList());
                _messages = new(DataStore.ReadMessages());
            };
        }

        /// <summary>
        /// Актуальный список пользователей.
        /// </summary>
        protected static ObservableCollection<User> _users = new(DataStore.ReadUsers());

        /// <summary>
        /// Актуальный список сообщений.
        /// </summary>
        protected ObservableCollection<UserMessage> _messages = new(DataStore.ReadMessages());

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
            _users = new(Generator.GenerateUsers());
            // Дублирование кода выходит...
            // Вероятно, надо было искусственно вызывать CollectionChanged
            // или выносить обработчики в нормальный метод
            DataStore.UpdateUsers(_users.ToList()); 
            _users = new(DataStore.ReadUsers());
            _messages = new(Generator.GenerateMessages());
            DataStore.UpdateMessages(_messages.ToList());
            _messages = new(DataStore.ReadMessages());
            return Ok();
        }
    }
}