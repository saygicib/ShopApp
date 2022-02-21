using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Abstract
{
    public interface IOrderDal : IRepositoryBaseDal<Order>
    {
        List<Order> GetOrders(string userId);
    }
}
