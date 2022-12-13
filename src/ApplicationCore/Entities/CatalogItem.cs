using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class CatalogItem : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        //New color attribute added to the catalog item constructor
        public string Color { get; private set; }
        public decimal Price { get; private set; }
        public string PictureUri { get; private set; }

        public int CatalogTypeId { get; private set; }
        public CatalogType CatalogType { get; private set; }

        public int CatalogBrandId { get; private set; }
        public CatalogBrand CatalogBrand { get; private set; }

        //new catalog color id created
        public int CatalogColorId { get; private set; }
        public CatalogColor CatalogColor { get; private set; }

        public CatalogItem(int catalogTypeId,
            int catalogBrandId,
            int catalogColorId,
            string description,
            //Color added in the constructor
            string color,
            string name,
            decimal price,
            string pictureUri)
        {
            CatalogTypeId = catalogTypeId;
            CatalogBrandId = catalogBrandId;
            //catalog color id added to the constructor
            CatalogColorId = catalogColorId;
            Description = description;
            Color = color;
            Name = name;
            Price = price;
            PictureUri = pictureUri;
        }

        //Color added to the update details method
        public void UpdateDetails(string name, string description, string color, decimal price)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NullOrEmpty(description, nameof(description));
            //Check to make sure that color is not null or empty
            Guard.Against.NullOrEmpty(color, nameof(color));
            Guard.Against.NegativeOrZero(price, nameof(price));

            Name = name;
            Description = description;
            Color = color;
            Price = price;
        }

        public void UpdateBrand(int catalogBrandId)
        {
            Guard.Against.Zero(catalogBrandId, nameof(catalogBrandId));
            CatalogBrandId = catalogBrandId;
        }

        public void UpdateType(int catalogTypeId)
        {
            Guard.Against.Zero(catalogTypeId, nameof(catalogTypeId));
            CatalogTypeId = catalogTypeId;
        }

        //added an update color method
        public void UpdateColor(int catalogColorId)
        {
            Guard.Against.Zero(catalogColorId, nameof(catalogColorId));
            CatalogColorId = catalogColorId;
        }

        public void UpdatePictureUri(string pictureName)
        {
            if (string.IsNullOrEmpty(pictureName))
            {
                PictureUri = string.Empty;
                return;
            }
            PictureUri = $"images\\products\\{pictureName}?{new DateTime().Ticks}";
        }
    }
}