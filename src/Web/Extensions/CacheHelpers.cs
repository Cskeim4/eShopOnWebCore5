using System;

namespace Microsoft.eShopWeb.Web.Extensions
{
    public static class CacheHelpers
    {
        public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromSeconds(30);
        private static readonly string _itemsKeyTemplate = "items-{0}-{1}-{2}-{3}-{4}";

        //add color to the item cache key generation method
        public static string GenerateCatalogItemCacheKey(int pageIndex, int itemsPage, int? brandId, int? typeId, int? colorId)
        {
            return string.Format(_itemsKeyTemplate, pageIndex, itemsPage, brandId, typeId, colorId);
        }

        public static string GenerateBrandsCacheKey()
        {
            return "brands";
        }

        public static string GenerateTypesCacheKey()
        {
            return "types";
        }

        //New cache helper for colors
        public static string GenerateColorsCacheKey()
        {
            return "colors";
        }
    }
}