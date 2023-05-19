using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYP.Models.Dashboard
{
    public class Links
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="Please enter the Link Title"),MaxLength(50,ErrorMessage ="Only 1-50 charachters are required")]
        public string Title { get; set; }
        [MaxLength(50, ErrorMessage = "Only 1-100 charachters are required for Subtitle")]
        public string? SubTitle { get; set; }
        [MaxLength(250, ErrorMessage = "Only 1-250 charachters are required For Description of Link")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please provide the Link ")]
        public string links { get; set; }

      
    }
}
