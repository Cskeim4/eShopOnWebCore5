using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public class CatalogItemViewModelService : ICatalogItemViewModelService
    {
        private readonly IRepository<CatalogItem> _catalogItemRepository;

        public CatalogItemViewModelService(IRepository<CatalogItem> catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }

        public async Task UpdateCatalogItem(CatalogItemViewModel viewModel)
        {
            var existingCatalogItem = await _catalogItemRepository.GetByIdAsync(viewModel.Id);
            //Add color to the update details call 
            existingCatalogItem.UpdateDetails(viewModel.Name, existingCatalogItem.Description, existingCatalogItem.Color, viewModel.Price);
            await _catalogItemRepository.UpdateAsync(existingCatalogItem);
        }
    }
}