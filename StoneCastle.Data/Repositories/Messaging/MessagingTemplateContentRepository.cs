﻿using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;
using StoneCastle.Messaging.Models;

namespace StoneCastle.Messaging.Repositories
{
    public class MessagingTemplateContentRepository : Repository<MessagingTemplateContent, System.Guid>, IMessagingTemplateContentRepository    
    {
        public MessagingTemplateContentRepository(ISCDataContext context) : base(context)
        {
        }

        public List<MessagingTemplateContent> GetTemplateContentTitles(Guid templateId)
        {
            var query = (from tc in this.DataContext.Set<Models.MessagingTemplateContent>()
                         join t in this.DataContext.Set<Models.MessagingTemplate>() on tc.MessagingTemplateId equals t.Id
                         where tc.MessagingTemplateId == templateId
                         orderby tc.CreatedDate descending
                         select tc ).Take(20).ToList();
            return query;
        }

        public int CountTemplateContent(Guid templateId)
        {
            int count = 0;
            count = this.GetAll().Count(x => x.MessagingTemplateId == templateId);
            return count;
        }

    }
}
