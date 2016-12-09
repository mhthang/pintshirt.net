using StoneCastle.Domain;
using StoneCastle.Messaging.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.Messaging
{
    public interface IMessagingTemplateContentRepository : IRepository<MessagingTemplateContent, Guid>
    {
        List<MessagingTemplateContent> GetTemplateContentTitles(Guid templateId);
        int CountTemplateContent(Guid templateId);
    }
}
