using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasarnasApp.Shared.Models
{
    public class InstansiRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description{ get; set; }

        public string? Logo {get;set;}=string.Empty;
        public string? Thumb {get;set;}=string.Empty;

        public byte[]? DataLogo{get;set; }

    }
}
