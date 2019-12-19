using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.ServicesHosting.Controllers
{
    [Route("api/employees")]
    [Produces("application/json")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesApiController(IEmployeesData employeesData)
        {
            _employeesData = employeesData ?? throw new ArgumentException(nameof(employeesData));
        }

        [HttpGet, ActionName("get")]
        public IEnumerable<EmployeeView> GetAll()
        {
            return _employeesData.GetAll();
        }
        [HttpGet("{id}"), ActionName("get")]
        public EmployeeView GetById(int id)
        {
            return _employeesData.GetById(id);
        }
        [NonAction]
        public void Commit()
        {
            
        }
        [HttpPost, ActionName("post")]
        public void AddNew([FromBody]EmployeeView model)
        {
            _employeesData.AddNew(model);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeesData.Delete(id);
        }
        [HttpPut, ActionName("put")]
        public EmployeeView UpdateEmployee(int id, [FromBody]EmployeeView entity)
        {
            return _employeesData.UpdateEmployee(id, entity);
        }
    }
}