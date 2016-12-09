using StoneCastle.Services;
using System.Collections.Generic;

using System;
using StoneCastle.Messaging.Models;

namespace StoneCastle.Messaging.Services
{
    public interface IMessagingMessageService
    {
        MessagingMessageModel GetMailMessage(Guid templateId, Dictionary<string, string> values);
        List<MessagingTemplateModel> GetMessagingTemplates();
        GetMessagingTemplateModel GetMessagingContent();
        List<MessagingMessageModel> GetMessages();
        GetMessageModel GetMessageTitles();
        int CountMessages();

        GetTemplateContentList GetTemplateContentTitles(Guid templateId);
        int CountTemplateContents(Guid templateId);

        MessagingMessageModel GetMailMessage(Guid messageId);
        TemplateContentModel GetMailTemplateContent(Guid contentId);

        TemplateContentModel SaveMailTemplateContent(TemplateContentModel contentDto);
    }
}
