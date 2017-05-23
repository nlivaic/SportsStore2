using SportsStore2.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore2.Domain.Entities {
    public class Cart {
        public List<CartItem> Items;
        private IProductRepository repository;

        public Cart(IProductRepository repo) {
            Items = new List<CartItem>();
            repository = repo;
        }

        public void AddToCart(int productId) {
            CartItem item = Items.Where(c => c.Item.ProductId == productId).FirstOrDefault();
            Product product = null;
            // If item already exists, simply advance the counter.
            if (item != null) {
                item.Quantity++;
            } else {        // Item doesn't exists, we have to add it to the collection.
                // But first we have to check if the product exists in the repository.
                product = repository.Products.Where(p => p.ProductId == productId).FirstOrDefault();
                if (product != null) {
                    Items.Add(new CartItem {
                        Item = product,
                        Quantity = 1
                    });
                }
            }
        }

        public void RemoveFromCart(int productId) {
            CartItem item = Items.Where(p => p.Item.ProductId == productId).FirstOrDefault();
            // We can only remove items that were in the cart in the first place.
            if (item != null) {
                Items.Remove(item);
            }
        }
        
        public void Clear() {
            Items.Clear();
        }

        public decimal GetTotalValue() {
            return Items.Sum(p => p.Item.Price * p.Quantity);
        }
    }
}