using StoneCastle.Messaging.Models;
using System.Threading.Tasks;

namespace StoneCastle.Email
{
    public interface ISendMailProvider
    {
        Task SendAsync(MessagingMessageModel mailMessage);
    }
}
