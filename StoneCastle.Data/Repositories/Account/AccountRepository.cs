using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Account.Repositories
{
    public class AccountRepository : Repository<Account.Models.Account, System.Guid>, IAccountRepository
    {
        public AccountRepository(ISCDataContext context) : base(context)
        {
        }
    }
}
