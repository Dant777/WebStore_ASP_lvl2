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

        public IEnumerable<ProductDto> GetProducts(ProductFilter filter)
        {
            var products = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .AsQueryable();

            if (filter.SectionId.HasValue)
                products = products.Where(x => x.SectionId == filter.SectionId.Value);
            if (filter.BrandId.HasValue)
                products = products.Where(x => x.BrandId == filter.BrandId.Value);

            return products
                .Select(p => p.ToDto())
                .ToList();
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
    }
}
