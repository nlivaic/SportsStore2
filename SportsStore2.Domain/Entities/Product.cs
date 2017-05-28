using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore2.Domain.Entities {
    public class Product {
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Please enter product name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter product description.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter product category.")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Please enter product price.")]
        [Range(0.00, double.MaxValue, ErrorMessage = "Please enter a valid price amount.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}