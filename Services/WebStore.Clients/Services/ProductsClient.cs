using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
using WebStore.Interfaces;

namespace WebStore.Clients.Services
{
    public class ProductsClient : BaseClient, IProductService
    {
        protected override string ServiceAddress { get; } = "api/products";

        public ProductsClient(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Section> GetSections()
        {
            string url = $"{ServiceAddress}/sections";
            return Get<List<Section>>(url);
        }

        public IEnumerable<Brand> GetBrands()
        {
            string url = $"{ServiceAddress}/brands";
            return Get<List<Brand>>(url);
        }

        public IEnumerable<ProductDto> GetProducts(ProductFilter filter)
        {
            string url = $"{ServiceAddress}";
            var response = Post(url, filter);
            return response.Content.ReadAsAsync<IEnumerable<ProductDto>>().Result;
        }

        public ProductDto GetProductById(int id)
        {
            string url = $"{ServiceAddress}/{id}";
            return Get<ProductDto>(url);
        }
    }
}
