using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUTFProjeYonetimi.Models
{
	public class Duyuru
	{
		[Key]
		public int ID { get; set; }

		public DateTime Tarih { get; set; }

		[MaxLength(255), DisplayName("Duyuru Başlığı")]
		public string Baslik { get; set; }

		[DisplayName("Duyuru Metni")]
		public string Metin { get; set; }

		[DisplayName("Fakülte")]
		public int FakulteID { get; set; }

		[DisplayName("Bölüm")]
		public int BolumID { get; set; }

		public bool Etkin { get; set; }
	}
}