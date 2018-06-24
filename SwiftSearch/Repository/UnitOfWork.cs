using SwiftSearch.Interfaces;
using SwiftSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SwiftSearch.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private SwiftSearchDbContext _context;

        public UnitOfWork(SwiftSearchDbContext context)
        {
            this._context = context;
            Vehicle = new VehicleRepository(_context);
            Furniture = new FurnitureRepository(_context);
        }
        public IVehicleRepository Vehicle { get; protected set; }   

        public IFurnitureRepository Furniture { get; protected set; }

        public void Complete()
        {
           _context.SaveChangesAsync();
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

      

        #endregion


    }
}