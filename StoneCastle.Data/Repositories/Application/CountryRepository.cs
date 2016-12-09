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
    public class CountryRepository : Repository<Country, System.Int32>, ICountryRepository
    {
        public CountryRepository(ISCDataContext context) : base(context)
        {
        }
    }
}
