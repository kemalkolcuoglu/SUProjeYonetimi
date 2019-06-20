using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUTFProjeYonetimi.Models
{
	public class Donem
	{
		public int ID { get; set; }

		[DisplayName("Dönem Adı"), MaxLength(25)]
		public string Ad { get; set; }

		[DisplayName("Başlangıç Tarihi")]
		public DateTime BaslangicTarihi { get; set; }

		[DisplayName("Bitiş Tarihi")]
		public DateTime BitisTarihi { get; set; }

		public bool Etkin { get; set; }
		public bool Silindi { get; set; }
	}
}