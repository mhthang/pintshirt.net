using StoneCastle.Services;
using System.Collections.Generic;

using System;

namespace StoneCastle.Messaging.Services
{
    public interface IMessagingDatabindingHelperService : IBaseService
    {
        String bind(String source, Dictionary<string, string> values);
    }
}
