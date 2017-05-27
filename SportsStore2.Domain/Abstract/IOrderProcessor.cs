using SportsStore2.Domain.Entities;

namespace SportsStore2.Domain.Abstract {
    public interface IOrderProcessor {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
