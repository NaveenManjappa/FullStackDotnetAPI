using FullStackAPI.Data;
using FullStackAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace FullStackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;
        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllEmployees()
        {
           var employees= await _fullStackDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]Employee empRequest)
        {
            Random random = new Random();

            //empRequest.Id = random.Next();
            await _fullStackDbContext.Employees.AddAsync(empRequest);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(empRequest);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            var emp = await _fullStackDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null)
                return NotFound();

            return Ok(emp);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id,Employee employee)
        {
           var emp= await _fullStackDbContext.Employees.FindAsync(id);
            if(emp == null)
                return NotFound();

            emp.Name = employee.Name;
            emp.Email = employee.Email;
            emp.Phone = employee.Phone;
            emp.Salary = employee.Salary;
            emp.Department = employee.Department;

            await _fullStackDbContext.SaveChangesAsync();
            return Ok(emp);
            
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var emp = await _fullStackDbContext.Employees.FindAsync(id);
            if (emp == null)
                return NotFound();

            _fullStackDbContext.Employees.Remove(emp);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(emp);
        }
    }
}
