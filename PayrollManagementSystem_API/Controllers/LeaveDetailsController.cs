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
    public class LeaveDetailsController : ControllerBase
    {
        private readonly PayrollManagementSystemMVCContext _context;

        public LeaveDetailsController(PayrollManagementSystemMVCContext context)
        {
            _context = context;
        }

        // GET: api/LeaveDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveDetail>>> GetLeaveDetails()
        {
            return await _context.LeaveDetails.ToListAsync();
        }

        // GET: api/LeaveDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveDetail>> GetLeaveDetail(int id)
        {
            var leaveDetail = await _context.LeaveDetails.FindAsync(id);

            if (leaveDetail == null)
            {
                return NotFound();
            }

            return leaveDetail;
        }

        // PUT: api/LeaveDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveDetail(int id, LeaveDetail leaveDetail)
        {
            if (id != leaveDetail.IndexLd)
            {
                return BadRequest();
            }

            _context.Entry(leaveDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveDetailExists(id))
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

        // POST: api/LeaveDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeaveDetail>> PostLeaveDetail(LeaveDetail leaveDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid Model");

            }
            using (var _dbContext = new PayrollManagementSystemMVCContext())
            {
                _dbContext.LeaveDetails.Add(new LeaveDetail()
                {
                    EmployeeId = leaveDetail.EmployeeId,
                    LeaveDate = leaveDetail.LeaveDate,
                    ApplyDate = leaveDetail.ApplyDate,
                    LeaveDays = leaveDetail.LeaveDays,
                    Reason = leaveDetail.Reason,
                    LeaveType = leaveDetail.LeaveType,
                    //LeaveStatus = leaveDetail.LeaveStatus
                    //LeaveMonth = leaveDetail.LeaveMonth
                });
                _dbContext.SaveChanges();
            }
            return Ok();

        }

        // DELETE: api/LeaveDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveDetail(int id)
        {
            var leaveDetail = await _context.LeaveDetails.FindAsync(id);
            if (leaveDetail == null)
            {
                return NotFound();
            }

            _context.LeaveDetails.Remove(leaveDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaveDetailExists(int id)
        {
            return _context.LeaveDetails.Any(e => e.IndexLd == id);
        }



    }
}
