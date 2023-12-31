﻿namespace BasarnasApp.Shared.Models
{
    public class PihakTerkaitRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public InstansiRequest Instansi { get; set; }
        public DistrictRequest? District { get; set; }
        public string? UserId { get; set; } =string.Empty;
        public string? Password { get; set; }
        public string? ProfileName { get; set; }
        public string? ProfileAddress { get; set; }
        public string? ProfileJabatan { get; set; }
    }
}
