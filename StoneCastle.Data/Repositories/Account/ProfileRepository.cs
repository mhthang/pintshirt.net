using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Account.Repositories
{
    public class ProfileRepository : Repository<Account.Models.Profile, System.Guid>, IProfileRepository
    {
        public ProfileRepository(ISCDataContext context) : base(context)
        {
        }
    }
}
