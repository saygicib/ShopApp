using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfOrderDal : EfCoreGenericRepositoryDal<Order, Context>, IOrderDal
    {
        public List<Order> GetOrders(string userId)
        {
            using (var context = new Context())
            {
                var orders = context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).AsQueryable();//Sorgu henüz Database'e gitmedi.
                if (string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(x => x.UserId == userId);
                }
                return orders.ToList();//Listeye çevirildiği an da sorgu database'e gidiyor.
            }
        }
    }
}
