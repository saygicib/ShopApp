using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Abstract
{
    public interface IRepositoryBaseDal<TEntity>
    {
        TEntity GetById(int id);
        TEntity GetOne(Expression<Func<TEntity, bool>> predicate=null);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate=null);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
