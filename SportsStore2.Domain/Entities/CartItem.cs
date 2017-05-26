using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore2.Domain.Entities {
    public class CartItem {
        public Product Item { get; set; }
        public int Quantity { get; set; }
    }
}