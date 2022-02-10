using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new Context();
            if(context.Database.GetPendingMigrations().Count()==0)
            {
                if(context.Categories.Count()==0)
                {
                    context.Categories.AddRange(Categories);
                }
                if(context.Products.Count()==0)
                {
                    context.Products.AddRange(Products);
                    context.AddRange(ProductCategory);

                }
                context.SaveChanges();
            }
        }
        private static Category[] Categories =
        {
            new Category(){Name="Telefon"},
            new Category(){Name="Bilgisayar"},
            new Category(){Name="Elektronik"}
        }; 
        private static Product[] Products =
        {
            new Product(){Name="Samsung A52",Price=5500,ImageUrl="s1.jpg",Description="asdasdfasdga dfa sdgasdf"},
            new Product(){Name="Samsung A72",Price=7500,ImageUrl="s2.jpg",Description="asdasasdfas dfa sdgasdf"},
            new Product(){Name="Samsung A54",Price=4500,ImageUrl="s3.jpg",Description="asdasdfdfasdsdgasdf"},
            new Product(){Name="Samsung A64",Price=8000,ImageUrl="s4.jpg",Description="asdasdfasfasdfassdf"},
            new Product(){Name="Samsung A11",Price=3000,ImageUrl="s5.jpg",Description="asdasdfasddfasdfasdfasdgasdrfqawdf"},
        };
        private static ProductCategory[] ProductCategory =
        {
            new ProductCategory(){Product=Products[0],Category = Categories[0] },
            new ProductCategory(){Product=Products[0],Category = Categories[2] },
            new ProductCategory(){Product=Products[1],Category = Categories[1] },
            new ProductCategory(){Product=Products[1],Category = Categories[0] },
            new ProductCategory(){Product=Products[2],Category = Categories[2] },
            new ProductCategory(){Product=Products[2],Category = Categories[1] },
            new ProductCategory(){Product=Products[3],Category = Categories[1] },
            new ProductCategory(){Product=Products[4],Category = Categories[2] },
        };
    }
}
