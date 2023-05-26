using System.ComponentModel.DataAnnotations;

namespace FYP.Models.Dashboard
{
    public class Discussion
    {
        [Key]
        public int id { get; set; }
        public string? Title { get; set; }
        public string? Sub_Title { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public string? Asked_By { get; set; }
        public string? Answered_By { get; set; }
        public string? Date { get; set; }
    }
}
