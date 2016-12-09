using System.Collections.Generic;
using System.Security.Claims;
using log4net;
using StoneCastle.Domain;
using StoneCastle.Commons;
using StoneCastle.Common.Models;

namespace StoneCastle.Services
{
    public abstract class BaseService : DisposableObject, IBaseService
    {
        public ILog Logger { get; set; }

        protected IUnitOfWork UnitOfWork { get; set; }

        private BaseService()
        {

        }

        protected BaseService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork; 
        }

        #region Protected Methods
        protected Pager GetDefaultPager()
        {
            Pager pager = new Pager()
            {
                PageIndex = 0,
                PageSize = 9999
            };

            return pager;
        }
        #endregion

        #region Dispose
        private bool _disposed;

        protected override void Dispose(bool isDisposing)
        {
            if (!this._disposed)
            {
                if (isDisposing)
                {
                    UnitOfWork = null;
                }
                _disposed = true;
            }            
        }
        #endregion
    }
}
