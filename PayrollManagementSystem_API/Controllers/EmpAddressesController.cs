using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollManagementSystem_Sprint2.Models;

namespace PayrollManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpAddressesController : ControllerBase
    {
        private readonly PayrollManagementSystemMVCContext _context;

        public EmpAddressesController(PayrollManagementSystemMVCContext context)
        {
            _context = context;
        }

        // GET: api/EmpAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpAddress>>> GetEmpAddresses()
        {
            return await _context.EmpAddresses.ToListAsync();
        }

        // GET: api/EmpAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpAddress>> GetEmpAddress(string id)
        {
            var empAddress = await _context.EmpAddresses.FindAsync(id);

            if (empAddress == null)
            {
                return NotFound();
            }

            return empAddress;
        }

        // PUT: api/EmpAddresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpAddress(string id, EmpAddress empAddress)
        {
            if (id != empAddress.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(empAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpAddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmpAddresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpAddress>> PostEmpAddress(EmpAddress empAddress)
        {
            _context.EmpAddresses.Add(empAddress);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmpAddressExists(empAddress.EmployeeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmpAddress", new { id = empAddress.EmployeeId }, empAddress);
        }

        // DELETE: api/EmpAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpAddress(string id)
        {
            var empAddress = await _context.EmpAddresses.FindAsync(id);
            if (empAddress == null)
            {
                return NotFound();
            }

            _context.EmpAddresses.Remove(empAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpAddressExists(string id)
        {
            return _context.EmpAddresses.Any(e => e.EmployeeId == id);
        }
    }
}
