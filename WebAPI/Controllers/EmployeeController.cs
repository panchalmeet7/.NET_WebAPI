using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities.Data;
using WebAPI.Entities.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly SampleDbContext _sampleDbContext;

        public EmployeeController(SampleDbContext sampleDbContext)
        {
            _sampleDbContext = sampleDbContext;
        }
        //private static List<Employee> AllEmployee = new List<Employee>
        //    {
        //    var AllEmployee = _sampleDbContext.Employees.ToList(),
        //    };
        
        //private static readonly List<Employee> AllEmployee = new List<Employee>(); 

        //Get All Record
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployee()
        {
            return Ok(await _sampleDbContext.Employees.ToListAsync());
        }

        //Get Single Record
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Employee>>> GetSingleEmployee(int id)
        {
            var singleEmployee = await _sampleDbContext.Employees.FindAsync(id);
            if (singleEmployee == null)
            {
                return BadRequest("employee not found!");
            }
            return Ok(singleEmployee);
        }

        //Create
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            _sampleDbContext.Employees.Add(employee);
            _sampleDbContext.SaveChanges();
            return Ok(await _sampleDbContext.Employees.ToListAsync());
        }

        //Update
        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee employees)
        {
            var updatedData = _sampleDbContext.Employees.Where(h => h.Employeeid == employees.Employeeid).FirstOrDefault();
            if (updatedData == null)
            {
                return BadRequest("employee not found");
            }
            else
            {
                updatedData.Employeeid = employees.Employeeid;
                updatedData.Email = employees.Email;
                updatedData.Addresss = employees.Addresss;
                updatedData.City = employees.City;
                updatedData.Firstname = employees.Firstname;
                updatedData.Lastname = employees.Lastname;
            }
            _sampleDbContext.Employees.Update(updatedData);
            _sampleDbContext.SaveChanges();
            return Ok(await _sampleDbContext.Employees.ToListAsync());
        }

        //Delete 
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var deleteHero = _sampleDbContext.Employees.Where(hero => hero.Employeeid == id).FirstOrDefault();
            if (deleteHero == null)
            {
                return BadRequest("employee not found!");
            }
             _sampleDbContext.Employees.Remove(deleteHero);
            _sampleDbContext.SaveChanges();
            return Ok(await _sampleDbContext.Employees.ToListAsync());
        }
    }
}
