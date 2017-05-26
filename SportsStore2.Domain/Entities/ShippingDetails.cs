using System.ComponentModel.DataAnnotations;

namespace SportsStore2.Domain.Entities {
    public class ShippingDetails {
        [Required(ErrorMessage = "Please enter first name.")]
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name.")]
        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter address line.")]
        [Display(Name = "Line 1: ")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Line 2: ")]
        public string AddressLine2 { get; set; }
        [Display(Name = "Line 3: ")]
        public string AddressLine3 { get; set; }
        [Required(ErrorMessage = "Please enter address line.")]
        [RegularExpression(@"[a-zA-Z0-9][a-zA-Z0-9.]*@[a-zA-Z0-9]{3,}\.[a-zA-Z]{2,}")]
        public string Email { get; set; }
        [Display(Name = "Gift wrap: ")]
        public bool GiftWrap { get; set; }
    }
}