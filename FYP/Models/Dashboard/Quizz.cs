using System.ComponentModel.DataAnnotations;

namespace FYP.Models.Dashboard
{
    public class Quizz
    {
        public int id { get; set; }
        [Display(Name = "Quizz Name"), Required]
        public string QuizzName { get; set; }
        [Display(Name = "Google Form Ebeded Form Link"),Required]
        public string GoogleFormLink { get; set; }

        public string UploadedBy { get; set; }
        public string CreateDate { get; set; }
    }
}
