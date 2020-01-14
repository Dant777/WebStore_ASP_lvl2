using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebStore.Controllers;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;
using Xunit;

namespace WebStore.Tests
{
    public class CatalogControllerTests
    {
        [Fact]
        public void ProductDetails_Returns_View_With_Correct_Item()
        {
            // Arrange
            var productMock = new Mock<IProductService>();
            productMock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns(new ProductDto
                {
                    Id = 1,
                    Name = "Test",
                    ImageUrl = "TestImage.jpg",
                    Order = 0,
                    Price = 10,
                    Brand = new BrandDto
                    {
                        Id = 1,
                        Name = "TestBrand"
                    }
                });
            var controller = new CatalogController(productMock.Object);

            // Act
            var result = controller.ProductDetails(1);

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<ProductViewModel>(viewResult.ViewData.Model);

            Xunit.Assert.Equal(1, model.Id);
            Xunit.Assert.Equal("Test", model.Name);
            Xunit.Assert.Equal(10, model.Price);
            Xunit.Assert.Equal("TestBrand", model.BrandName);
        }

        [Fact]
        public void ProductDetails_Returns_NotFound()
        {
            // Arrange
            var productMock = new Mock<IProductService>();
            productMock
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns((ProductDto)null);
            var controller = new CatalogController(productMock.Object);

            // Act
            var result = controller.ProductDetails(1);

            // Assert
            Xunit.Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Shop_Method_Returns_Correct_View()
        {
            // Arrange
            var productMock = new Mock<IProductService>();
            productMock
                .Setup(p => p.GetProducts(It.IsAny<ProductFilter>()))
                .Returns(new List<ProductDto>
                {
                    new ProductDto
                    {
                        Id = 1,
                        Name = "Test",
                        ImageUrl = "TestImage.jpg",
                        Order = 0,
                        Price = 10,
                        Brand = new BrandDto
                        {
                            Id = 1,
                            Name = "TestBrand"
                        }
                    },
                    new ProductDto
                    {
                        Id = 2,
                        Name = "Test2",
                        ImageUrl = "TestImage2.jpg",
                        Order = 1,
                        Price = 22,
                        Brand = new BrandDto
                        {
                            Id = 1,
                            Name = "TestBrand"
                        }
                    }
                });
            var controller = new CatalogController(productMock.Object);

            // Act
            var result = controller.Shop(1, 5);

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<CatalogViewModel>(viewResult.ViewData.Model);

            Xunit.Assert.Equal(2, model.Products.Count());
            Xunit.Assert.Equal(5, model.BrandId);
            Xunit.Assert.Equal(1, model.SectionId);
            Xunit.Assert.Equal("TestImage2.jpg", model.Products.ToList()[1].ImageUrl);
        }
    }
}
