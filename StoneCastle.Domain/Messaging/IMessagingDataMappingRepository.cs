using StoneCastle.Domain;
using StoneCastle.Messaging.Models;
using System;

namespace StoneCastle.Messaging
{
    public interface IMessagingDataMappingRepository : IRepository<MessagingDataMapping, Guid>
    {
    }
}
