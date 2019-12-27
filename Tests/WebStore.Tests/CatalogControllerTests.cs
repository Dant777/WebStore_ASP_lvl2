using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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
        private Mock<IProductService> _mockProductService;
        private CatalogController _controller;
        public CatalogControllerTests()
        {
            //объекты заглушки для передачи в конструктор
            _mockProductService = new Mock<IProductService>();
            _controller  = new CatalogController(_mockProductService.Object);
        }
        //проверка что методд controller.ProductDetails() возвращяет корректную модель
        [Fact]
        public void ProductDetails_Returns_View_With_Correct_Item()
        {
            
            //Arrange - подготовка
            //Заглушка для нужного метода
            //должен возвращять какойто список товаров LIst<ProductDto>
            
            //База данных
           List<ProductDto> list = new List<ProductDto>{
               new ProductDto
           {
               Id = 1,
               ImageUrl = "PictURL",
               Name = "Name1",
               Order = 1,
               Price = 1000,
               Brand = new BrandDto
               {
                   Id = 1,
                   Name = "Brand1"
               }
           },
               new ProductDto
               {
                   Id = 2,
                   ImageUrl = "PictURL",
                   Name = "Name2",
                   Order = 2,
                   Price = 2000,
                   Brand = new BrandDto
                   {
                       Id = 2,
                       Name = "Brand2"
                   }
               }
           };
           
            int id = 1;
            _mockProductService.Setup(c => c.GetProductById(id)).Returns(list.FirstOrDefault(x=>x.Id == id));
            
            //Act - обращение
            //Вызов тестируемого меттода

            var result = _controller.ProductDetails(id);
            
            //Assert - проверка
            //Проверим тип пришедших данных
            //ПРоверим поля модели

            var viewResult = Assert.IsType<ViewResult>(result);
            var model =  Assert.IsAssignableFrom<ProductViewModel>(viewResult.ViewData.Model);
            //Проверка по имени
            Assert.Equal("Name1", model.Name);
            
        }
        [Fact]
        //Metod controller.ProductDetails() должен вернуть 404 NotFound, если не найден товар по id
        public void ProductDetails_Returns_NotFound()
        {
            //Arrange - подготовка
            //Заглушка для нужного метода
            //этот метод всегда будет возвращять null
            
            //База данных
            List<ProductDto> list = new List<ProductDto>{
                new ProductDto
                {
                    Id = 1,
                    ImageUrl = "PictURL",
                    Name = "Name1",
                    Order = 1,
                    Price = 1000,
                    Brand = new BrandDto
                    {
                        Id = 1,
                        Name = "Brand1"
                    }
                },
                new ProductDto
                {
                    Id = 2,
                    ImageUrl = "PictURL",
                    Name = "Name2",
                    Order = 2,
                    Price = 2000,
                    Brand = new BrandDto
                    {
                        Id = 2,
                        Name = "Brand2"
                    }
                }
            };
            int id = 3;//больше 2 возвращяет null
            _mockProductService.Setup(c => c.GetProductById(id)).Returns(list.FirstOrDefault(x => x.Id == id));
            //Act - оброщение
            var result = _controller.ProductDetails(id);

            //Assert - проверка
            //ответ от контроллера должен быть типа NotFoundResult
            Assert.IsType<NotFoundResult>(result);
            

        }
        //проверка корректной работы controller.Shop()
        public void Shop_Method_Returns_Current_View()
        {
            //Arrange - подготовка
            //Заглушка для нужного метода
            //возвращять список LIst<ProductDto>

            //Act - оброщение
            //Вызов тестируемого меттода

            //Assert - проверка
            //Проверяем тип данных
            //проверяем поля модели
        }

    }
       
}
