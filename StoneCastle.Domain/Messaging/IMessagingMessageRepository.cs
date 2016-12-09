using StoneCastle.Domain;
using StoneCastle.Messaging.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.Messaging
{
    public interface IMessagingMessageRepository : IRepository<MessagingMessage, Guid>
    {
        List<MessagingMessage> GetMessageTitles();
        int CountMessages();
    }
}
