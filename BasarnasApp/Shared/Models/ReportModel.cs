namespace BasarnasApp.Shared.Models
{


    public class AdminReportModel
    {
        public int Bulan { get; set; }
        public int Tahun { get; set; }
        public ICollection<ReportModel> Datas { get; set; } = new List<ReportModel>();
    }

    public class ReportModel
    {
        public int No { get; set; }
        public string Kesatuan { get; set; }    
        public string Instansi { get; set; }    
        public int Kejadian { get; set; }
        public List<int> Penyebab { get; set; } = new List<int>();
        public DateTime Tanggal { get; set; }

    }


    public class ReportKejadianModel
    {
        public int No { get; set; }
        public string Kejadian { get; set; }
        public DateTime Tanggal { get; set; }
        public string PihakTerkait { get; set; }
        public string Lokasi { get; set; }
        public string Status { get; set; }
        public string Pelapor { get; set; }

    }
}
