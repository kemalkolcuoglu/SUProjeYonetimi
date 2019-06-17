using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUTFProjeYonetimi.Models
{
	public class Proje
	{
		[Key, DisplayName("Proje No.")]
		public int ID { get; set; }

		public int ProjeOneriID { get; set; }

		public string ProjeNo { get; set; }

		public int ProjeTipi { get; set; }

		[Required, MaxLength(255), DisplayName("Proje Adı")]
		public string ProjeAdi { get; set; }

		[Required, MaxLength(255), DisplayName("Proje Açıklaması")]
		public string ProjeAciklamasi { get; set; }

		public string Rapor { get; set; }

		[DisplayName("Ek Dosya")]
		public string EkDosya { get; set; }

		[MaxLength(255), DisplayName("Ek Alan-1")]
		public string EkAlan1 { get; set; }

		[MaxLength(255), DisplayName("Ek Alan-2")]
		public string EkAlan2 { get; set; }

		[DisplayName("Başlangıç Tarihi")]
		public DateTime BaslangicTarihi { get; set; }

		[DisplayName("Bitiş Tarihi")]
		public DateTime BitisTarihi { get; set; }

		public bool Etkin { get; set; }

		public bool Silindi { get; set; }
	}
}