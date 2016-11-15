using System.Collections.Generic;
using System.Linq;

using NorthwindWebAPI.Data;
using NorthwindWebAPI.Models;

namespace NorthwindWebAPI.Services
{

    public class EmployeeService
    {
        private EmployeeRepository _employeeRepository;

        private static List<Employee> _employeeList;

        //this boolean determines if cached employee list is up to date
        private static bool _employeeListStale = true;  

        public EmployeeService()
        { _employeeRepository = new EmployeeRepository(); }

        public IEnumerable<Employee> Get()
        {
            if(_employeeList == null || _employeeListStale)
            {
                _employeeList = _employeeRepository.Get().ToList();
            }

            return _employeeList;
        }

        public Employee Get(int employeeId)
        {
            if (_employeeList != null && _employeeListStale == false)
                return _employeeList.FirstOrDefault(emp => emp.EmployeeID == employeeId);
            else return _employeeRepository.Get(employeeId);
        }

        public void Add(Employee employee)
        {
            _employeeRepository.Add(employee);
            _employeeListStale = true;  
        }

        public void Update(Employee employee)
        {
            _employeeRepository.Update(employee);
            _employeeListStale = true;
        }

        public void Delete(int employeeId)
        {
            _employeeRepository.Delete(employeeId);
            _employeeListStale = true;
        }
    }
}
