using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMT_Api.Data;
using OMT_Api.Entities;

namespace OMT_Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public EmployeesController(EmployeeDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get() => await _context.Employees.ToListAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var Employee = await _context.Employees.FindAsync(id);
            return Employee == null ? NotFound() : Ok(Employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] JsonPatchDocument<Employee> employeeDoc)
        {
            if (employeeDoc == null)
            {
                return BadRequest();
            }

            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            employeeDoc.ApplyTo(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var EmployeeToDelete = await _context.Employees.FindAsync(id);
            if (EmployeeToDelete == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(EmployeeToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}