using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYP.Models.Dashboard
{
    public class CourseList
    {
        [Key]
        public int id { get; set; }

        [Required,DisplayName("Department Name")]
        public string? Department { get; set; }
        [Required, DisplayName("Degree Name")]
        public string? DegreeName { get; set; }
        [Required, DisplayName("Course Title")]
        public string? CourseTitle { get; set; }
        [Required, DisplayName("Course No")]
        public string? CourseNo { get; set; }
        [Required, DisplayName("Instructor")]
        public string? Instructor { get; set; }

        [DisplayName("Course Start Date")]
        public string? CourseStartDate { get; set; }
        [DisplayName("Course End Date")]
        public string? CourseEndDate { get; set; }

        [DisplayName("Semester")]
        public string? Semester { get; set; }

        [DisplayName("Enrollment Start Date")]
        public string? EnrollmentStartDate { get; set; }

        [DisplayName("Enrollment End Date")]
        public string? EnrollmentEndDate { get; set; }
        [Required, DisplayName("Course short Description"), DataType(DataType.MultilineText),MaxLength(150,ErrorMessage ="Maximum 150 charachters are allowed")]
        public string? CourseShortDescription { get; set; }
        [DisplayName("Course Description"),DataType(DataType.MultilineText)]
        public string? CourseDescription { get; set; }
        
        public string? CoursePicName { get; set; }

        [Required, DisplayName("Course Start Date")]
        public DateTime? CourseStarts { get; set; }
        [Required, DisplayName("Course End Date")]
        public DateTime? CourseEnds { get; set; }
        [Required, DisplayName("Enrollment Start Date")]
        public DateTime? EnrollStartDate { get; set; }
        [Required, DisplayName("Enrollment End Date")]
        public DateTime? EnrollEndDate { get; set; }

        [DisplayName("Course Picture"),NotMapped]
        public IFormFile? CoursePic { get; set; }
    }
}
