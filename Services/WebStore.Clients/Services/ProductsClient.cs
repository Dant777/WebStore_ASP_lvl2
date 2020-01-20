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

        public PagedProductDto GetProducts(ProductFilter filter)
        {
            string url = $"{ServiceAddress}";
            var response = Post(url, filter);
            return response.Content.ReadAsAsync<PagedProductDto>().Result;
        }

        public ProductDto GetProductById(int id)
        {
            string url = $"{ServiceAddress}/{id}";
            return Get<ProductDto>(url);
        }

        public Section GetSectionById(int id)
        {
            var url = $"{ServiceAddress}/sections/{id}";
            var result = Get<Section>(url);
            return result;
        }

        public Brand GetBrandById(int id)
        {
            var url = $"{ServiceAddress}/brands/{id}";
            var result = Get<Brand>(url);
            return result;
        }

        public SaveResultDto CreateProduct(ProductDto productDto)
        {
            var url = $"{ServiceAddress}/create";
            var response = Post(url, productDto);
            var result = response.Content.ReadAsAsync<SaveResultDto>().Result;
            return result;
        }

        public SaveResultDto UpdateProduct(ProductDto productDto)
        {
            var url = $"{ServiceAddress}";
            var response = Put(url, productDto);
            var result = response.Content.ReadAsAsync<SaveResultDto>().Result;
            return result;
        }

        public SaveResultDto DeleteProduct(int productId)
        {
            var url = $"{ServiceAddress}/{productId}";
            var response = DeleteAsync(url).Result;
            var result = response.Content.ReadAsAsync<SaveResultDto>().Result;
            return result;
        }

    }
}
