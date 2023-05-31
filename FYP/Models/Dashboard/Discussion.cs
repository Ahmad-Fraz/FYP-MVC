using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYP.Models.Dashboard
{
    public class Discussion
    {
      
        [Key]
        public int id { get; set; }
        public string? Title { get; set; }
        [Display(Name = "Sub Title")]
        public string? Sub_Title { get; set; }
        [Required]
        public string? Question { get; set; }
        [Display(Name = "Asked By")]
        public string? Asked_By { get; set; }
        
        public string? Date { get; set; }
    }
}
