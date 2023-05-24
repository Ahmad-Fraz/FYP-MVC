using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FYP.Models
{
    public class CreateRoles
    {       
        [Required,DisplayName("Role Name")]
        public string? RoleName { get; set; }
    }
}
