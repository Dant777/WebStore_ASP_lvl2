using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.ViewComponents
{
    public class Sections : ViewComponent
    {
        private readonly IProductService _productService;

        public Sections(IProductService productService)
        {
            _productService = productService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sections = GetSections();
            return View(sections);
        }

        private List<SectionViewModel> GetSections()
        {
            var categories = _productService.GetSections();

            var parentCategories = categories.Where(x => !x.ParentId.HasValue).ToArray();
            var parentSections = new List<SectionViewModel>();

            // получим и заполним родительские категории
            foreach (var parentCategory in parentCategories)
            {
                parentSections.Add(new SectionViewModel()
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name,
                    Order = parentCategory.Order,
                    ParentSection = null
                });
            }

            // получим и заполним дочерние категории
            foreach (var sectionViewModel in parentSections)
            {
                var childCategories = categories.Where(c => c.ParentId == sectionViewModel.Id);
                foreach (var childCategory in childCategories)
                {
                    sectionViewModel.ChildSections.Add(new SectionViewModel()
                    {
                        Id = childCategory.Id,
                        Name = childCategory.Name,
                        Order = childCategory.Order,
                        ParentSection = sectionViewModel
                    });
                }
                sectionViewModel.ChildSections = sectionViewModel.ChildSections.OrderBy(c => c.Order).ToList();
            }
            parentSections = parentSections.OrderBy(c => c.Order).ToList();

            return parentSections;
        }
    }
}
