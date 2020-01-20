using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.Helper;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList()
        {
            var products = _productService.GetProducts(new ProductFilter());
            return View(products);
        }

        public IActionResult Edit(int? id)
        {
            var notParentSections = _productService
                .GetSections()
                .Where(s => s.ParentId != null);
            var brands = _productService.GetBrands();

            // выполним проверки...
            if (id is null)
                return View(new ProductViewModel
                {
                    Sections = new SelectList(notParentSections, "Id", "Name"),
                    Brands = new SelectList(brands, "Id", "Name")
                });

            var product = _productService.GetProductById(id.Value);
            if (product is null)
                return NotFound();

            return View(product.ToViewModel(notParentSections, brands));
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            var notParentSections = _productService
                .GetSections()
                .Where(s => s.ParentId != null);
            var brands = _productService.GetBrands();

            if (ModelState.IsValid)
            {
                if (model.Id >0)
                {
                    _productService.UpdateProduct(model.ToDto());
                }
                else
                {
                    _productService.CreateProduct(model.ToDto());
                }

                return RedirectToAction(nameof(ProductList));
            }

            model.Brands = new SelectList(brands, "Id", "Name", model.BrandId);
            model.Sections = new SelectList(notParentSections, "Id", "Name", model.SectionId);

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound();

            return View(product.ToViewModel(new List<Section>(), new List<Brand>()));
        }

        [HttpPost]
        public IActionResult Delete(ProductViewModel model)
        {
            _productService.DeleteProduct(model.Id);
            return RedirectToAction(nameof(ProductList));
        }

    }
}
