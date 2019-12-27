
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
            var mockService = new Mock<IValueService>();//��������
            mockService
                .Setup(c=>c.GetAsync())
                .ReturnsAsync(new List<string> { "1", "2"});//��������
            _controller = new HomeController(mockService.Object, null);
        }


        [Fact]
        public async Task Index_Method_Returns_View_With_Values()
        {

            //Arrange - ����������


            //Act - ��������� 

            var result = await _controller.Index();
            //Assert - ��������

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void ContactUs_Returns_View()
        {

            //Act - ��������� 

            var result = _controller.ContactUs();
            //Assert - ��������
            Assert.IsType<ViewResult>(result);
            
        }
        [Fact]
        public void ErrorStatus_404_Redirects_to_NotFound()
        {
            //Act - ���������
            var result = _controller.ErrorStatus("404");//�������� 404

            //Assert - ��������
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            //��������� ��� action - ������
            Assert.Equal("NotFound", redirectToActionResult.ActionName);
        }

        [Fact]
        public void ErrorStatus_Antother_Returns_Content_Result()
        {
            //Act - ��������� � ������ � ������� 500
            var result = _controller.ErrorStatus("500");
            var contentResult = Xunit.Assert.IsType<ContentResult>(result);
            //��������� 
            Xunit.Assert.Equal("�����c��� ��� ������: 500",
                contentResult.Content);
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
