using System.Collections.Generic;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;

namespace WebStore.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        PagedProductDto GetProducts(ProductFilter filter);

        /// <summary>
        /// Получить товар по Id
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сущность Product, если нашел, иначе null</returns>
        ProductDto GetProductById(int id);

        /// <summary>Секция по Id</summary>
        /// <param name="id">Id</param>
        Section GetSectionById(int id);

        /// <summary>Бренд по Id</summary>
        /// <param name="id">Id</param>
        Brand GetBrandById(int id);

        /// <summary>Создать продукт</summary>
        /// <param name="product">Сущность Product</param>
        SaveResultDto CreateProduct(ProductDto product);

        /// <summary>Обновить продукт</summary>
        /// <param name="product">Сущность Product</param>
        SaveResultDto UpdateProduct(ProductDto product);

        /// <summary>Удалить продукт</summary>
        /// <param name="productId">Id продукта</param>
        SaveResultDto DeleteProduct(int productId);
    }
}