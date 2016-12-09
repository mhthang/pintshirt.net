using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;
using StoneCastle.Messaging.Models;

namespace StoneCastle.Messaging.Repositories
{
    public class MessagingTypeRepository : Repository<MessagingType, int>, IMessagingTypeRepository    
    {
        public MessagingTypeRepository(ISCDataContext context) : base(context)
        {
        }
    }
}
