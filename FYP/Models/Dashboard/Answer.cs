using System.ComponentModel.DataAnnotations;

namespace FYP.Models.Dashboard
{
    public class Answer
    {
        [Key]
        public int id { get; set; }
        [Required, Display(Name = "Answer")]
        public string? _Answer { get; set; }
        [Display(Name = "Answered By")]
        public string? Answered_By { get; set; }
        public string? Date { get; set; }
        public int? Question_ID { get; set; }
    }
}
