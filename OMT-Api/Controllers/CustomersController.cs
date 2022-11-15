using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}