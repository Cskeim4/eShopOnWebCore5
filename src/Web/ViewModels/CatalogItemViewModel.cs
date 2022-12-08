namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class CatalogItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //Add color to the catalog item view model
        public string Color { get; set; }
        public string PictureUri { get; set; }
        public decimal Price { get; set; }
    }
}