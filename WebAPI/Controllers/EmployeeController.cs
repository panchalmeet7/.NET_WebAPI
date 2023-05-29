using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> AllEmployee = new List<Employee>
            {
                new Employee {
                    Id = 1,
                    FirstName ="Meet",
                    LastName = "Panchal",
                    City="Nadiad",
                    Department = ".NET"
                },
                new Employee {
                    Id = 2,
                    FirstName ="Dev",
                    LastName = "shah",
                    City="Ahmedabad",
                    Department = "JAVA"
                },
            };

        //Get All
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployee()
        {
            return Ok(AllEmployee);
        }
        //Get Single Record
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Employee>>> GetSingleEmployee(int id)
        {
            var singleEmployee = AllEmployee.Find(emp => emp.Id == id);
            if (singleEmployee == null)
            {
                return BadRequest(" not found!");
            }
            return Ok(singleEmployee);
        }
        //Create
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            AllEmployee.Add(employee);
            return Ok(AllEmployee);
        }
        //Update
        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee employees)
        {
            var updatedData = AllEmployee.Find(h => h.Id == employees.Id);
            if (updatedData == null)
            {
                return BadRequest("not found");
            }
            else
            {
                updatedData.Id = employees.Id;
                updatedData.FirstName = employees.FirstName;
                updatedData.LastName = employees.LastName;
                updatedData.City = employees.City;
                updatedData.Department = employees.Department;
            }
            return Ok(AllEmployee);
        }
        //Delete 
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var deleteHero = AllEmployee.Find(hero => hero.Id == id);
            if (deleteHero == null)
            {
                return BadRequest("hero not found!");
            }
            AllEmployee.Remove(deleteHero);
            return Ok(AllEmployee);
        }
    }
}
