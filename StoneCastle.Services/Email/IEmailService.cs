﻿using StoneCastle.Messaging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Email
{
    public interface IEmailService
    {
        Task SendAsync(Guid templateId, Dictionary<string, string> values);
        Task SendAsync(MessagingMessageModel mailMessage);
    }
}
