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
        public ICollection<Penanganan>? Penanganan { get; set; } =new List<Penanganan>();
        public string? Thumb { get;  set; }
        public Pelapor Pelapor { get; set; }

        public void AddPenanganan(Penanganan penanganan)
        {
            var oldPenanganan = Penanganan.FirstOrDefault(x => x.PihakTerkait.Id == penanganan.PihakTerkait.Id);
            if (oldPenanganan == null)
            {
                Penanganan.Add(penanganan);
            }
        }
    }

}
