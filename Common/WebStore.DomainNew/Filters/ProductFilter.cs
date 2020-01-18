using System.Collections.Generic;

namespace WebStore.DomainNew.Filters
{
    public class ProductFilter 
    {
        public int? SectionId { get; set; }
        public int? BrandId { get; set; }
        public List<int> Ids { get; set; }

        /// <summary>Текущая страница</summary>
        public int Page { get; set; }

        /// <summary>Количество элементов на странице</summary>
        public int? PageSize { get; set; }

    }
}