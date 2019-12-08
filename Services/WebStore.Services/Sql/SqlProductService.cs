using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
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

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            var products = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .AsQueryable();

            if (filter.SectionId.HasValue)
                products = products.Where(x => x.SectionId == filter.SectionId.Value);
            if (filter.BrandId.HasValue)
                products = products.Where(x => x.BrandId == filter.BrandId.Value);

            return products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
