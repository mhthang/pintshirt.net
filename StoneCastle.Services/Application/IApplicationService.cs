using StoneCastle.Account.Models;
using StoneCastle.Common.Models;
using StoneCastle.Organization.Models;
using StoneCastle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Application.Services
{
    public interface IApplicationService : IBaseService
    {
        List<Application.Models.CountryModel> GetCountries();
        List<Application.Models.TimezoneModel> GetTimezones();
    }
}
