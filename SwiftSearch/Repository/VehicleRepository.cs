using System;
using System.Web;
using System.Data.Entity;
using System.Linq;
using SwiftSearch.Data;
using SwiftSearch.Interfaces;
using SwiftSearch.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace SwiftSearch.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private SwiftSearchDbContext _context;
        
        public VehicleRepository(SwiftSearchDbContext context)
        {
            this._context = context;
            //this._dbSet = context.Set<Vehicle>();
        }

        public IEnumerable<Vehicle> GetVehiclesBySearch()
        {
            var vehicle = _context.Vehicles.ToList();
           // var veh = _dbSet.ToList();
            return vehicle;
                                            

        }

        public void Delete(object ID)
        {
           Vehicle vehicle = _context.Vehicles.Find(ID);
            _context.Vehicles.Remove(vehicle);
        }

        public Task<Vehicle> FindAsync(object ID)
        {
            return _context.Vehicles.FindAsync(ID); 
        }

        public async Task<IEnumerable<Vehicle>> GetAllDataAsync()
        {
            var data = await _context.Vehicles.ToListAsync();
            return data;
        }

       

        public void Insert(Vehicle obj)
        {
            _context.Vehicles.Add(obj);
         
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Vehicle obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }

        public async Task<Vehicle> MakeVehiclePayment()
        {
            var pay = await _context.Vehicles.FirstOrDefaultAsync();
            var model = new Vehicle
            {
                ID = pay.ID,
                CarName = pay.CarName,
                CarAddress = pay.CarAddress,
                CarColor = pay.CarColor,
                CarDealer = pay.CarDealer,
                CarPrice = pay.CarPrice,
                CarModel = pay.CarModel,
                CarImage = pay.CarImage
            };
            return model;

        }
    }
}