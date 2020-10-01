using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogNetCore.DataAccess.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;
        internal DbSet<T> dbSet;
        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            this.dbSet.Add(entity);
        }
        public void Update(T entity)
        {
            this.dbSet.Update(entity);
        }
        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            //FIlter
            if (filter != null)
               query= query.Where(filter);
            //Include Properties
            if (includeProperties != null)
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries)) 
                    query = query.Include(includeProperty);
            //Order
            if (orderBy != null)
                return orderBy(query).ToList();
            
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            //FIlter
            if (filter != null)
                query = query.Where(filter);
            //Include Properties
            if (includeProperties != null)
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);
            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
            Remove(entityToRemove);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

      
    }
}
