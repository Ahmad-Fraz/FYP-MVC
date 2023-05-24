namespace FYP.Models
{
    public class EditRole
    {
        public EditRole()
        {
             Users = new List<string>();
        }
        public string Id { get; set; }
        public string? RoleName { get; set; }
        public IList<string>? Users { get; set; }
    }
}