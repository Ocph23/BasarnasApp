﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace BasarnasApp.Server.Models
{
    public class PihakTerkait
    {
      //  [Column("id_pihak_terkait")]
        public int Id { get; set; }
      //  [Column("nama_pihak_terkait")]
        public string Name { get; set; }
      //  [Column("keterangan")]

        public string? Email { get; set; }   
        public string? Description { get; set; }
        public Instansi Instansi { get; set; }
        public District? District { get; set; }
        public string UserId { get; set; }

    }





}
