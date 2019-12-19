using System.Collections.Generic;
using WebStore.DomainNew.ViewModels;

namespace WebStore.DomainNew.Dto.Order
{
    public class CreateOrderDto
    {
        public OrderViewModel OrderViewModel { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}