
using System.ComponentModel.DataAnnotations.Schema;

namespace BasarnasApp.Server.Models
{
    public class Instansi
    {

       // [Column("id_instansi")]
        public int Id { get; set; }


       // [Column("nama_instansi")]
        public string Name { get; set; }

       // [Column("keterangan")]
        public string? Description { get; set; }

        public string? Logo {get;set;}=string.Empty;
        public string? Thumb {get;set;}=string.Empty;

    }


}
