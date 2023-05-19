using System.ComponentModel.DataAnnotations;

namespace FYP.Models.Dashboard
{
    public class MultipleImages
    {
        [Key]
        public int id { get; set; }
        public string? FileName { get; set; }
        public string? URL { get; set; }

    }
}
