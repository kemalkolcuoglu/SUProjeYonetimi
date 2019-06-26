using SUTFProjeYonetimi.Models.Enum;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using static SUTFProjeYonetimi.App_Start.Tanimlamalar;

namespace SUTFProjeYonetimi.Models.EkModel
{
	public static class SLOlusturma
	{
		public static SelectList AkademisyenListele(int fakulte, int bolum)
		{
			List<SelectListItem> list = new List<SelectListItem>();
			List<Akademisyen> akademisyenler;

			if (fakulte == 0 && bolum == 0)
				akademisyenler = akademisyenIslemleri.VeriGetir("Silindi = 0 And Etkin = 1 And Yetki != 0");
			else if (fakulte != 0 && bolum == 0)
				akademisyenler = akademisyenIslemleri.VeriGetir("Silindi = 0 And Etkin = 1 And Yetki != 0 And FakulteID = " + fakulte);
			else
				akademisyenler = akademisyenIslemleri.VeriGetir("Silindi = 0 And Etkin = 1 And Yetki != 0 And FakulteID = " + fakulte + " And BolumID = " + bolum);

			foreach (var item in akademisyenler)
			{
				SelectListItem sli = new SelectListItem()
				{
					Text = item.Unvan + " " + item.Ad + " " + item.Soyad,
					Value = item.ID.ToString()
				};

				list.Add(sli);
			}
			return new SelectList(list, "Value", "Text");
		}

		public static SelectList OgrenciListele(int fakulte, int bolum)
		{
			List<SelectListItem> list = new List<SelectListItem>();
			List<Ogrenci> ogrenciler;

			if (fakulte == 0 && bolum == 0)
				ogrenciler = ogrenciIslemleri.VeriGetir("Silindi = 0 And Etkin = 1");
			else if (fakulte == 0 && bolum != 0)
				ogrenciler = ogrenciIslemleri.VeriGetir("Silindi = 0 And Etkin = 1 And FakulteID = " + fakulte);
			else
				ogrenciler = ogrenciIslemleri.VeriGetir("Silindi = 0 And Etkin = 1 And FakulteID = " + fakulte + " And BolumID = " + bolum);

			foreach (var item in ogrenciler)
			{
				SelectListItem sli = new SelectListItem()
				{
					Text = item.OgrenciNo + "-" + item.Ad + " " + item.Soyad,
					Value = item.ID.ToString()
				};

				list.Add(sli);
			}
			return new SelectList(list, "Value", "Text");
		}

		public static SelectList FakulteListele()
		{
			List<SelectListItem> list = new List<SelectListItem>();

			foreach (var item in fakulteIslemleri.VeriGetir("Silindi = 0 And Etkin = 1"))
			{
				SelectListItem sli = new SelectListItem()
				{
					Text = item.Ad,
					Value = item.ID.ToString()
				};

				list.Add(sli);
			}
			return new SelectList(list, "Value", "Text");
		}

		public static SelectList BolumListele()
		{
			List<SelectListItem> list = new List<SelectListItem>();

			foreach (var item in bolumIslemleri.VeriGetir("Silindi = 0 And Etkin = 1"))
			{
				SelectListItem sli = new SelectListItem()
				{
					Text = item.Ad,
					Value = item.ID.ToString()
				};

				list.Add(sli);
			}
			return new SelectList(list, "Value", "Text");
		}

		public static SelectList OgrenimTipiListele()
		{
			List<SelectListItem> list = new List<SelectListItem>();
			SelectListItem sli1 = new SelectListItem()
			{
				Text = "Normal Öğretim",
				Value = ((int)OgrenimTipi.NormalOgretim).ToString()
			};

			SelectListItem sli2 = new SelectListItem()
			{
				Text = "İkinci Öğretim",
				Value = ((int)OgrenimTipi.IkinciOgretim).ToString()
			};

			list.Add(sli1);
			list.Add(sli2);

			return new SelectList(list, "Value", "Text");
		}

		public static SelectList SinifListele()
		{
			List<SelectListItem> list = new List<SelectListItem>();
			SelectListItem sli1 = new SelectListItem()
			{
				Text = "1. Sınıf",
				Value = "1"
			};
			SelectListItem sli2 = new SelectListItem()
			{
				Text = "2. Sınıf",
				Value = "2"
			};
			SelectListItem sli3 = new SelectListItem()
			{
				Text = "3. Sınıf",
				Value = "3"
			};
			SelectListItem sli4 = new SelectListItem()
			{
				Text = "4. Sınıf",
				Value = "4"
			};

			list.Add(sli1);
			list.Add(sli2);
			list.Add(sli3);
			list.Add(sli4);


			return new SelectList(list, "Value", "Text");
		}

		public static SelectList ProjeTipiListele(int fakulte)
		{
			List<SelectListItem> list = new List<SelectListItem>();
			List<ProjeTipi> projeTipi;
			if (fakulte == 0)
				projeTipi = projeTipiIslemleri.VeriGetir("Etkin = 1 And Silindi = 0");
			else
				projeTipi = projeTipiIslemleri.VeriGetir("Etkin = 1 And Silindi = 0 And FakulteID = " + fakulte);

			foreach (var item in projeTipi)
			{
				SelectListItem sli = new SelectListItem()
				{
					Text = item.Ad,
					Value = item.ID.ToString()
				};

				list.Add(sli);
			}
			return new SelectList(list, "Value", "Text");
		}

		public static SelectList DekanListele()
		{
			List<SelectListItem> list = new List<SelectListItem>();

			foreach (var item in akademisyenIslemleri.VeriGetir("Silindi = 0 And Etkin = 1 And Yetki = " + (int)Yetkilendirme.Dekan))
			{
				SelectListItem sli = new SelectListItem()
				{
					Text = item.Unvan + " " + item.Ad + " " + item.Soyad,
					Value = item.ID.ToString()
				};

				list.Add(sli);
			}
			return new SelectList(list, "Value", "Text");
		}

		public static SelectList BolumBaskaniListele()
		{
			List<SelectListItem> list = new List<SelectListItem>();

			foreach (var item in akademisyenIslemleri.VeriGetir("Silindi = 0 And Etkin = 1 And Yetki = " + (int)Yetkilendirme.BolumBaskani))
			{
				SelectListItem sli = new SelectListItem()
				{
					Text = item.Unvan + " " + item.Ad + " " + item.Soyad,
					Value = item.ID.ToString()
				};

				list.Add(sli);
			}
			return new SelectList(list, "Value", "Text");
		}

		public static SelectList YetkiListele()
		{
			List<SelectListItem> list = new List<SelectListItem>();
			SelectListItem sli1 = new SelectListItem()
			{
				// Yetkilendirme.Dekan
				Text = "Dekan",
				Value = "1"
			};
			SelectListItem sli2 = new SelectListItem()
			{
				// Yetkilendirme.BolumBaskani
				Text = "Bölüm Başkanı",
				Value = "2"
			};
			SelectListItem sli3 = new SelectListItem()
			{
				//Yetkilendirme.Danisman
				Text = "Danışman",
				Value = "3"
			};
			SelectListItem sli4 = new SelectListItem()
			{
				//Yetkilendirme.Ogrenci
				Text = "Öğrenci",
				Value = "4"
			};
			list.Add(sli1);
			list.Add(sli2);
			list.Add(sli3);
			list.Add(sli4);
			return new SelectList(list, "Value", "Text");
		}

		public static SelectList ProjeDurumuListele()
		{
			List<SelectListItem> list = new List<SelectListItem>();
			SelectListItem sli1 = new SelectListItem()
			{
				// ProjeDurumu.Beklemede
				Text = ProjeDurumu.Beklemede.ToString(),
				Value = Convert.ToInt32(ProjeDurumu.Beklemede).ToString()
			};
			SelectListItem sli2 = new SelectListItem()
			{
				// ProjeDurumu.DanismanOnayi
				Text = "Danışman Onaylanan",
				Value = Convert.ToInt32(ProjeDurumu.DanismanOnayi).ToString()
			};
			SelectListItem sli3 = new SelectListItem()
			{
				// ProjeDurumu.BaskanOnayi
				Text = "Bölüm Başkanı Onaylanan",
				Value = Convert.ToInt32(ProjeDurumu.BaskanOnayi).ToString()
			};
			SelectListItem sli4 = new SelectListItem()
			{
				// ProjeDurumu.Onaylandi
				Text = "Onaylandı",
				Value = Convert.ToInt32(ProjeDurumu.Onaylandi).ToString()
			};
			SelectListItem sli5 = new SelectListItem()
			{
				// ProjeDurumu.Reddedildi
				Text = "Reddedildi",
				Value = Convert.ToInt32(ProjeDurumu.Reddedildi).ToString()
			};
			SelectListItem sli6 = new SelectListItem()
			{
				// ProjeDurumu.AcikProje
				Text = "Açık Proje",
				Value = Convert.ToInt32(ProjeDurumu.AcikProje).ToString()
			};
			SelectListItem sli7 = new SelectListItem()
			{
				// ProjeDurumu.KapaliProje
				Text = "Kapalı Proje",
				Value = Convert.ToInt32(ProjeDurumu.KapaliProje).ToString()
			};
			list.Add(sli1);
			list.Add(sli2);
			list.Add(sli3);
			list.Add(sli4);
			list.Add(sli5);
			list.Add(sli6);
			list.Add(sli7);

			return new SelectList(list, "Value", "Text");
		}
	}
}