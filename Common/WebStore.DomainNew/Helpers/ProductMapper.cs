using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;

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
                Brand = p.BrandId.HasValue ? new BrandDto { Id = p.Brand.Id, Name = p.Brand.Name } : null
            };
    }
}
