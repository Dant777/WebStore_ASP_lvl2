using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStore.Controllers;
using WebStore.DomainNew.Dto.Order;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;
using Xunit;

namespace WebStore.Tests
{
    public class CartControllerTests
    {
        private Mock<ICartService> _mockCartService;
        private Mock<IOrdersService> _mockOrdersService;
        private CartController _controller;

        public CartControllerTests()
        {
            //объекты заглушки для передачи в конструктор
            _mockCartService =new Mock<ICartService>();
            _mockOrdersService = new Mock<IOrdersService>();

            _controller = new CartController(_mockCartService.Object, _mockOrdersService.Object);
        }
        [Fact]
        public void CheckOut_ModelState_Invalid_Retuens_ViewModel()
        {
            //Arrage
            //добавляем ошибку в ModelState
            _controller.ModelState.AddModelError("error","InvalidModel");
            //Act
            var result = _controller.CheckOut(new OrderViewModel
            {
                Name = "test"
            });
            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OrderDetailsViewModel>(viewResult.ViewData.Model);
            //модел все равно должна была созздаться
            Assert.Equal("test",model.OrderViewModel.Name);
        }
        [Fact]
        public void CheckOut_Calls_Service_And_Return_Redirect()
        {
            #region Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
            }));
            // setting up cartService
            var mockCartService = new Mock<ICartService>();
            mockCartService.Setup(c => c.TransformCart()).Returns(new
                CartViewModel()
                {
                    Items = new Dictionary<ProductViewModel, int>()
                    {
                        { new ProductViewModel(), 1 }
                    }
                });
            // setting up ordersService
            var mockOrdersService = new Mock<IOrdersService>();
            mockOrdersService.Setup(c =>c.CreateOrder(It.IsAny<CreateOrderDto>(), It.IsAny<string>()))
                .Returns(new OrderDto() { Id = 1 });

            var controller = new CartController(mockCartService.Object,
                mockOrdersService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = user
                    }
                }
            };
            #endregion
            // Act
            var result = controller.CheckOut(new OrderViewModel()
            {
                Name =
                    "test",
                Address = "",
                Phone = ""
            });
            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectResult.ControllerName);
            Assert.Equal("OrderConfirmed", redirectResult.ActionName);
            Assert.Equal(1, redirectResult.RouteValues["id"]);
        }

    }
}
