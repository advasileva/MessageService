using System.Collections.Generic;

namespace MessageService.Models
{
    public class UserMessage
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}