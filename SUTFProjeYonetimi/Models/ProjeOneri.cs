using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUTFProjeYonetimi.Models
{
	public class ProjeOneri
	{
		[Key]
		public int ID { get; set; }

		[DisplayName("Öğrenci")]
		public int OgrenciID { get; set; }

		[DisplayName("Danışman")]
		public int DanismanID { get; set; }

		[DisplayName("Proje Adı")]
		public string ProjeAdi { get; set; }

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

		[DisplayName("Bölüm Başkanı Onay Tarihi")]
		public DateTime BolumBaskaniOnayTarihi { get; set; }

		[DisplayName("Bölüm Başkanı Onayı")]
		public bool BolumBaskaniOnay { get; set; }

		[DisplayName("Danışman Onay Tarihi")]
		public DateTime DanismanOnayTarihi { get; set; }

		[DisplayName("Danışman Onayı")]
		public bool DanismanOnay { get; set; }
	}
}