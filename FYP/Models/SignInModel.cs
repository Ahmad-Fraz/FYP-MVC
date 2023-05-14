using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Keyless]
    public class SignInModel
    {
       
        [Display(Name = "Email"),DataType(DataType.EmailAddress), Required(ErrorMessage ="Please enter  your Email")]
        public string Email { get; set; }

        [Display(Name ="Password"),DataType(DataType.Password),Required(ErrorMessage = "Please enter  your Password")]
        public string password { get; set; }

        [Display(Name ="Keep me signed in")]
        public bool keepMeSignedIn { get; set; }
    }
}