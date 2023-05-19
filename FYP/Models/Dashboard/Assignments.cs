using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYP.Models.Dashboard
{
    public class Assignments
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Please enter the Subject Name"), MaxLength(50, ErrorMessage = "Only 1-50 charachters are required")]
        public string? Subject { get; set; }
        [Display(Name = "Uploaded By")]
        public string? Uploaded_By { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Uploaded Time")]
        public string? Uploaded_Time { get; set; }
        [Required(ErrorMessage = "Please provide due date"),Display(Name = "Submission Date")]
        public string? Submission_Date { get; set; }
        public string? Time { get; set; }
        public string? File_Name { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }


    }
}
