using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUTFProjeYonetimi.Models
{
	public class Proje
	{
		[Key, DisplayName("Proje No.")]
		public int ID { get; set; }

		[DisplayName("Fakülte")]
		public int FakulteID { get; set; }

		[DisplayName("Bölüm")]
		public int BolumID { get; set; }

		[DisplayName("Dönemi")]
		public int DonemID { get; set; }

		[DisplayName("Öğrenci")]
		public int OgrenciID { get; set; }

		[DisplayName("Danışman")]
		public int DanismanID { get; set; }

		public string ProjeNo { get; set; }

		public int ProjeTipi { get; set; }

		[Required, MaxLength(255), DisplayName("Proje Adı")]
		public string ProjeAdi { get; set; }

		[Required, MaxLength(255), DisplayName("Proje Açıklaması")]
		public string ProjeAciklamasi { get; set; }

		[DisplayName("Proje Konusu ve Amacı")]
		public string ProjeKonusuAmaci { get; set; }

		[DisplayName("Maliyet Araştırması")]
		public string MaliyetArastirmasi { get; set; }

		[DisplayName("Çevresel Etkileri")]
		public string CevreselEtkileri { get; set; }

		[DisplayName("Etik Sakıncaları")]
		public string EtikSakincalari { get; set; }

		[DisplayName("Yaralanılan Kaynaklar")]
		public string YararlanilanKaynaklar { get; set; }

		public DateTime Tarih { get; set; }

		public int Durum { get; set; }

		[DisplayName("Ek Dosya")]
		public string EkDosya { get; set; }

		[DisplayName("Başlangıç Tarihi")]
		public DateTime BaslangicTarihi { get; set; }

		[DisplayName("Bitiş Tarihi")]
		public DateTime BitisTarihi { get; set; }

		public string Rapor { get; set; }

		[MaxLength(255), DisplayName("Ek Alan-1")]
		public string EkAlan1 { get; set; }

		[MaxLength(255), DisplayName("Ek Alan-2")]
		public string EkAlan2 { get; set; }

		public bool Etkin { get; set; }

		public bool Silindi { get; set; }
	}
}