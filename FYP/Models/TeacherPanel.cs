using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TeacherPanel
    {

        [Display(Name ="News & Events")]
        public IList<string>? New_Events { get; set; }

        [Display(Name = "Important Links")]
        public IList<string>? Important_Links { get; set; }

        [NotMapped]
        public IFormFile? Assignments{ get; set; }

        public IList<string>? MyNotes { get; set; }

        public IList<string>? Result { get; set; }

        public IList<string>? Current_Course { get; set; }

        public IList<string>? Previous_Course { get; set; }

    }
}
