using BasarnasApp.Shared;
using BasarnasApp.Shared.Models;

namespace BasarnasMobilaApp.Models
{
    public class Kejadian : BaseNotify
    {
        private int id;
        private DistrictRequest district;
        private JenisKejadianRequest jenis;
        private string longlat;
        private string photo;
        private DateTime tanggal;
        private StatusLaporan status;
        private Pelapor pelapor;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        public DistrictRequest District
        {
            get { return district; }
            set { SetProperty(ref district, value); }
        }
        public JenisKejadianRequest JenisKejadian
        {
            get { return jenis; }
            set { SetProperty(ref jenis, value); }
        }
        public string? LongLat
        {
            get { return longlat; }
            set { SetProperty(ref longlat, value); }
        }
        public string? Photo
        {
            get { return photo; }
            set { SetProperty(ref photo, value); }
        }
        public DateTime Tanggal
        {
            get { return tanggal; }
            set { SetProperty(ref tanggal, value); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description , value); }
        }

        public StatusLaporan Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }
        public Pelapor Pelapor
        {
            get { return pelapor; }
            set { SetProperty(ref pelapor, value); }
        }

        public byte[] DatPhoto { get;  set; }
    }
}
