using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Shop.Core;
using Shop.Data;

namespace Shop.Service
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly DatabaseContext _dbcontext;
        private readonly DbSet<T> _dbset;
        public Service(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
            _dbset = _dbcontext.Set<T>();
        }
        //Getir
        public List<T> GetAll()
        {
            return _dbset.ToList();
        }
        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbset.Where(expression).ToList();
        }
        public T Get(Expression<Func<T, bool>> expression)
        {
            return _dbset.FirstOrDefault(expression);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbset.Where(expression).ToListAsync();
        }
        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return _dbset.FirstOrDefaultAsync(expression);
        }

        //Ekle
        public void add(T entity)
        {
            _dbset.Add(entity);
        }
        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }
        //Bul
        public T Find(int id)
        {
            return _dbset.Find(id);
        }
        public async Task<T> FindAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }
        //Silme işlemi için
        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }
        //Gücelleme Kayıt
        public void Update(T entity)
        {
            _dbset.Update(entity);
        }

        public int Save()
        {
            return _dbcontext.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _dbcontext.SaveChangesAsync();
        }
    }
}
