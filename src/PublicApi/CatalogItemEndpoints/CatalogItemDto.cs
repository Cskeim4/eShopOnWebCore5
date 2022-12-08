namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints
{
    public class CatalogItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //Add color to the catalog item dto declaration
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }
    }
}
