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
        //{
        //        new Employee {
        //            Employeeid = 1,
        //            Firstname = "fname",
        //            Lastname ="lname",
        //            Email ="email@gmail.com",
        //            Addresss = "add1" ,
        //            City ="city1",
        //        },
        //        new Employee {
        //            Employeeid = 2,
        //            Firstname = "fname2",
        //            Lastname ="lname",
        //            Email ="second@gmail.com",
        //            Addresss = "add2" ,
        //            City ="city2",
        //        },
        //};

        //Get All Record

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployee()
        {
            return Ok(await _sampleDbContext.Employees.ToListAsync());
            //return Ok(AllEmployee);
        }

        //Get Single Record
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Employee>>> GetSingleEmployee(int id)
        {
            var singleEmployee = await _sampleDbContext.Employees.FindAsync(id);
            if (singleEmployee == null)
                return BadRequest("employee not found!");

            return Ok(singleEmployee);
        }

        //Create
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            await _sampleDbContext.Employees.AddAsync(employee);
            await _sampleDbContext.SaveChangesAsync();
            return Ok(await _sampleDbContext.Employees.ToListAsync());
        }

        //Update
        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee employees)
        {
            var updatedData = await _sampleDbContext.Employees.FindAsync(employees.Employeeid);
            if (updatedData == null)
                return BadRequest("employee not found");

            updatedData.Employeeid = employees.Employeeid;
            updatedData.Email = employees.Email;
            updatedData.Addresss = employees.Addresss;
            updatedData.City = employees.City;
            updatedData.Firstname = employees.Firstname;
            updatedData.Lastname = employees.Lastname;

            await _sampleDbContext.SaveChangesAsync();
            return Ok(await _sampleDbContext.Employees.ToListAsync());
        }

        //Delete 
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var deleteHero = _sampleDbContext.Employees.Where(hero => hero.Employeeid == id).FirstOrDefault();
            if (deleteHero == null)
                return BadRequest("employee not found!");

            _sampleDbContext.Employees.Remove(deleteHero);
            _sampleDbContext.SaveChanges();
            return Ok(await _sampleDbContext.Employees.ToListAsync());
        }
    }
}
