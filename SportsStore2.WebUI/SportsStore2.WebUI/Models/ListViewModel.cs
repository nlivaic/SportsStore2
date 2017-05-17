using SportsStore2.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore2.WebUI.Models {
    public class ListViewModel {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}