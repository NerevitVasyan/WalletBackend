using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpendingsWebAPI.DAL.Abstraction
{
    public interface IRepos<T> where T: class
    {
        void Add(T entity);
        IQueryable<T> GetAll();
    }
}
