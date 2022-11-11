using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OMT_Api.Data;
using OMT_Api.Entities;
using OMT_Api.Models;
using OMT_Api.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace OMT_Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDbContext _context;
        private readonly IAuthService _authService;

        public EmployeesController(EmployeeDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get() => await _context.Employees.ToListAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var Employee = await _context.Employees.FindAsync(id);
            return Employee == null ? NotFound() : Ok(Employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeAuth employeeAuth)
        {
            _authService.CreatePasswordHash(employeeAuth.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var employee = new Employee();
            employee.FirstName = employeeAuth.FirstName;
            employee.LastName = employeeAuth.LastName;
            employee.Email = employeeAuth.Email;
            employee.EmployeeId = employeeAuth.EmployeeId;
            employee.Role = employeeAuth.Role;
            employee.PasswordHash = passwordHash;
            employee.PasswordSalt = passwordSalt;

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(EmployeeLoginDto employeeLoginDto)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(a => a.EmployeeId == employeeLoginDto.EmployeeId);
            if (employee == null)
            {
                return BadRequest();
            }
            if (!_authService.VerifyPasswordHash(employeeLoginDto.Password, employee.PasswordHash, employee.PasswordSalt))
            {
                return BadRequest();
            }
            string token = _authService.CreateToken(employee);
            return Ok(token);
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