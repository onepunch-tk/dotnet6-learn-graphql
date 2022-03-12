using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrephQL_Web.API.Schema
{
    public class InstructorType
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
    }
}
