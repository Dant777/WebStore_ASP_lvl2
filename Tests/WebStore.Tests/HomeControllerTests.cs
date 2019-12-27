
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStore.Controllers;
using WebStore.Interfaces;
using Xunit;

namespace WebStore.Tests
{
    public class HomeControllerTests
    {
        private HomeController _controller;
        public HomeControllerTests()
        {
            //Arrange
            var mockService = new Mock<IValueService>();//Заглушка
            mockService
                .Setup(c=>c.GetAsync())
                .ReturnsAsync(new List<string> { "1", "2"});//ожидание
            _controller = new HomeController(mockService.Object, null);
        }


        [Fact]
        public async Task Index_Method_Returns_View_With_Values()
        {

            //Arrange - подготовка


            //Act - оброщение 

            var result = await _controller.Index();
            //Assert - проверка

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void ContactUs_Returns_View()
        {

            //Act - оброщение 

            var result = _controller.ContactUs();
            //Assert - проверка
            Assert.IsType<ViewResult>(result);
            
        }
        [Fact]
        public void ErrorStatus_404_Redirects_to_NotFound()
        {
            //Act - оброщение
            var result = _controller.ErrorStatus("403");//Ожидание 404

            //Assert - проверка
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            //Проверяем имя action - метода
            Assert.Equal("NotFound", redirectToActionResult.ActionName);
        }

        [Fact]
        public void ErrorStatus_Antother_Returns_Content_Result()
        {
            //Act - обращение к методу с ошибкой 500
            var result = _controller.ErrorStatus("500");
            var contentResult = Xunit.Assert.IsType<ContentResult>(result);
            Xunit.Assert.Equal("Статуcный код ошибки: 500",
                contentResult.Content);
        }

    }
}
