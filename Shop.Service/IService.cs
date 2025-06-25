using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service
{
    public interface IService<T>
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression);
        void add(T entity);
        T Find(int id);
        T Get(Expression<Func<T, bool>> expression);
        void Update(T entity);
        void Delete(T entity);
        int Save();

        //Async
        Task AddAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<int> SaveAsync();
        Task<T> FindAsync(int id);
    }
}
