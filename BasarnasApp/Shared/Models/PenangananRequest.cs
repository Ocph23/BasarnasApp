using BasarnasApp.Shared;

namespace BasarnasApp.Shared.Models
{
    public class PenangananRequest
    {
        public int Id { get; set; }

        public InstansiRequest Instansi { get; set; }

        public PihakTerkaitRequest PihakTerkait { get; set; }

        public StatusPenganan Status { get; set; }

        public string? Lokasi { get; set; }
        public string? Deskripsi { get; set; }
        public Penyebab Penyebab { get; set; }
    }

}
