using SportsStore2.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore2.Domain.Entities {
    public class Cart {
        public IEnumerable<CartItem> Items { get { return CartItems; } }
        private List<CartItem> CartItems;

        public Cart() {
            CartItems = new List<CartItem>();
        }

        public void AddToCart(Product product) {
            CartItem item = CartItems.Where(c => c.Item.ProductId == product.ProductId).FirstOrDefault();
            // If item already exists, simply advance the counter.
            if (item != null) {
                item.Quantity++;
            } else {        // Item doesn't exists, we have to add it to the collection.
                CartItems.Add(new CartItem {
                    Item = product,
                    Quantity = 1
                });
            }
        }

        public void RemoveFromCart(Product product) {
            CartItems.RemoveAll(i => i.Item.ProductId == product.ProductId);
        }
        
        public void Clear() {
            CartItems.Clear();
        }

        public decimal GetTotalValue() {
            return CartItems.Sum(p => p.Item.Price * p.Quantity);
        }
    }
}