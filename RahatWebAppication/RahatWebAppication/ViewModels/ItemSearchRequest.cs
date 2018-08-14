using System.Collections.Generic;

namespace RahatWebAppication.ViewModels
{
    public class ItemSearchRequest
    {
        public List<int> CategoryIds { get; set; } 
        public PriceRangeModel PriceRange { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}