namespace MessageService.Models
{
    /// <summary>
    /// Класс настроек проекта.
    /// </summary>
    public static class ConfigSettings
    {
        /// <summary>
        /// Путь к файлу со списком пользователей.
        /// </summary>
        public const string UsersFile = "DataBase/Users.json";

        /// <summary>
        /// Путь к файлу со списком сообщений.
        /// </summary>
        public const string MessagesFile = "DataBase/Messages.json";
    }
}
