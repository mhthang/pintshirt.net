using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;
using StoneCastle.Messaging.Models;

namespace StoneCastle.Messaging.Repositories
{

    public class MessagingDataMappingRepository : Repository<MessagingDataMapping, System.Guid>, IMessagingDataMappingRepository
    {
        public MessagingDataMappingRepository(ISCDataContext context) : base(context)
        {
        }

    }
}
