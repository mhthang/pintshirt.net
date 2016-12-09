using StoneCastle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Account.Services
{
    public interface IProfileService : IBaseService
    {
        Models.ProfileModel t();
    }
}
