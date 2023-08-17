using System.ComponentModel.DataAnnotations.Schema;

namespace BasarnasApp.Server.Models
{
    public class Pelapor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email{ get; set; }
        public string? PhoneNumber { get;  set; }

        public string Address { get; set; }
        public string? Photo { get;  set; }
        public string? Thumb { get; internal set; }
        public string UserId { get; set; }
        
        [NotMapped]
        public string Password { get; set; }
    }

}
