using System;
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
                .Where(p => !p.IsDeleted)
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

        public SaveResultDto CreateProduct(ProductDto product)
        {
            try
            {
                _context.Products.Add(product.ToProduct());
                _context.SaveChanges();

                return new SaveResultDto(true);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new SaveResultDto(false, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return new SaveResultDto(false, ex.Message);
            }
            catch (Exception ex)
            {
                return new SaveResultDto(false, ex.Message);
            }
        }

        public SaveResultDto UpdateProduct(ProductDto productDto)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productDto.Id);
            if (product == null)
            {
                return new SaveResultDto(false, "Entity does not exist");
            }

            // скопируем все поля модели
            product.BrandId = productDto.Brand.Id;
            product.SectionId = productDto.Section.Id;
            product.ImageUrl = productDto.ImageUrl;
            product.Order = productDto.Order;
            product.Price = productDto.Price;
            product.Name = productDto.Name;

            try
            {
                _context.SaveChanges();
                return new SaveResultDto(true);
            }
            catch (Exception ex)
            {
                return new SaveResultDto(false, ex.Message);
            }
        }

        public SaveResultDto DeleteProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return new SaveResultDto(false, "Entity does not exist");
            }

            try
            {
                product.IsDeleted = true;
                _context.SaveChanges();

                return new SaveResultDto(true);
            }
            catch (Exception ex)
            {
                return new SaveResultDto(false, ex.Message);
            }
        }
    }
}
