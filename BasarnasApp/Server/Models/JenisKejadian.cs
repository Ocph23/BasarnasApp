
using System.ComponentModel.DataAnnotations.Schema;

namespace BasarnasApp.Server.Models
{
    public class JenisKejadian
    {
        //[Column("id_jenis_kejadian")]
        public int Id { get; set; }


        //[Column("nama_kejadian")]
        public string Name { get; set; }


        //[Column("keterangan")]
        public string? Description { get; set; }

        public ICollection<ItemInstansi> JenisInstansi {get; set;} = new List<ItemInstansi>();

        public void AddNewInstansi(Instansi instansi){
            JenisInstansi.Add(new ItemInstansi{ Instansi = instansi});
        }
    }


    public class ItemInstansi{
        public int Id { get; set;}
        public Instansi Instansi {get; set;}
    }
}
