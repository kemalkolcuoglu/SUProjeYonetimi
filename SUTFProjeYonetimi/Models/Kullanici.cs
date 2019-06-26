namespace SUTFProjeYonetimi.Models
{
	public class Kullanici
	{
		public Ogrenci Ogrenci { get; set; }
		public Akademisyen Akademisyen { get; set; }
		public int Yetki { get; set; }

		//public int ID { get; set; }
		//public int Yetki { get; set; }
		//public int NitelikID { get; set; }
		//public string KullaniciAdi { get; set; }
		//public string Sifre { get; set; }
		//public DateTime SonErisimTarihi { get; set; }
		//public bool Etkin { get; set; }
		//public bool Silindi { get; set; }
	}
}