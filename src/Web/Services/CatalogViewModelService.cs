using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    /// <summary>
    /// This is a UI-specific service so belongs in UI project. It does not contain any business logic and works
    /// with UI-specific types (view models and SelectListItem types).
    /// </summary>
    public class CatalogViewModelService : ICatalogViewModelService
    {
        private readonly ILogger<CatalogViewModelService> _logger;
        private readonly IRepository<CatalogItem> _itemRepository;
        private readonly IRepository<CatalogBrand> _brandRepository;
        private readonly IRepository<CatalogType> _typeRepository;
        //create a new repository for the catalog colors
        private readonly IRepository<CatalogColor> _colorRepository;
        private readonly IUriComposer _uriComposer;

        //add color to the catalog view model service constructor
        public CatalogViewModelService(
            ILoggerFactory loggerFactory,
            IRepository<CatalogItem> itemRepository,
            IRepository<CatalogBrand> brandRepository,
            IRepository<CatalogType> typeRepository,
            IRepository<CatalogColor> colorRepository,
            IUriComposer uriComposer)
        {
            _logger = loggerFactory.CreateLogger<CatalogViewModelService>();
            _itemRepository = itemRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _colorRepository = colorRepository;
            _uriComposer = uriComposer;
        }

        //add color to the get catalog items method
        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId, int? colorId)
        {
            _logger.LogInformation("GetCatalogItems called.");

            var filterSpecification = new CatalogFilterSpecification(brandId, typeId, colorId);
            var filterPaginatedSpecification =
                new CatalogFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage, brandId, typeId, colorId);

            // the implementation below using ForEach and Count. We need a List.
            var itemsOnPage = await _itemRepository.ListAsync(filterPaginatedSpecification);
            var totalItems = await _itemRepository.CountAsync(filterSpecification);

            //Add color to the catalog item attributes that will be copied over to the catalog index view model
            var vm = new CatalogIndexViewModel()
            {
                CatalogItems = itemsOnPage.Select(i => new CatalogItemViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    //Add color to the catalog attributes
                    Color = i.Color,
                    PictureUri = _uriComposer.ComposePicUri(i.PictureUri),
                    Price = i.Price
                }).ToList(),
                Brands = (await GetBrands()).ToList(),
                Types = (await GetTypes()).ToList(),
                Colors = (await GetColors()).ToList(),
                BrandFilterApplied = brandId ?? 0,
                TypesFilterApplied = typeId ?? 0,
                //add the color filter 
                ColorFilterApplied = colorId ?? 0,
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = itemsOnPage.Count,
                    TotalItems = totalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return vm;
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            _logger.LogInformation("GetBrands called.");
            var brands = await _brandRepository.ListAsync();

            var items = brands
                .Select(brand => new SelectListItem() { Value = brand.Id.ToString(), Text = brand.Brand })
                .OrderBy(b => b.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            _logger.LogInformation("GetTypes called.");
            var types = await _typeRepository.ListAsync();

            var items = types
                .Select(type => new SelectListItem() { Value = type.Id.ToString(), Text = type.Type })
                .OrderBy(t => t.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        //create a get colors method to get the colors from the catalog list
        public async Task<IEnumerable<SelectListItem>> GetColors()
        {
            _logger.LogInformation("GetColors called.");
            var colors = await _colorRepository.ListAsync();

            var items = colors
                .Select(color => new SelectListItem() { Value = color.Id.ToString(), Text = color.Color })
                .OrderBy(c => c.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }
    }
}