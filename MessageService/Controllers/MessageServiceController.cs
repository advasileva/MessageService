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
        private DataStore _dataStore;

        /// <summary>
        /// Явно определённый конструктор для подписки на события изменения коллекций.
        /// </summary>
        public MessageServiceController(DataStore dataStore)
        {
            _dataStore = dataStore;
            _users = new(_dataStore.ReadUsers());
            _messages = new(_dataStore.ReadMessages());
            _users.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                _dataStore.UpdateUsers(_users.ToList());
                _users = new(_dataStore.ReadUsers());
            };
            _messages.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                _dataStore.UpdateMessages(_messages.ToList());
                _messages = new(_dataStore.ReadMessages());
            };
        }

        /// <summary>
        /// Актуальный список пользователей.
        /// </summary>
        protected ObservableCollection<User> _users;

        /// <summary>
        /// Актуальный список сообщений.
        /// </summary>
        protected ObservableCollection<UserMessage> _messages;

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
            _dataStore.UpdateUsers(_users.ToList()); 
            _users = new(_dataStore.ReadUsers());
            _messages = new(Generator.GenerateMessages());
            _dataStore.UpdateMessages(_messages.ToList());
            _messages = new(_dataStore.ReadMessages());
            return Ok("Списки инициализированы");
        }
    }
}