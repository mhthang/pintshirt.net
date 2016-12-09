using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Email
{
    public interface IWorkingEmailService : IEmailService
    {
        Task SendTestEmail(Guid emailTemplateId, Dictionary<string, string> values);
    }
}
