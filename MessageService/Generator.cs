using MessageService.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MessageService
{
    public static class Generator
    {
        private static readonly Random _random = new();

        public static List<User> GenerateUsers()
        {
            List<User> users = new();
            int count = _random.Next(5, 10);
            for (int i = 0; i < count; i++)
            {
                users.Add(new User
                {
                    UserName = GenerateString(_random.Next(3, 6)),
                    // Проверять уникальность
                    Email = GenerateString(_random.Next(8, 12)) + '@' + emails[_random.Next(emails.Length)],
                });
            }
            return users;
        }
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

        private static string GenerateString(int lenght)
        {
            char[] chars = new char[lenght];
            for (int i = 0; i < lenght; i++)
            {
                chars[i] = (char)('a' + _random.Next(26));
            } 
            return new string(chars);
        }

        private static string[] emails = { "edu.hse.ru", "yandex.ru", "gmail.com", "mail.ru", "yahoo.com" };
    }
}
