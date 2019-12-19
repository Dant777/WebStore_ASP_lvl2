using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.Clients.Services
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        protected override string ServiceAddress { get; } = "api/employees";

        public EmployeesClient(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<EmployeeView> GetAll()
        {
            string url = $"{ServiceAddress}";
            return Get<List<EmployeeView>>(url);
        }

        public EmployeeView GetById(int id)
        {
            string url = $"{ServiceAddress}/{id}";
            return Get<EmployeeView>(url);
        }

        public void Commit()
        {
        }

        public void AddNew(EmployeeView model)
        {
            string url = $"{ServiceAddress}";
            Post(url, model);
        }

        public void Delete(int id)
        {
            string url = $"{ServiceAddress}/{id}";
            Delete(url);
        }

        public EmployeeView UpdateEmployee(int id, EmployeeView entity)
        {
            string url = $"{ServiceAddress}/{id}";
            var result = Put(url, entity);
            return result.Content.ReadAsAsync<EmployeeView>().Result;
        }

    }
}
