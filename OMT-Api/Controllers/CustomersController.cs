using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMT_Api.Data;
using OMT_Api.Entities;

namespace OMT_Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly OmtDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(OmtDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet, Authorize]
        public async Task<IEnumerable<Customer>> Get() => await _context.Customers.ToListAsync();

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return customer == null ? NotFound() : Ok(customer);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        // TO DO TEST
        [HttpPatch("{id}"), Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] JsonPatchDocument<Customer> customerDoc)
        {
            if (customerDoc == null)
            {
                return BadRequest();
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            customerDoc.ApplyTo(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // TO DO TEST
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customerToDelete = await _context.Customers.FindAsync(id);
            if (customerToDelete == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customerToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}