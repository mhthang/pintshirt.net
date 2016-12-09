using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneCastle.Domain;
using StoneCastle.Messaging.Services;

namespace StoneCastle.Email
{
    public class WorkingEmailService : EmailServiceBase, IWorkingEmailService
    {
        public WorkingEmailService(ISendMailProvider provider, IMessagingMessageService msgService, IUnitOfWork unitOfWork) : base(provider, msgService, unitOfWork)
        {
        }

        public async Task SendTestEmail(Guid emailTemplateId, Dictionary<string, string> values)
        {
            Logger.Debug($"Sending Email Template *{emailTemplateId}*");

            if (emailTemplateId == null || emailTemplateId == Guid.Empty)
                throw new ArgumentNullException("emailTemplateId");

            Logger.Debug($"Sending Email Template *{emailTemplateId}*");

            await this.SendAsync(emailTemplateId, values);

        }

    }
}
