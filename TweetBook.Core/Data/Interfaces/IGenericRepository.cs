using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Core.Data
{
    public interface IGenericRepository<T> where T : class
    {
        

        Task<T> GetByIdAsync(int Id);
        Task<IEnumerable<T>> GetAllAsync();

        Task<int> AddAsync(T item);
        Task<int> DeleteByIdAsync(int Id);
        Task<int> UpdateAsync(T item);
    }
}
