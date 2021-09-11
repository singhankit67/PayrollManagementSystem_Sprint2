using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PayrollManagementSystem_API.Models;
using PayrollManagementSystem_Sprint2.Models;

namespace PayrollManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeMastersController : ControllerBase
    {
        private readonly PayrollManagementSystemMVCContext _context;
        private readonly JWTSettings _jwtSettings;

        public EmployeeMastersController(PayrollManagementSystemMVCContext context, IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
            _jwtSettings = jwtsettings.Value;
        }

        // GET: api/EmployeeMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeMaster>>> GetEmployeeMasters()
        {
            return await _context.EmployeeMasters.ToListAsync();
        }

        // GET: api/EmployeeMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeMaster>> GetEmployeeMaster(string id)
        {
            var employeeMaster = await _context.EmployeeMasters.FindAsync(id);

            if (employeeMaster == null)
            {
                return NotFound();
            }

            return employeeMaster;
        }

        // PUT: api/EmployeeMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeMaster(string id, EmployeeMaster employeeMaster)
        {
            if (id != employeeMaster.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employeeMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeMasterExists(id))
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

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<AuthorizationResponse>> Login([FromBody] LoginDetails loginObj)
        {
            var user = _context.EmployeeMasters.FirstOrDefault(a => a.EmployeeUserName == loginObj.UserName && a.EmployeePassword == loginObj.Password);
            if (user == null) return NotFound();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.EmployeeUserName)
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)


            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenRes = tokenHandler.WriteToken(token);

            AuthorizationResponse ar = new AuthorizationResponse(user, tokenRes);
            return Ok(ar);

        }


        // POST: api/EmployeeMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeMaster>> PostEmployeeMaster(EmployeeMaster employeeMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid Model");

            }
            using (var _dbContext = new PayrollManagementSystemMVCContext())
            {
                _dbContext.EmployeeMasters.Add(new EmployeeMaster()
                {
                    EmployeeId = employeeMaster.EmployeeId,
                    EmployeePassword = employeeMaster.EmployeePassword,
                    EmployeeDoj = employeeMaster.EmployeeDoj,
                    EmployeeFirstname = employeeMaster.EmployeeFirstname,
                    EmployeeLastname = employeeMaster.EmployeeLastname,
                    AdminPrivilege = employeeMaster.AdminPrivilege,
                    Gender = employeeMaster.Gender,
                    MaritalStatus = employeeMaster.MaritalStatus
                });
                _dbContext.SaveChanges();
            }

            return Ok();

        }

        // DELETE: api/EmployeeMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeMaster(string id)
        {
            var employeeMaster = await _context.EmployeeMasters.FindAsync(id);
            if (employeeMaster == null)
            {
                return NotFound();
            }

            _context.EmployeeMasters.Remove(employeeMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeMasterExists(string id)
        {
            return _context.EmployeeMasters.Any(e => e.EmployeeId == id);
        }
    }
}
