using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class CatalogFilterPaginatedSpecification : Specification<CatalogItem>
    {
        //add color to the catalog filer specification and query
        public CatalogFilterPaginatedSpecification(int skip, int take, int? brandId, int? typeId, int? colorId)
            : base()
        {
            if (take == 0)
            {
                take = int.MaxValue;
            }
            Query
                .Where(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
                (!typeId.HasValue || i.CatalogTypeId == typeId) &&
                (!colorId.HasValue || i.CatalogColorId == colorId))
                .Skip(skip).Take(take);
        }
    }
}
