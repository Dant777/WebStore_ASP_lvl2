using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace WebStore.Clients
{
    public abstract class BaseClient
    {
        protected HttpClient Client { get; set; }

        protected string SerciveAddress { get; set; }

        protected BaseClient(IConfiguration configuration)
        {
            //Экземпляр клиента
            Client = new HttpClient()
            {
                // Базовый адрес, на котором будут хостится сервисы
                BaseAddress = new Uri(configuration["ClientAddress"])
            };
            //Очистка хеедра
            Client.DefaultRequestHeaders.Accept.Clear();
            //Установка хедера который говорит серверу что он отправлял данные в формате json
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

    }
}
