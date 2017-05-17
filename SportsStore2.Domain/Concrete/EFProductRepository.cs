﻿using SportsStore2.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore2.Domain.Entities;

namespace SportsStore2.Domain.Concrete {
    public class EFProductRepository : IProductRepository {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products {
            get { return context.Products;  }
        }
    }
}