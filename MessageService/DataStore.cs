using MessageService.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MessageService
{
    public static class DataStore
    {
        public static List<User> ReadUsers()
        {
            string data = File.ReadAllText(StaticSettings.UsersFile);
            return JsonSerializer.Deserialize<List<User>>(data);
        }

        public static void UpdateUsers(List<User> users)
        {
            users.Sort((first, second) => -first.Email.CompareTo(second.Email));
            string data = JsonSerializer.Serialize(users);
            File.WriteAllText(StaticSettings.UsersFile, data);
        }

        public static List<UserMessage> ReadMessages()
        {
            string data = File.ReadAllText(StaticSettings.MessagesFile);
            return JsonSerializer.Deserialize<List<UserMessage>>(data);
        }

        public static void UpdateMessages(List<UserMessage> messages)
        {
            string data = JsonSerializer.Serialize(messages);
            File.WriteAllText(StaticSettings.MessagesFile, data);
        }
    }
}
