using LoggingTest.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingTest.Identity.Resources
{
    public class LoginResource
    {
        public JwtToken Token { get; set; }
        public EmployeeResource Employee { get; set; }
    }
}
