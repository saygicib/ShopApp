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
    public class EfCartDal : EfCoreGenericRepositoryDal<Cart, Context>, ICartDal
    {
        public Cart GetCartByUserId(string userId)
        {
            using (var context = new Context())
            {
                return context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefault(x=>x.UserId==userId);
            }
        }
    }
}
