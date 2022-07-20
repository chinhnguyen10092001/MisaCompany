using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA_Web05_NVCHINH.Model
{
    public class Employee:Base
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public int? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? email { get; set; }
        public string? Address { get; set; }
        public string? IndentityNumber { get; set; }
        public string? IndentityDate { get; set; }
        public string? IndentityPlace { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankName { get; set; }
        public string? BankBranchName { get; set; }
        
    }
}
