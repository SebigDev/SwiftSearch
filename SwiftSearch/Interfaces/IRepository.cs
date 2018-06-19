using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftSearch.Interfaces
{
    public interface IRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAllDataAsync();
        Task<T> FindAsync(object ID);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object ID);
        void Save();

    }
}
