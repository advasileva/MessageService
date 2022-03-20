using MessageService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MessageService.Services
{
    /// <summary>
    /// Сервис для доступа к хранилищу данных.
    /// </summary>
    public class DataStore
    {
        /// <summary>
        /// Получение списка пользователей.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        public List<User> ReadUsers()
        {
            try
            {
                string data = File.ReadAllText(ConfigSettings.UsersFile);
                return JsonSerializer.Deserialize<List<User>>(data);
            }
            catch
            {
                return new List<User>();
            }
        }

        /// <summary>
        /// Обновление списка пользователей.
        /// </summary>
        /// <param name="users">Список пользователей.</param>
        public void UpdateUsers(List<User> users)
        {
            try
            {
                users.Sort((first, second) => first.Email.CompareTo(second.Email));
                string data = JsonSerializer.Serialize(users);
                File.WriteAllText(ConfigSettings.UsersFile, data);
            }
            catch { }
        }

        /// <summary>
        /// Получение списка сообщений.
        /// </summary>
        /// <returns>Список сообщений.</returns>
        public List<UserMessage> ReadMessages()
        {
            try
            {
                string data = File.ReadAllText(ConfigSettings.MessagesFile);
                return JsonSerializer.Deserialize<List<UserMessage>>(data);
            }
            catch
            {
                return new List<UserMessage>();
            }
        }

        /// <summary>
        /// Обновление списка сообщений.
        /// </summary>
        /// <param name="messages">Список сообщений.</param>
        public void UpdateMessages(List<UserMessage> messages)
        {
            try
            {
                string data = JsonSerializer.Serialize(messages);
                File.WriteAllText(ConfigSettings.MessagesFile, data);
            }
            catch { }
        }
    }
}
