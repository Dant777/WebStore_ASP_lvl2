using System.Collections.Generic;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;

namespace WebStore.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter filter);

        /// <summary>
        /// Получить товар по Id
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сущность Product, если нашел, иначе null</returns>
        Product GetProductById(int id);
    }
}