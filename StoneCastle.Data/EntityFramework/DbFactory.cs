using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace StoneCastle.Data.EntityFramework
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ISCDataContext _context;
        public DbFactory(ISCDataContext context)
        {
            this._context = context;
        }

        public DbFactory()
        {

        }
        
        public ISCDataContext Init()
        {
            return _context ?? (_context = new SCDataContext(StoneCastle.Commons.Constants.ENTITY_FRAMEWORK_CONNECTION_STRING));
        }


        protected override void DisposeCore()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}
