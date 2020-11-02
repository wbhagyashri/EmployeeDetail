using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeAccess.Models
{
    public class EmpDetails
    {
        public bool IsAuthorized { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public string EmpMobile { get; set; }
        public string EmpEmail { get; set; }
        public string messageType { get; set; }
        public string message { get; set; }
    }
}