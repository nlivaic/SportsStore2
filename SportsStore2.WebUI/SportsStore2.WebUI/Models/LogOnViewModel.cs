
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore2.WebUI.Models {
    public class LogOnViewModel {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}