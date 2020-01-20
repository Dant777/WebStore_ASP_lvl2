using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.ViewModels;

namespace WebStore.DomainNew.Helper
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(this Product p) =>
            new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                // добавим информацию о бренде, если она есть
                Brand = p.BrandId.HasValue ? new BrandDto { Id = p.Brand.Id, Name = p.Brand.Name } : null,
                Section = new SectionDto
                {
                    Id = p.SectionId,
                    Name = p.Section.Name
                }
            };

        public static ProductDto ToDto(this ProductViewModel p) =>
            new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                // добавим информацию о бренде, если она есть
                Brand = p.BrandId.HasValue ? new BrandDto { Id = p.BrandId.Value, Name = p.BrandName } : null,
                Section = new SectionDto
                {
                    Id = p.SectionId,
                    Name = p.Section
                }
            };

        public static Product ToProduct(this ProductDto productDto) =>
            new Product
            {
                BrandId = productDto.Brand?.Id,
                SectionId = productDto.Section.Id,
                Name = productDto.Name,
                ImageUrl = productDto.ImageUrl,
                Order = productDto.Order,
                Price = productDto.Price
            };

        public static ProductViewModel ToViewModel(this ProductDto product, IEnumerable<Section> sections, IEnumerable<Brand> brands) =>
            new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Section = product.Section.Name,
                SectionId = product.Section.Id,
                BrandName = product.Brand?.Name,
                BrandId = product.Brand?.Id,
                Brands = new SelectList(brands, "Id", "Name", product.Brand?.Id),
                Sections = new SelectList(sections, "Id", "Name", product.Section.Id)
            };

    }
}