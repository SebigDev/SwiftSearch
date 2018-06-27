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
       IVehicleRepository VehicleRepo { get; }
        IFurnitureRepository FurnitureRepo { get; }
        void Complete();
    }
}
