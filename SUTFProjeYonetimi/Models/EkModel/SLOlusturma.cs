using SUTFProjeYonetimi.Models.Enum;
using System.Collections.Generic;
using System.Web.Mvc;
using static SUTFProjeYonetimi.App_Start.Tanimlamalar;

namespace SUTFProjeYonetimi.Models.EkModel
{
	public static class SLOlusturma
	{
		public static SelectList AkademisyenListele()
		{
			List<SelectListItem> list = new List<SelectListItem>();

			foreach (var item in akademisyenIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.OFakulteID + " And BolumID = " + AnlikOturum.Kullanici.OBolumID))
			{
				SelectListItem sli = new SelectListItem()
				{
					Text = item.Ad + " " + item.Soyad,
					Value = item.ID.ToString()
				};

				list.Add(sli);
			}
			return new SelectList(list, "Value", "Text");
		}

		public static SelectList FakulteListele()
		{
			List<SelectListItem> list = new List<SelectListItem>();

			foreach (var item in fakulteIslemleri.VeriGetir())
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

			foreach (var item in bolumIslemleri.VeriGetir())
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

		public static SelectList ProjeTipiListele()
		{
			List<SelectListItem> list = new List<SelectListItem>();
			SelectListItem sli1 = new SelectListItem()
			{
				Text = "Proje Yönetimi",
				Value = "1"
			};
			SelectListItem sli2 = new SelectListItem()
			{
				Text = "Yazılım Projesi",
				Value = "2"
			};
			SelectListItem sli3 = new SelectListItem()
			{
				Text = "Donanım Projesi",
				Value = "3"
			};
			SelectListItem sli4 = new SelectListItem()
			{
				Text = "Bitirme Projesi",
				Value = "4"
			};

			list.Add(sli1);
			list.Add(sli2);
			list.Add(sli3);
			list.Add(sli4);

			return new SelectList(list, "Value", "Text");
		}

		public static SelectList DekanListele()
		{
			List<SelectListItem> list = new List<SelectListItem>();

			foreach (var item in vakademisyenIslemleri.VeriGetir("Yetki = " + (int)Yetkilendirme.Dekan))
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

			foreach (var item in vakademisyenIslemleri.VeriGetir("Yetki = " + (int)Yetkilendirme.BolumBaskani))
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
	}
}