using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Tests
{
    
    public class CatalogControllerTests
    {
        //проверка что методд controller.ProductDetails() возвращяет корректную модель
        public void ProductDetails_Returns_View_With_Correct_Item()
        { 
            //Arrange - подготовка
            //Заглушка для нужного метода
            //должен возвращять какойто список товаров LIst<ProductDto>

            //Act - обращение
            //Вызов тестируемого меттода

            //Assert - проверка
            //Проверим тип пришедших данных
            //ПРоверим поля модели
        }
        //Metod controller.ProductDetails() должен вернуть 404 NotFound, если не найден товар по id
        public void ProductDetails_Returns_NotFound()
        {
            //Arrange - подготовка
            //Заглушка для нужного метода
            //этот метод всегда будет возвращять null

            //Act - оброщение


            //Assert - проверка
            //ответ от контроллера должен быть типа NotFoundResult
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
