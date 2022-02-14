using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Abstract
{
    public interface IValidator<TEntity>
    {
        string ErrorMessage { get; set; }
        bool Validate(TEntity entity);
    }
}
