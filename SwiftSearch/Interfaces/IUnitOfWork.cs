using SwiftSearch.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftSearch.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
       IVehicleRepository Vehicle { get; }
        IFurnitureRepository Furniture { get; }
        void Complete();
    }
}
