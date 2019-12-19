using System.Collections.Generic;
using WebStore.DomainNew.Dto.Order;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.ViewModels;

namespace WebStore.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<OrderDto> GetUserOrders(string userName);
        OrderDto GetOrderById(int id);
        OrderDto CreateOrder(CreateOrderDto createOrderDto, string userName);
    }
}
