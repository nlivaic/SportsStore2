using SportsStore2.Domain.Entities;

namespace SportsStore2.WebUI.Models {
    public class CartSummaryViewModel {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}