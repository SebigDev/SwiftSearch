using SwiftSearch.Data;
using SwiftSearch.Interfaces;
using SwiftSearch.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SwiftSearch.Repository
{
   
    public class FurnitureRepository : IFurnitureRepository
    {
        private SwiftSearchDbContext _context;

        public FurnitureRepository(SwiftSearchDbContext context)
        {
            this._context = context;
        }
        public void Delete(object ID)
        {
            Furniture funiture = _context.Furnitures.Find(ID);
            _context.Furnitures.Remove(funiture);
        }

        public Task<Furniture> FindAsync(object ID)
        {
            return _context.Furnitures.FindAsync(ID);
        }

        public async Task<IEnumerable<Furniture>> GetAllDataAsync()
        {
            var data = await _context.Furnitures.ToListAsync();
            return data;
        }

        public IEnumerable<Furniture> GetFurnituresBySearch()
        {
            var furniture = _context.Furnitures.ToList();
            return furniture;
        }

        public void Insert(Furniture obj)
        {
            _context.Furnitures.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Furniture obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}