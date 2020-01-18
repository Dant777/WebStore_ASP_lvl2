using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.Helper;
using WebStore.Interfaces;

namespace WebStore.Services.Sql
{
    public class SqlProductService : IProductService
    {
        private readonly WebStoreContext _context;

        public SqlProductService(WebStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<Section> GetSections()
        {
            return _context.Sections.ToList();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public PagedProductDto GetProducts(ProductFilter filter)
        {
            var products = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .AsQueryable();

            if (filter.SectionId.HasValue)
                products = products.Where(x => x.SectionId == filter.SectionId.Value);
            if (filter.BrandId.HasValue)
                products = products.Where(x => x.BrandId == filter.BrandId.Value);

            var model = new PagedProductDto { TotalCount = products.Count() };

            if (filter.PageSize != null) // если указан размер страницы
            {
                model.Products = products
                    .Skip((filter.Page - 1) * (int)filter.PageSize)
                    .Take((int)filter.PageSize)
                    .Select(p => p.ToDto())
                    .ToList();
            }
            else // иначе работаем по старой логике
            {
                model.Products = products
                    .Select(p => p.ToDto())
                    .ToList();
            }

            return model;
        }

        public ProductDto GetProductById(int id)
        {
            var product = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return null;

            return product.ToDto();
        }

        public Section GetSectionById(int id)
        {
            return _context.Sections.FirstOrDefault(s => s.Id == id);
        }

        public Brand GetBrandById(int id)
        {
            return _context.Brands.FirstOrDefault(s => s.Id == id);
        }

    }
}
