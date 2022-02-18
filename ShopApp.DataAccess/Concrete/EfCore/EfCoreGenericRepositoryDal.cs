using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreGenericRepositoryDal<TEntity, TContext> : IRepositoryBaseDal<TEntity> where TEntity : class where TContext : DbContext, new()
    {
        public virtual void Create(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        public virtual void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();
            }
        }

        public virtual List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate=null)
        {
            using (var context = new TContext())
            {
                return predicate == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(predicate).ToList();
            }
        }

        public virtual TEntity GetById(int id)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().Find(id);
            }
        }

        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> predicate=null)
        {
            using (var context = new TContext())
            {
                return predicate == null
                    ? context.Set<TEntity>().SingleOrDefault()
                    : context.Set<TEntity>().Where(predicate).SingleOrDefault();
            }
        }

        public virtual void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Update(entity).State = EntityState.Modified;
                context.SaveChanges();

            }
        }
    }
}
