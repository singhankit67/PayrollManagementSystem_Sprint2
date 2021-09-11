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
    public class LeaveTypesController : ControllerBase
    {
        private readonly PayrollManagementSystemMVCContext _context;

        public LeaveTypesController(PayrollManagementSystemMVCContext context)
        {
            _context = context;
        }

        // GET: api/LeaveTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveType>>> GetLeaveTypes()
        {
            return await _context.LeaveTypes.ToListAsync();
        }

        // GET: api/LeaveTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveType>> GetLeaveType(string id)
        {
            var leaveType = await _context.LeaveTypes.FindAsync(id);

            if (leaveType == null)
            {
                return NotFound();
            }

            return leaveType;
        }

        // PUT: api/LeaveTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveType(string id, LeaveType leaveType)
        {
            if (id != leaveType.TypesOfLeave)
            {
                return BadRequest();
            }

            _context.Entry(leaveType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveTypeExists(id))
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

        // POST: api/LeaveTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeaveType>> PostLeaveType(LeaveType leaveType)
        {
            _context.LeaveTypes.Add(leaveType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LeaveTypeExists(leaveType.TypesOfLeave))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLeaveType", new { id = leaveType.TypesOfLeave }, leaveType);
        }

        // DELETE: api/LeaveTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveType(string id)
        {
            var leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            _context.LeaveTypes.Remove(leaveType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaveTypeExists(string id)
        {
            return _context.LeaveTypes.Any(e => e.TypesOfLeave == id);
        }
    }
}
