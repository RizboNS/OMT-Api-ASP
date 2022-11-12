﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMT_Api.Data;
using OMT_Api.Entities;
using OMT_Api.Models;
using OMT_Api.Services;

namespace OMT_Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //TO DO Method for a Employee to change password.

        private readonly EmployeeDbContext _context;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public EmployeesController(EmployeeDbContext context, IAuthService authService, IMapper mapper)
        {
            _context = context;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpGet, Authorize(Roles = "admin")]
        public async Task<IEnumerable<Employee>> Get() => await _context.Employees.ToListAsync();

        [HttpGet("{id}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee == null ? NotFound() : Ok(_mapper.Map<EmployeeResponseDto>(employee));
        }

        [HttpPost, Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(EmployeeRegisterDto employeeRegisterDto)
        {
            _authService.CreatePasswordHash(employeeRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var employee = _mapper.Map<Employee>(employeeRegisterDto);

            employee.PasswordHash = passwordHash;
            employee.PasswordSalt = passwordSalt;

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, _mapper.Map<EmployeeResponseDto>(employee));
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

        [HttpPatch("{id}"), Authorize(Roles = "admin")]
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

            return NoContent();
        }

        [HttpDelete("{id}"), Authorize(Roles = "admin")]
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