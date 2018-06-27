using SwiftSearch.Interfaces;
using SwiftSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SwiftSearch.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private SwiftSearchDbContext _context;

        public UnitOfWork(SwiftSearchDbContext context)
        {
            this._context = context;
            VehicleRepo = new VehicleRepository(_context);
            FurnitureRepo = new FurnitureRepository(_context);
        }
        public IVehicleRepository VehicleRepo { get; protected set; }   

        public IFurnitureRepository FurnitureRepo { get; protected set; }

        public void Complete()
        {
           _context.SaveChangesAsync();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}