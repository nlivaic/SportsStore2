using System;

namespace SportsStore2.WebUI.Models {
    /// <summary>
    /// Represents basic data for easier paging representation.
    /// </summary>
    public class PagingInfo {
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public decimal NrOfPages {
            get {
                return Math.Ceiling((decimal)TotalItems / PageSize);
            }
        }
        public int CurrentPage { get; set; }
    }
}