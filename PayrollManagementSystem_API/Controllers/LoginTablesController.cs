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
    public class LoginTablesController : ControllerBase
    {
        private readonly PayrollManagementSystemMVCContext _context;

        public LoginTablesController(PayrollManagementSystemMVCContext context)
        {
            _context = context;
        }

        // GET: api/LoginTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginTable>>> GetLoginTables()
        {
            return await _context.LoginTables.ToListAsync();
        }

        // GET: api/LoginTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoginTable>> GetLoginTable(string id)
        {
            var loginTable = await _context.LoginTables.FindAsync(id);

            if (loginTable == null)
            {
                return NotFound();
            }

            return loginTable;
        }

        // PUT: api/LoginTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoginTable(string id, LoginTable loginTable)
        {
            if (id != loginTable.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(loginTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginTableExists(id))
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

        // POST: api/LoginTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoginTable>> PostLoginTable(LoginTable loginTable)
        {
            _context.LoginTables.Add(loginTable);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoginTableExists(loginTable.EmployeeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLoginTable", new { id = loginTable.EmployeeId }, loginTable);
        }

        // DELETE: api/LoginTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoginTable(string id)
        {
            var loginTable = await _context.LoginTables.FindAsync(id);
            if (loginTable == null)
            {
                return NotFound();
            }

            _context.LoginTables.Remove(loginTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoginTableExists(string id)
        {
            return _context.LoginTables.Any(e => e.EmployeeId == id);
        }


    }
}
