using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore2.WebUI.Models {
    public class CategoryViewModel {
        public IEnumerable<string> Categories { get; set; }
        public string ChosenCategory { get; set; }
    }
}