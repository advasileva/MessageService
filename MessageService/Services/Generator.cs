using MessageService.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MessageService.Services
{
    /// <summary>
    /// Сервис для генерации объектов.
    /// </summary>
    public static class Generator
    {
        /// <summary>
        /// Экземпляр рандомайзера.
        /// </summary>
        private static readonly Random _random = new();

        /// <summary>
        /// Получение списка корректных пользователей.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        public static List<User> GenerateUsers()
        {
            List<User> users = new();
            int count = _random.Next(5, 10);
            for (int i = 0; i < count; i++)
            {
                string email;
                do
                {
                    email = GenerateString(_random.Next(8, 12)) + '@' + emails[_random.Next(emails.Length)];
                } while (users.Any(user => user.Email==email));
                users.Add(new User
                {
                    UserName = GenerateString(_random.Next(3, 6)),
                    Email = email,
                });
            }
            return users;
        }

        /// <summary>
        /// Получение списка сообщений.
        /// </summary>
        /// <returns>Список сообщений.</returns>
        public static List<UserMessage> GenerateMessages()
        {
            List<UserMessage> messages = new();
            int count = _random.Next(10, 20);
            for (int i = 0; i < count; i++)
            {
                messages.Add(new UserMessage
                {
                    Subject = GenerateString(_random.Next(10, 15)),
                    Message = GenerateString(_random.Next(20,40)),
                    SenderId = _random.Next(10).ToString(),
                    ReceiverId =  _random.Next(10).ToString(),
                });
            }
            return messages;
        }

        /// <summary>
        /// Получение случайной строки заданной длины.
        /// </summary>
        /// <param name="lenght">Длина строки.</param>
        /// <returns>Случайная строка.</returns>
        private static string GenerateString(int lenght)
        {
            char[] chars = new char[lenght];
            for (int i = 0; i < lenght; i++)
            {
                chars[i] = (char)('a' + _random.Next(26));
            } 
            return new string(chars);
        }

        /// <summary>
        /// Список доменов почт.
        /// </summary>
        private static string[] emails = { "edu.hse.ru", "yandex.ru", "gmail.com", "mail.ru", "yahoo.com" };
    }
}
