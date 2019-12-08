using System.Collections.Generic;
using System.Linq;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.Services.InMemory
{
    public class InMemoryEmployeeData : IEmployeesData
    {
        private readonly List<EmployeeView> _employees;

        public InMemoryEmployeeData()
        {
            _employees = new List<EmployeeView>
            {
                new EmployeeView
                {
                    Id = 1,
                    FirstName = "Иван",
                    SurName = "Иванов",
                    Patronymic = "Иванович",
                    Age = 22
                },
                new EmployeeView
                {
                    Id = 2,
                    FirstName = "Владислав",
                    SurName = "Петров",
                    Patronymic = "Иванович",
                    Age = 35
                }
            };
        }

        public void AddNew(EmployeeView model)
        {
            model.Id = _employees.Max(x=> x.Id) + 1;
            _employees.Add(model);
        }

        public void Commit()
        {
        }

        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }

        public IEnumerable<EmployeeView> GetAll()
        {
            return _employees;
        }

        public EmployeeView GetById(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }
    }
}
