using Microsoft.EntityFrameworkCore;
using OnlineGym.Models;

namespace OnlineGym.Repo
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async void Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task<T> Delete(int id)
        {
            var entity = _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return null; // Entity not found
            }

            _context.Set<T>().Remove(entity.Result);
            return entity.Result;
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<T> Update(int id, T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(id);
            if (existingEntity == null)
            {
                return null; // Entity not found
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            return existingEntity;
        }

    }
}
