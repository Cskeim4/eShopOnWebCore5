using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    //new catalog color class
    public class CatalogColor : BaseEntity, IAggregateRoot
    {
        public string Color { get; private set; }
        public CatalogColor(string color)
        {
            Color = color;
        }
    }
}