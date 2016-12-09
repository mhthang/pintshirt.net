using System;
using System.Collections.Generic;

namespace StoneCastle.Messaging.Models
{
    public class GetMessageModel
    {
        public int Total { get; set; }
        public List<MessagingMessageModel> Messages { get; set; }
    }
}
