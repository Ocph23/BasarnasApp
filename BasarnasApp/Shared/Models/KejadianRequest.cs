using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasarnasApp.Shared.Models
{
    public class KejadianRequest
    {
        public int Id { get; set; }
        public string Kode => $"Lap-{Id.ToString("D5")}";
        public int JenisKejadianId{ get; set; }
        public string? JenisKejadianName { get; set; }=string.Empty;
        public int DistrictId { get; set; }
        public string? DistrictName{ get; set; }=string.Empty;
        public string? LongLat { get; set; }
        public string? Photo { get; set; }
        public DateTime Tanggal { get; set; }
        public StatusLaporan Status { get; set; }
        public int  PelaporId { get; set; }
        public string? PelaporName { get; set; }= string.Empty;
        public byte[]? DataPhoto { get; set; } = default;

        


    }
}
