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


        public async Task<IViewComponentResult> InvokeAsync(string sectionId)
        {
            int.TryParse(sectionId, out var sectionIdInt);

            var sections = GetSections(sectionIdInt, out var parentSectionId);
            return View(new SectionCompleteViewModel
            {
                Sections = sections,
                CurrentSectionId = sectionIdInt,
                CurrentParentSectionId = parentSectionId

            });
        }

        /// <summary>
        /// Получает секции из базы и строит дерево
        /// </summary>
        /// <returns></returns>
        private List<SectionViewModel> GetSections(int? sectionId, out int? parentSectionId)
        {
            parentSectionId = null;

            var allSections = _productService.GetSections();
            var parentCategories = allSections.Where(p =>!p.ParentId.HasValue).ToArray();

            var parentSections = new List<SectionViewModel>();

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

            foreach (var sectionViewModel in parentSections)
            {
                var childCategories = allSections.Where(c =>
                    c.ParentId.Equals(sectionViewModel.Id));
                foreach (var childCategory in childCategories)
                {
                    if (childCategory.Id == sectionId)
                        parentSectionId = sectionViewModel.Id;
                    sectionViewModel.ChildSections.Add(new SectionViewModel()
                    {
                        Id = childCategory.Id,
                        Name = childCategory.Name,
                        Order = childCategory.Order,
                        ParentSection = sectionViewModel
                    });
                }
                sectionViewModel.ChildSections =
                    sectionViewModel.ChildSections.OrderBy(c => c.Order).ToList();
            }
            parentSections = parentSections.OrderBy(c => c.Order).ToList();
            return parentSections;
        }

    }
}
