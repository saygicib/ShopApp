﻿using ShopApp.DataAccess.Abstract;
using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfOrderDal : EfCoreGenericRepositoryDal<Order,Context>,IOrderDal
    {
    }
}
