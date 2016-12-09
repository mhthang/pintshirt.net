using StoneCastle.Domain;
using StoneCastle.Messaging.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.Messaging
{
    public interface IMessagingTemplateRepository : IRepository<MessagingTemplate, Guid>
    {
        List<MessagingTemplate> GetMessageTemplates(int type);
    }
}
