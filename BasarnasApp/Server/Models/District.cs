
using System.ComponentModel.DataAnnotations.Schema;

namespace BasarnasApp.Server.Models
{
    public class District
    {
        //[Column("id_district")]
        public int Id { get; set; }


        //[Column("nama_district")]
        public string Name { get; set; }


        //[Column("keterangan")]
        public string? Description { get; set; }
    }





}
