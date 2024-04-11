using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employee
{
    public class Employeecs
    { 
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string address { get; set; }
        public string Email { get; set; }

        public string MobileNumber { get; set; }
        public string DOB { get; set; }
        public string WorkExperience { get; set; }
        public string description { get; set; }

    }
}
