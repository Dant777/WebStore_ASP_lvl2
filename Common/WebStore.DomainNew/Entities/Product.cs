using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.DomainNew.Entities.Base;
using WebStore.DomainNew.Entities.Base.Interfaces;

namespace WebStore.DomainNew.Entities
{
    [Table("Products")]
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public int SectionId { get; set; }
        public int? BrandId { get; set; }

        [DisplayName("URL-адрес изображения")]
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string  Manufacturer { get; set; }

        [ForeignKey("SectionId")]
        public virtual Section Section { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }
    }
}