using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;
using StoneCastle.Messaging.Models;

namespace StoneCastle.Messaging.Repositories
{
    public class MessagingTemplateRepository : Repository<MessagingTemplate, System.Guid>, IMessagingTemplateRepository    
    {
        public MessagingTemplateRepository(ISCDataContext context) : base(context)
        {
        }

        public List<MessagingTemplate> GetMessageTemplates(int type)
        {
            List<MessagingTemplate> templates = this.GetAll().Where(x => x.IsPublish == true && x.MessagingTypeId == type).ToList();

            return templates;
        }
    }
}
