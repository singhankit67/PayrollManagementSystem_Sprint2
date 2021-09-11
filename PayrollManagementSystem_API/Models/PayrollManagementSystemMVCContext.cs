using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using System.Data.SqlClient;

#nullable disable

namespace PayrollManagementSystem_Sprint2.Models
{
    public partial class PayrollManagementSystemMVCContext : DbContext
    {
        public PayrollManagementSystemMVCContext()
        {
        }

        public PayrollManagementSystemMVCContext(DbContextOptions<PayrollManagementSystemMVCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CountingLeave> CountingLeaves { get; set; }
        public virtual DbSet<CountingMonthlyLeave> CountingMonthlyLeaves { get; set; }
        public virtual DbSet<EmpAddress> EmpAddresses { get; set; }
        public virtual DbSet<EmployeeMaster> EmployeeMasters { get; set; }
        public virtual DbSet<HalfWorkingDaysCount> HalfWorkingDaysCounts { get; set; }
        public virtual DbSet<HalfWorkingDaysTimesheet> HalfWorkingDaysTimesheets { get; set; }
        public virtual DbSet<LeaveDetail> LeaveDetails { get; set; }
        public virtual DbSet<LeaveMaster> LeaveMasters { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<LoginTable> LoginTables { get; set; }
        public virtual DbSet<PayrollDetail> PayrollDetails { get; set; }
        public virtual DbSet<PayrollMaster> PayrollMasters { get; set; }
        public virtual DbSet<TimeSheet> TimeSheets { get; set; }
        public virtual DbSet<WorkingDaysTimesheet> WorkingDaysTimesheets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.\\sqlexpress;Trusted_Connection=True;Database=PayrollManagementSystemMVC");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CountingLeave>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Counting_Leaves");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.NumOfLeaves).HasColumnName("Num_Of_Leaves");
            });

            modelBuilder.Entity<CountingMonthlyLeave>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Counting_Monthly_Leaves");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.LeaveMonth).HasColumnName("Leave_Month");

                entity.Property(e => e.NumOfLeaves).HasColumnName("Num_Of_Leaves");
            });

            modelBuilder.Entity<EmpAddress>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK_EmpAddrId");

                entity.ToTable("Emp_Address");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Street_Address");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.EmpAddress)
                    .HasForeignKey<EmpAddress>(d => d.EmployeeId)
                    .HasConstraintName("FK__Emp_Addre__Emplo__45F365D3");
            });

            modelBuilder.Entity<EmployeeMaster>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK_EMPLOYEE_ID");

                entity.ToTable("Employee_Master");

                entity.HasIndex(e => e.EmployeePassword, "unique_password")
                    .IsUnique();

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.AdminPrivilege)
                    .HasColumnName("Admin_Privilege")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EmployeeDoj)
                    .HasColumnType("date")
                    .HasColumnName("Employee_Doj");

                entity.Property(e => e.EmployeeFirstname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Firstname");

                entity.Property(e => e.EmployeeLastname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Lastname");

                entity.Property(e => e.EmployeePassword)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Password");

                entity.Property(e => e.EmployeeUserName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Employee_User_Name");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Marital_Status")
                    .HasDefaultValueSql("('NA')");
            });

            modelBuilder.Entity<HalfWorkingDaysCount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Half_Working_Days_Count");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.NumberOfHalfDays).HasColumnName("Number_of_Half_Days");
            });

            modelBuilder.Entity<HalfWorkingDaysTimesheet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Half_Working_Days_Timesheet");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.NumberOfHalfDays).HasColumnName("Number_of_Half_Days");
            });

            modelBuilder.Entity<LeaveDetail>(entity =>
            {
                entity.HasKey(e => e.IndexLd)
                    .HasName("PK__Leave_De__B5E01E341C464DAE");

                entity.ToTable("Leave_Details");

                entity.Property(e => e.IndexLd).HasColumnName("Index_Ld");

                entity.Property(e => e.ApplyDate)
                    .HasColumnType("date")
                    .HasColumnName("Apply_Date");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Approved_By");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.LeaveDate)
                    .HasColumnType("date")
                    .HasColumnName("Leave_Date");

                entity.Property(e => e.LeaveDays).HasColumnName("Leave_Days");

                entity.Property(e => e.LeaveMonth).HasColumnName("Leave_Month");

                entity.Property(e => e.LeaveStatus)
                    .HasColumnName("Leave_Status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LeaveType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Leave_Type");

                entity.Property(e => e.Reason)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LeaveDetails)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Leave_Det__Emplo__46E78A0C");

                entity.HasOne(d => d.LeaveTypeNavigation)
                    .WithMany(p => p.LeaveDetails)
                    .HasForeignKey(d => d.LeaveType)
                    .HasConstraintName("FK__Leave_Det__Leave__3B40CD36");
            });

            modelBuilder.Entity<LeaveMaster>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("Leave_Master");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.LeavesAvailable)
                    .HasColumnName("Leaves_Available")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.LeavesAvailed)
                    .HasColumnName("Leaves_Availed")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LeavesBalance)
                    .HasColumnName("Leaves_Balance")
                    .HasDefaultValueSql("((2))");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.LeaveMaster)
                    .HasForeignKey<LeaveMaster>(d => d.EmployeeId)
                    .HasConstraintName("FK__Leave_Mas__Emplo__48CFD27E");
            });

            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.HasKey(e => e.TypesOfLeave)
                    .HasName("PK__Leave_Ty__378A9BD19BBD738F");

                entity.ToTable("Leave_Types");

                entity.Property(e => e.TypesOfLeave)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Types_Of_Leave");
            });

            modelBuilder.Entity<LoginTable>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("Login_Table");

                entity.HasIndex(e => e.Password, "UQ__Login_Ta__87909B15EFFE9746")
                    .IsUnique();

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.EmployeeUserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Employee_User_Name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SecurityAnswer)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Security_Answer");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.LoginTable)
                    .HasForeignKey<LoginTable>(d => d.EmployeeId)
                    .HasConstraintName("FK__Login_Tab__Emplo__49C3F6B7");
            });

            modelBuilder.Entity<PayrollDetail>(entity =>
            {
                entity.HasKey(e => e.EmployeeGrade)
                    .HasName("PK_Employee_Grade");

                entity.ToTable("Payroll_Details");

                entity.Property(e => e.EmployeeGrade)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Grade");

                entity.Property(e => e.EmployeeBasic)
                    .HasColumnName("Employee_Basic")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EmployeeDa)
                    .HasColumnName("Employee_DA")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EmployeeHra)
                    .HasColumnName("Employee_HRA")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EmployeePf)
                    .HasColumnName("Employee_PF")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NetSalary)
                    .HasColumnName("Net_Salary")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalWorkingDays)
                    .HasColumnName("TotalWorking_Days")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<PayrollMaster>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("Payroll_Master");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id")
                    .HasDefaultValueSql("('E')");

                entity.Property(e => e.EmployeeDesignation)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Designation");

                entity.Property(e => e.EmployeeGrade)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Grade");

                entity.Property(e => e.EmployeeSalary)
                    .HasColumnName("Employee_Salary")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.EmployeeGradeNavigation)
                    .WithMany(p => p.PayrollMasters)
                    .HasForeignKey(d => d.EmployeeGrade)
                    .HasConstraintName("FK_Employee_Grade");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.PayrollMaster)
                    .HasForeignKey<PayrollMaster>(d => d.EmployeeId)
                    .HasConstraintName("FK__Payroll_M__Emplo__4AB81AF0");
            });

            modelBuilder.Entity<TimeSheet>(entity =>
            {
                entity.HasKey(e => e.IndexTs)
                    .HasName("PK__Time_She__B5E0E12F682F5381");

                entity.ToTable("Time_Sheet");

                entity.Property(e => e.IndexTs).HasColumnName("Index_Ts");

                entity.Property(e => e.EmployeeActivity)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Activity");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.NumberOfHoursSpent)
                    .HasColumnName("Number_Of_Hours_Spent")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimesheetId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Timesheet_Id");

                entity.Property(e => e.TotalHoursPerDay)
                    .HasColumnName("Total_Hours_PerDay")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.WorkDate)
                    .HasColumnType("date")
                    .HasColumnName("Work_Date");

                entity.Property(e => e.WorkMonth).HasColumnName("Work_Month");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TimeSheets)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Time_Shee__Emplo__4CA06362");
            });

            modelBuilder.Entity<WorkingDaysTimesheet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Working_Days_Timesheet");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.WorkingDays).HasColumnName("Working_Days");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        public IQueryable<LeaveDetail> GetMonth()
        {
            return this.LeaveDetails.FromSqlRaw("exec USP_Get_Month");
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
   }
}
