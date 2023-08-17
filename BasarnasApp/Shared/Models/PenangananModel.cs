using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasarnasApp.Shared.Models
{
    public class PenangananModel
    {
        public int Id { get; set; }
        public Penyebab Penyebab { get; set; }
        public DateTime Tanggal { get; set; }
        public int InstansiId { get; set; }
        public int PihakTerkaitId { get; set; }
    }
}
