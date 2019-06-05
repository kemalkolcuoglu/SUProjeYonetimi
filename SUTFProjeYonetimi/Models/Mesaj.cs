using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUTFProjeYonetimi.Models
{
	public class Mesaj
	{
		public int ID { get; set; }

		[DisplayName("Gönderen")]
		public int GonderenID { get; set; }

		[DisplayName("Alıcı")]
		public int AliciID { get; set; }

		[MaxLength(255), DisplayName("Mesaj Metni")]
		public string Metin { get; set; }

		[DisplayName("Gönderim Tarihi")]
		public DateTime GonderimTarihi { get; set; }
	}
}