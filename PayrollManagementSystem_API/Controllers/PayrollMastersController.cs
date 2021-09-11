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
    public class PayrollMastersController : ControllerBase
    {
        private readonly PayrollManagementSystemMVCContext _context;

        public PayrollMastersController(PayrollManagementSystemMVCContext context)
        {
            _context = context;
        }

        // GET: api/PayrollMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayrollMaster>>> GetPayrollMasters()
        {
            return await _context.PayrollMasters.ToListAsync();
        }

        // GET: api/PayrollMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayrollMaster>> GetPayrollMaster(string id)
        {
            GetMonthTimeSheet();
            var payrollMaster = await _context.PayrollMasters.FirstOrDefaultAsync(i => i.EmployeeId == id).ConfigureAwait(false);

            TotalWorkingDays(id);
            if (payrollMaster == null)
            {
                return NotFound();
            }

            return payrollMaster;
        }

        public string SalaryFindGrade(string id)
        {
            PayrollMaster employeeGrade = _context.PayrollMasters.FirstOrDefault(i => i.EmployeeId == id);
            string grade = employeeGrade.EmployeeGrade;
            return grade;
        }

        public LeaveMaster SalaryLeaveDetails(string id)
        {
            LeaveMaster leaveMaster = new LeaveMaster();
            leaveMaster = _context.LeaveMasters.FirstOrDefault(i => i.EmployeeId == id);
            return leaveMaster;
        }

        public PayrollDetail SalaryPayrollDetails(string grade)
        {
            PayrollDetail payroll = new PayrollDetail();
            payroll = _context.PayrollDetails.FirstOrDefault(i => i.EmployeeGrade == grade);
            return payroll;
        }

        public HalfWorkingDaysCount SalaryHalfWorkingDays(string id)
        {
            HalfWorkingDaysCount hwdObj = new HalfWorkingDaysCount();
            hwdObj = _context.HalfWorkingDaysCounts.FirstOrDefault(i => i.EmployeeId == id);
            return hwdObj;
        }

        public WorkingDaysTimesheet SalaryWorkingDays(string id)
        {
            WorkingDaysTimesheet wObj = new WorkingDaysTimesheet();
            wObj = _context.WorkingDaysTimesheets.FirstOrDefault(i => i.EmployeeId == id);
            return wObj;
        }


        public async void TotalWorkingDays(string id)
        {
            double? totalWorkingDays = 0;
            double? salary = 0;
            double? halfWorkingDay =0;
            double? fullWorkingDay = 0;
            double? netSalary = 0;

            string grade = SalaryFindGrade(id);

            LeaveMaster leaveMaster = SalaryLeaveDetails(id);
            double? leaveBalance = leaveMaster.LeavesBalance;
            double? leaveAvailed = leaveMaster.LeavesAvailed;

            PayrollDetail employeeGrade1 = SalaryPayrollDetails(grade);
            if(employeeGrade1!=null)
            {
                netSalary = employeeGrade1.NetSalary;
            }           

            HalfWorkingDaysCount halfWorkingDays = SalaryHalfWorkingDays(id);
            if(halfWorkingDays!=null)
            {
                halfWorkingDay = halfWorkingDays.NumberOfHalfDays;
            }           
            
            WorkingDaysTimesheet fullWorkingDays = SalaryWorkingDays(id);
            if(fullWorkingDays!=null)
            {
                fullWorkingDay = fullWorkingDays.WorkingDays;
            }           
            
            if (halfWorkingDay != 0)
            {
                totalWorkingDays = fullWorkingDay - (halfWorkingDay / 2);
            }
            else
            {
                totalWorkingDays = fullWorkingDay;
            }
            if (leaveBalance == 2)
            {
                salary = (netSalary/21)*totalWorkingDays;
            }

            else if (leaveBalance >= 0 && leaveBalance < 2)
            {
                salary = (netSalary / 21) * (totalWorkingDays + leaveAvailed);
            }

            else if (leaveBalance < 0)
            {
                salary = (netSalary / 21) * (totalWorkingDays + leaveBalance);
            }

            var sql = $"Update Payroll_Master set Employee_Salary={salary} where Employee_Id = '{id}'";

            _context.Database.ExecuteSqlRaw(sql);
        }

        // PUT: api/PayrollMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayrollMaster(string id, PayrollMaster payrollMaster)
        {
            if (id != payrollMaster.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(payrollMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayrollMasterExists(id))
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

        // POST: api/PayrollMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PayrollMaster>> PostPayrollMaster(PayrollMaster payrollMaster)
        {
            _context.PayrollMasters.Add(payrollMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayrollMaster", new { id = payrollMaster.EmployeeId }, payrollMaster);
        }

        // DELETE: api/PayrollMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayrollMaster(string id)
        {
            var payrollMaster = await _context.PayrollMasters.FindAsync(id);
            if (payrollMaster == null)
            {
                return NotFound();
            }

            _context.PayrollMasters.Remove(payrollMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PayrollMasterExists(string id)
        {
            return _context.PayrollMasters.Any(e => e.EmployeeId == id);
        }


        public async Task<IActionResult> GetMonthTimeSheet()                                     //CHANGE -2 08/09/2021
        {
            //this.FromSqlRaw("exec USP_Get_Month");
            _context.Database.ExecuteSqlInterpolated($"exec USP_Get_Month_Timesheet");
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
