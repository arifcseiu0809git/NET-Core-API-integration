using DMS.DAL.Repositories.ESSRepository;
using DMS.DAL.Repositories.PromotionRepository;
using System;

namespace DMS.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable {

        //private ESSDBContext context;

        public UnitOfWork()
        {
            //context = new ESSDBContext();
        }
        public IESSRepository ESSRepository
        {
            get { return new ESSRepository(); }

        }
        //public UnitOfWork(ESSDBContext _context)
        //{
        //    this.context = _context;
        //}

        //private ESSRepository _ESSRepository;
        //public ESSRepository ESSRepository
        //{
        //    get
        //    {
        //        if (this._ESSRepository == null)
        //        {
        //            this._ESSRepository = new ESSRepository(context);
        //        }

        //        return _ESSRepository;
        //    }
        //}


        private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                //context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

  }
}
