using BasarnasApp.Shared;

namespace BasarnasApp.Server.Models
{
    public class Kejadian
    {
        public int Id { get; set; }
        public string Kode => $"Lap-{Id.ToString("D5")}";
        public JenisKejadian JenisKejadian {get;set;}
        public District District { get; set; }
        public string? LongLat { get; set; }
        public string? Photo { get; set; }
        public DateTime Tanggal { get; set; }
        public StatusLaporan Status { get; set; }
        public ICollection<PihakTerkait>? PihakTerkait { get; set; } =new List<PihakTerkait>();
        public Pelapor Pelapor { get; set; }
    }

}
