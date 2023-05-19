using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Models
{
    public class SignUpModel
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress), Required(ErrorMessage = "Please enter your Email Address")]
        public string Email { get; set; }

        [Display(Name = "Date of Birth"), DataType(DataType.Date), Required(ErrorMessage = "Please enter your Date of Birth")]
        public DateTime DOB { get; set; }

        [Display(Name = "Phone No"), Required(ErrorMessage = "Please enter your Phone No")]
        public string PhoneNo { get; set; }

        [Display(Name = "Password"), DataType(DataType.Password), Required(ErrorMessage = "Please enter a Password")]
        public string password { get; set; }

        [Display(Name = "Confirm Password"), Compare("password"), NotMapped, DataType(DataType.Password), Required(ErrorMessage = "Please Confirm  your Password")]
        public string confirm_password { get; set; }

        public string Degree { get; set; }

        [Display(Name = "I m joining As:")]
        public string Member_Type { get; set; }

        [NotMapped, Display(Name = "Profile Picture")]
        public IFormFile? Profile_pic { get; set; }

        [Display(Name = "Gender")]
        public string? Gender { get; set; }

        [Display(Name = "Home Address")]
        public string? Home_Address { get; set; }

        [Display(Name = "City Name")]
        public string? City_Name { get; set; }

        [Display(Name = "Profile Photo")]
        public string? Profile_Photo_Path { get; set; }

        public string? Two_step_Verification_Phone { get; set; }
        public string? Recovery_Email { get; set; }

      
    }
}