using SportsStore2.Domain.Entities;

namespace SportsStore2.Domain.Abstract {
    interface IOrderProcessor {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
