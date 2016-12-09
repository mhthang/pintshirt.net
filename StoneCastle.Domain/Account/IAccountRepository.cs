using StoneCastle.Domain;

namespace StoneCastle.Account
{
    public interface IAccountRepository : IRepository<Account.Models.Account, System.Guid>
    {
    }
}
