namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints
{
    public class CreateCatalogItemRequest : BaseRequest 
    {
        public int CatalogBrandId { get; set; }
        public int CatalogTypeId { get; set; }
        public string Description { get; set; }
        //Add color to the create catalog item request
        public string Color { get; set; }
        public string Name { get; set; }
        public string PictureUri { get; set; }
        public string PictureBase64 { get; set; }
        public string PictureName { get; set; }
        public decimal Price { get; set; }
    }

}
