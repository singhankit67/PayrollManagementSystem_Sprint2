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
    public class TimeSheetsController : ControllerBase
    {
        private readonly PayrollManagementSystemMVCContext _context;

        public TimeSheetsController(PayrollManagementSystemMVCContext context)
        {
            _context = context;
        }

        // GET: api/TimeSheets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeSheet>>> GetTimeSheets()
        {
            return await _context.TimeSheets.ToListAsync();
        }

        // GET: api/TimeSheets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimeSheet>> GetTimeSheet(int id)
        {
            var timeSheet = await _context.TimeSheets.FindAsync(id);

            if (timeSheet == null)
            {
                return NotFound();
            }

            return timeSheet;
        }

        // PUT: api/TimeSheets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimeSheet(int id, TimeSheet timeSheet)
        {
            if (id != timeSheet.IndexTs)
            {
                return BadRequest();
            }

            _context.Entry(timeSheet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeSheetExists(id))
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

        // POST: api/TimeSheets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TimeSheet>> PostTimesheet(TimeSheet timeSheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid Model");

            }
            using (var _dbContext = new PayrollManagementSystemMVCContext())
            {
                _dbContext.TimeSheets.Add(new TimeSheet()
                {
                    EmployeeActivity=timeSheet.EmployeeActivity,
                    EmployeeId=timeSheet.EmployeeId,
                    TimesheetId=timeSheet.TimesheetId,
                    WorkDate=timeSheet.WorkDate,
                    WorkMonth=timeSheet.WorkMonth,
                    NumberOfHoursSpent=timeSheet.NumberOfHoursSpent,
                    TotalHoursPerDay=9.5,
                    Status=false,
                });
                _dbContext.SaveChanges();
            }
            return Ok();

        }
        // DELETE: api/TimeSheets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeSheet(int id)
        {
            var timeSheet = await _context.TimeSheets.FindAsync(id);
            if (timeSheet == null)
            {
                return NotFound();
            }

            _context.TimeSheets.Remove(timeSheet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TimeSheetExists(int id)
        {
            return _context.TimeSheets.Any(e => e.IndexTs == id);
        }        
    }
}
