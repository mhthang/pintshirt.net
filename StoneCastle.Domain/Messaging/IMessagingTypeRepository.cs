using StoneCastle.Domain;
using StoneCastle.Messaging.Models;
using System;
using System.Collections.Generic;

namespace StoneCastle.Messaging
{
    public interface IMessagingTypeRepository : IRepository<MessagingType, int>
    {
    }
}
