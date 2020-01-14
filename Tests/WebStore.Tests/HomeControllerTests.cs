using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStore.Controllers;
using WebStore.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace WebStore.Tests
{
    public class HomeControllerTests
    {
        private readonly ITestOutputHelper _output;
        private HomeController _controller;

        public HomeControllerTests(ITestOutputHelper output)
        {
            _output = output;
            // Arrange
            var mockService = new Mock<IValueService>();
            mockService
                .Setup(c => c.GetAsync())
                .ReturnsAsync(new List<string> {"1", "2" });

            _controller = new HomeController(mockService.Object, null);
        }

        [Theory(DisplayName = "Add Numbers")]
        [InlineData(4, 5, 9)]
        [InlineData(2, 3, 5)]
        public void TestAddNumbers(int x, int y, int expectedResult)
        {
            _output.WriteLine($"current x={x}");
        
            Assert.Equal(4, x);
            Assert.Equal(5, y);
        }

        [Fact]
        public async Task Index_Method_Returns_View_With_Values()
        {
            _output.WriteLine("This is extra output...");

            // Arrange

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }


        [Fact]
        public void ContactUs_Returns_View()
        {
            // Arrange

            // Act
            var result = _controller.ContactUs();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ErrorStatus_404_Redirects_to_NotFound()
        {
            // Act
            var result = _controller.ErrorStatus("404");

            // Assert
            // проверяем возвращаемый тип данных
            var redirectToActionResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Null(redirectToActionResult.ControllerName);
            // проверяем имя action-метода ("NotFound")
            Xunit.Assert.Equal("NotFound", redirectToActionResult.ActionName);
        }

        [Fact]
        public void ErrorStatus_Antother_Returns_Content_Result()
        {
            // Act - обратимся к методу с кодом ошибки 500
            var result = _controller.ErrorStatus("500");

            // Assert
            // проверяем возвращаемый тип данных
            var contentResult = Xunit.Assert.IsType<ContentResult>(result);
            // проверяем сообщение на выходе
            Xunit.Assert.Equal("Статуcный код ошибки: 500", contentResult.Content);
        }

        [Fact]
        public void Checkout_Returns_View()
        {
            var result = _controller.Checkout();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void BlogSingle_Returns_View()
        {
            var result = _controller.BlogSingle();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Blog_Returns_View()
        {
            var result = _controller.Blog();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_Returns_View()
        {
            var result = _controller.Error();
            Xunit.Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void NotFound_Returns_View()
        {
            var result = _controller.NotFound();
            Xunit.Assert.IsType<ViewResult>(result);
        }

    }
}
