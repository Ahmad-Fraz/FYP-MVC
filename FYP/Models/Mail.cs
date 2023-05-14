using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Mail
    {
        [Key]
        public int id { get; set; }

        public string? Name { get; set; }
        public string? From { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
    }
}
