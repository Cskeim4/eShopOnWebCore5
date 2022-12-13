using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class CatalogIndexViewModel
    {
        public List<CatalogItemViewModel> CatalogItems { get; set; }
        public List<SelectListItem> Brands { get; set; }
        public List<SelectListItem> Types { get; set; }
        //add a new list for colors
        public List<SelectListItem> Colors { get; set; }
        public int? BrandFilterApplied { get; set; }
        public int? TypesFilterApplied { get; set; }
        //add a new filter for color
        public int? ColorFilterApplied { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}
