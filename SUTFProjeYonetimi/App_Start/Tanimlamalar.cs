using SUTFProjeYonetimi.Models;
using SUTFProjeYonetimi.Transactions;

namespace SUTFProjeYonetimi.App_Start
{
	public static class Tanimlamalar
	{
		public static TemelIslemler<Akademisyen> akademisyenIslemleri = new TemelIslemler<Akademisyen>("akademisyen");
		public static TemelIslemler<Bolum> bolumIslemleri = new TemelIslemler<Bolum>("bolum");
		public static TemelIslemler<Fakulte> fakulteIslemleri = new TemelIslemler<Fakulte>("fakulte");
		public static TemelIslemler<Kullanici> kullaniciIslemleri = new TemelIslemler<Kullanici>("kullanici");
		public static TemelIslemler<Mesaj> mesajIslemleri = new TemelIslemler<Mesaj>("mesaj");
		public static TemelIslemler<Ogrenci> ogrenciIslemleri = new TemelIslemler<Ogrenci>("ogrenci");
		public static TemelIslemler<OgrenciDanisman> ogrenciDanismanIslemleri = new TemelIslemler<OgrenciDanisman>("ogrencidanisman");
		public static TemelIslemler<Proje> projeIslemleri = new TemelIslemler<Proje>("proje");
		public static TemelIslemler<ProjeNot> projeNotIslemleri = new TemelIslemler<ProjeNot>("projenot");
		public static TemelIslemler<ProjeOgrenciDanisman> projeOgrDanIslemleri = new TemelIslemler<ProjeOgrenciDanisman>("projeogrdan");
		public static TemelIslemler<ProjeOneri> projeOneriIslemleri = new TemelIslemler<ProjeOneri>("projeoneri");
	}
}