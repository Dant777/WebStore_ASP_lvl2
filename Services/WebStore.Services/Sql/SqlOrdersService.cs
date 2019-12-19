using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL;
using WebStore.DomainNew.Dto.Order;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Helpers;
using WebStore.Interfaces;

namespace WebStore.Services.Sql
{
    public class SqlOrdersService : IOrdersService
    {
        private readonly WebStoreContext _context;
        private readonly UserManager<User> _userManager;

        public SqlOrdersService(WebStoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            return _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .Where(o => o.User.UserName == userName)
                // сконвертируем в Dto-заказ
                .Select(o => o.ToDto())
                .ToList();
        }

        public OrderDto GetOrderById(int id)
        {
            var order = _context
                .Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == id);

            if (order == null) return null;

            // вернем заказ, сконвертированный в Dto
            return order.ToDto();
        }

        public OrderDto CreateOrder(CreateOrderDto createOrderDto, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using (var transaction = _context.Database.BeginTransaction())
            {
                var order = new Order
                {
                    Address = createOrderDto.OrderViewModel.Address,
                    Name = createOrderDto.OrderViewModel.Name,
                    Date = DateTime.Now,
                    Phone = createOrderDto.OrderViewModel.Phone,
                    User = user
                };

                _context.Orders.Add(order);

                foreach (var item in createOrderDto.OrderItems)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id == item.Id);

                    if (product == null)
                    {
                        throw new InvalidOperationException("Продукт не найден в базе");
                    }

                    var orderItem = new OrderItem
                    {
                        Price = product.Price,
                        Quantity = item.Quantity,
                        Order = order,
                        Product = product
                    };

                    _context.OrderItems.Add(orderItem);
                }

                _context.SaveChanges();
                transaction.Commit();

                return GetOrderById(order.Id);
            }
        }
    }
}
