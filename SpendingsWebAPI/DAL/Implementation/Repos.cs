using Microsoft.EntityFrameworkCore;
using SpendingsWebAPI.DAL.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpendingsWebAPI.DAL.Implementation
{
    public class Repos<T> : IRepos<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _set;

        public Repos(DbContext dbContext)
        {
            _dbContext = dbContext;
            _set = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _set.Add(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _set.AsQueryable();
        }
    }
}
