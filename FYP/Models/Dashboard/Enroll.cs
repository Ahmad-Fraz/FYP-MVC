using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace FYP.Models.Dashboard
{
    public class Enroll
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Candidate Name"),Required]
        public string? CandidateName { get; set; }
        [Display(Name = "Candidate Email"), Required,DataType(DataType.EmailAddress)]
        public string? CandidateEmail { get; set; }
        [Display(Name = "Candidate Phone No"), Required]
        public string? CandidatePhoneNo { get; set; }
        [Display(Name = "Enroll In Course")]
        public bool EnrollInCourse { get; set; }

        public int? CourseId { get; set; }

    }
}
