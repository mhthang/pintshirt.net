using StoneCastle.Data.EntityFramework;
using StoneCastle.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoneCastle.Application.Models;

namespace StoneCastle.Application.Repositories
{
    public class TimezoneRepository : Repository<Timezone, System.Int32>, ITimezoneRepository
    {
        public TimezoneRepository(ISCDataContext context) : base(context)
        {
        }
    }
}
