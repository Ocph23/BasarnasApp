using BasarnasApp.Shared;
using BasarnasApp.Shared.Models;

namespace BasarnasApp.Server.Models
{
    public class Penanganan
    {
        public int Id { get; set; }

        public Instansi Instansi { get; set; }

        public PihakTerkait? PihakTerkait { get; set; }

        public StatusPenganan Status { get; set; }

        public Penyebab Penyebab { get; set; }
        public string? Lokasi { get;  set; }
        public string?  Deskripsi { get; set; }
    }

}
