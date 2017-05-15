using SportsStore2.Domain.Entities;
using System.Linq;

namespace SportsStore2.Domain.Abstract {
    public interface IProductRepository {
        IQueryable<Product> Products { get; }
    }
}