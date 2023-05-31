using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ContactUsModel
    {
        [Key]
        public int id { get; set; }

        [Display(Name ="Name"),Required(ErrorMessage ="Please write your Name")]
        public string? Name { get; set; }

        [Display(Name = "Email"), DataType(DataType.EmailAddress), Required(ErrorMessage = "Please write your Email")]
        public string? Email { get; set; }

        [Display(Name = "Subject"), Required(ErrorMessage = "What  is your Subject(Main Topic)")]
        public string? Subject { get; set; }

        [Display(Name = "Description"),DataType(DataType.MultilineText), Required(ErrorMessage = "Please write some description here to explain us the problem.")]
        public string? Description { get; set; }
      
    }
}
