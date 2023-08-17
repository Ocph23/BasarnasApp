namespace BasarnasApp.Shared.Models
{
    public class ReportModel
    {
        public int No { get; set; }
        public string Kesatuan { get; set; }    
        public int Kejadian { get; set; }
        public List<int> Penyebab { get; set; } = new List<int>();
        public DateTime Tanggal { get; set; }

    }
}
