using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasarnasApp.Shared.Models
{
	public class PelaporRequest
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
		public string? Photo { get; set; }
        public string? Thumb { get; set; }
		public string? UserId { get; set; }
		public string? Password { get; set; }
		public string? ConfirmPassoword { get; set; }
		public byte[]? PhotoData { get; set; }
        public byte[]? DataPhoto { get; set; }
    }
}
