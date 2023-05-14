using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class ApplicationUser:IdentityUser
    {
        public string? Gender { get; set; }
        public string? FullName{ get; set; }
        public string? Profile_Photo_Path { get; set; }
        public string? Degree{ get; set; }
        public string? Member_Type{ get; set; }
        public DateTime? DOB{ get; set; }
        public string? Home_Address { get; set; }
        public string? City_Name { get; set; }
        public string? Other_Addresses { get; set; }
        public string? Two_step_Verification_Phone { get; set; }
        public string? Recovery_Email { get; set; }

    }
}
