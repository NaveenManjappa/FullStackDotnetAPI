using FullStackAPI.Data;
using FullStackAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
