using SportsStore2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SportsStore2.Domain.Concrete {
    public class EFDbContext : DbContext {
        public DbSet<Product> Products { get; set; }
    }
}