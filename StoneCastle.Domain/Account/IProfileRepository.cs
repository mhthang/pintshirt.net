using StoneCastle.Domain;

namespace StoneCastle.Account
{
    public interface IProfileRepository : IRepository<Account.Models.Profile, System.Guid>
    {
    }
}
