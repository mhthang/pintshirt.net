using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Data.EntityFramework
{
    public interface IDbFactory : IDisposable
    {
        ISCDataContext Init();
    }
}
