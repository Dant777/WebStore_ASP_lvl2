using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.ViewComponents
{
    public class Brands : ViewComponent
    {
        private readonly IProductService _productService;

        public Brands(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string brandId)
        {
            int.TryParse(brandId, out var brandIdResult);
            var brands = GetBrands();
            return View(new BrandCompleteViewModel
                {
                    Brands = brands,
                    CurrentBrandId = brandIdResult
                });
        }

        private IEnumerable<BrandViewModel> GetBrands()
        {
            var brands = _productService.GetBrands();

            return brands.Select(x => new BrandViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Order = x.Order,
                ProductsCount = 0
            }).OrderBy(x => x.Order)
                .ToList();
        }
    }
}
