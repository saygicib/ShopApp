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
                }
                context.SaveChanges();
            }
        }
        private static Category[] Categories =
        {
            new Category(){Name="Telefon"},
            new Category(){Name="Bilgisayar"}
        }; 
        private static Product[] Products =
        {
            new Product(){Name="Samsung A52",Price=5500,ImageUrl="1.jpg"},
            new Product(){Name="Samsung A72",Price=7500,ImageUrl="2.jpg"},
            new Product(){Name="Samsung A54",Price=4500,ImageUrl="3.jpg"},
            new Product(){Name="Samsung A64",Price=8000,ImageUrl="4.jpg"},
            new Product(){Name="Samsung A11",Price=3000,ImageUrl="5.jpg"},
        };        
    }
}
