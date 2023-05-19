using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dashboard
{
    public class news_events
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage ="Please provide the event date."), Display(Name = "Event Date")]
        public string? Date { get; set; }

        [Required(ErrorMessage = "Please provide the event Start time."),Display(Name = "Event Start Time")]
        public string? FromTime { get; set; }

        [Required(ErrorMessage = "Please provide the event End time."), Display(Name = "Event End Time")]
        public string? ToTime { get; set; }

        [Required(ErrorMessage = "Please provide the event Occuring Locations.")]
        public string? Locations{ get; set; }

        [Required(ErrorMessage = "Please provide the event Title")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Please provide the event Description."),DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please provide the event Short Description."), DataType(DataType.MultilineText),Display(Name ="Short Description"),MinLength(20,ErrorMessage ="Please enter minimun 20 charachters"),MaxLength(150,ErrorMessage ="Please enter maximum 150 charachters")]
        public string? ShortDescription { get; set; }


        public string? Event_Image_Name { get; set; }

        [NotMapped]
        public IFormFile? FormFile { get; set; }

    }
}