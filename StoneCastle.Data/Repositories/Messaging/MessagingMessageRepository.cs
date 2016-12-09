using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;
using StoneCastle.Messaging.Models;

namespace StoneCastle.Messaging.Repositories
{
    public class MessagingMessageRepository : Repository<MessagingMessage, System.Guid>, IMessagingMessageRepository 
    {
        public MessagingMessageRepository(ISCDataContext context) : base(context)
        {
        }

        public int CountMessages()
        {
            int count = 0;
            count = this.GetAll().Count(x => x.Id != null);
            return count;
        }

        public List<MessagingMessage> GetMessageTitles()
        {
            var query = (from m in this.DataContext.Set<MessagingMessage>()
                            join tc in this.DataContext.Set<MessagingTemplateContent>() on m.MessagingTemplateContentId equals tc.Id
                            join t in this.DataContext.Set<MessagingTemplate>() on tc.MessagingTemplateId equals t.Id
                         orderby m.CreatedDate descending
                         select m).Take(20).ToList();

            return query;
        }
    }
}
