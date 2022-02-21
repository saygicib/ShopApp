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
        public void ClearCart(int cartId)
        {
            using (var context = new Context())
            {
                var command = @"delete from CartItems where CartId=@p0";
                context.Database.ExecuteSqlRaw(command, cartId);
            }
        }

        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context = new Context())
            {
                var command = @"delete from CartItems where CartId=@p0 and ProductId=@p1";
                context.Database.ExecuteSqlRaw(command,cartId,productId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            using (var context = new Context())
            {
                return context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefault(x=>x.UserId==userId);
            }
        }
        public override void Update(Cart entity)
        {
            using (var context = new Context())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
