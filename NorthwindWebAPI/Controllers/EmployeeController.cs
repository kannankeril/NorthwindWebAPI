using System.Net;
using System.Net.Http;
using System.Web.Http;

using NorthwindWebAPI.Models;
using NorthwindWebAPI.Services;

namespace NorthwindWebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        private EmployeeService _employeeService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService();
        }

        // GET: api/Employee
        public HttpResponseMessage Get()
        {
            var employees = _employeeService.Get();
            if (employees != null)
                return Request.CreateResponse(HttpStatusCode.OK, employees);
            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No employees found");
        }

        // GET: api/Employee/5
        public HttpResponseMessage Get(int id)
        {
            var employee = _employeeService.Get(id);
            if (employee != null)
                return Request.CreateResponse(HttpStatusCode.OK, employee);
            else return Request.CreateErrorResponse(HttpStatusCode.NotFound
                                , "Employee with Id " + id.ToString() + " does not exist");
        }

        // POST: api/Employee
        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            _employeeService.Add(employee);
            return Request.CreateResponse(HttpStatusCode.Created); ;
        }

        // PUT: api/Employee/5
        public HttpResponseMessage Put(int id, [FromBody]Employee employee)
        {
            _employeeService.Update(employee);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE: api/Employee/5
        public HttpResponseMessage Delete(int id)
        {
            _employeeService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
