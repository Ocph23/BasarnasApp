using System.ComponentModel.DataAnnotations.Schema;

namespace BasarnasApp.Server.Models
{
    public class Pelapor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email{ get; set; }

        public string Address { get; set; }
        public string UserId { get; set; }
        
        
        [NotMapped]
        public string Password { get; set; }

    }

}
