using SUTFProjeYonetimi.Filters;
using SUTFProjeYonetimi.Models;
using SUTFProjeYonetimi.Models.EkModel;
using SUTFProjeYonetimi.Models.Enum;
using SUTFProjeYonetimi.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SUTFProjeYonetimi.App_Start.Tanimlamalar;

namespace SUTFProjeYonetimi.Controllers
{
	[AnlikOturumFilter]
	public class OgrenciController : Controller
	{
		#region CRUD Islemleri

		/*
		 *	CRUD İşlemlerini yalnızca Akademisyen gerçekleştirebilir. Öğrenciler bu işlemleri gerçekleştiremezler.
		 * 
		 *	Yetkilendirmeler
		 *	-------------------
		 *	1 - Dekan -> Bütün Öğrencileri Görebilir
		 *	2 - Bölüm Başkanı -> Sadece Kendi Bölümünün Öğrencilerini Görebilir
		 *	3 - Danışman -> Sadece Kendi Danışmanlık Yaptığı Öğrencileri Görebilir
		 * 
		 */

		[DanismanFilter]
		public ActionResult Liste()
		{
			List<VOgrenci> ogrenciler;
			switch (AnlikOturum.Kullanici.Yetki)
			{
				case (int)Yetkilendirme.SystemAdmin:
					ogrenciler = vogrenciIslemleri.VeriGetir(); break;
				case (int)Yetkilendirme.Dekan:
					ogrenciler = vogrenciIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " And Silindi = 0"); break;
				case (int)Yetkilendirme.BolumBaskani:
					ogrenciler = vogrenciIslemleri.VeriGetir("Silindi = 0 AND FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " AND BolumID = " + AnlikOturum.Kullanici.Akademisyen.BolumID); break;
				case (int)Yetkilendirme.Danisman:
					ogrenciler = vogrenciIslemleri.VeriGetir("Silindi = 0 AND DanismanID = " + AnlikOturum.Kullanici.Akademisyen.ID); break;
				case (int)Yetkilendirme.Ogrenci:
					return RedirectToAction("Anasayfa", "Panel");
				default: return HttpNotFound();
			}
			return View(ogrenciler);
		}

		[DanismanFilter]
		public ActionResult Detay(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Liste));

			VOgrenci ogrenci;
			switch (AnlikOturum.Kullanici.Yetki)
			{
				case (int)Yetkilendirme.SystemAdmin:
					ogrenci = vogrenciIslemleri.Bul("ID = " + id); break;
				case (int)Yetkilendirme.Dekan:
					ogrenci = vogrenciIslemleri.Bul("FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " And ID = " + id + " AND Silindi = 0");
					break;
				case (int)Yetkilendirme.BolumBaskani:
					ogrenci = vogrenciIslemleri.Bul("FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " And BolumID = " + AnlikOturum.Kullanici.Akademisyen.BolumID + " And ID = " + id + " AND Silindi = 0");
					break;
				case (int)Yetkilendirme.Danisman:
					ogrenci = vogrenciIslemleri.Bul("ID = " + id + " AND Silindi = 0 AND DanismanID = " + AnlikOturum.Kullanici.Akademisyen.ID);
					break;
				case (int)Yetkilendirme.Ogrenci:
					return RedirectToAction("Anasayfa", "Panel");
				default: return HttpNotFound();
			}

			if (ogrenci == null)
				return HttpNotFound();

			return View(ogrenci);
		}

		[DanismanFilter]
		public ActionResult Ekle()
		{
			ViewData["Fakulte"] = SLOlusturma.FakulteListele();
			ViewData["Bolum"] = SLOlusturma.BolumListele();
			ViewData["Sinif"] = SLOlusturma.SinifListele();
			ViewData["OgrenimTipi"] = SLOlusturma.OgrenimTipiListele();
			return View("EkleDuzenle");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[DanismanFilter]
		public ActionResult Ekle(Ogrenci ogrenci)
		{
			if (ModelState.IsValid)
			{
				int durum = ogrenciIslemleri.Ekle(ogrenci);

				if (durum > 0)
					return RedirectToAction(nameof(Liste));
			}
			ViewData["Fakulte"] = SLOlusturma.FakulteListele();
			ViewData["Bolum"] = SLOlusturma.BolumListele();
			ViewData["Sinif"] = SLOlusturma.SinifListele();
			ViewData["OgrenimTipi"] = SLOlusturma.OgrenimTipiListele();

			return View("EkleDuzenle", ogrenci);
		}

		[DanismanFilter]
		public ActionResult Duzenle(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Liste));

			VOgrenci ogrenci;
			switch (AnlikOturum.Kullanici.Yetki)
			{
				case (int)Yetkilendirme.SystemAdmin:
				case (int)Yetkilendirme.Dekan:
					ogrenci = vogrenciIslemleri.Bul("ID = " + id); break;
				case (int)Yetkilendirme.BolumBaskani:
					ogrenci = vogrenciIslemleri.Bul("ID = " + id + " AND FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " AND BolumID = " + AnlikOturum.Kullanici.Akademisyen.BolumID);
					break;
				case (int)Yetkilendirme.Danisman:
					ogrenci = vogrenciIslemleri.Bul("ID = " + id + " AND Silindi = 0 AND DanismanID = " + AnlikOturum.Kullanici.Akademisyen.ID);
					break;
				default: return HttpNotFound();
			}

			if (ogrenci == null)
				return HttpNotFound();

			ViewData["Fakulte"] = SLOlusturma.FakulteListele();
			ViewData["Bolum"] = SLOlusturma.BolumListele();
			ViewData["Sinif"] = SLOlusturma.SinifListele();
			ViewData["OgrenimTipi"] = SLOlusturma.OgrenimTipiListele();

			return View("EkleDuzenle", ogrenci);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[DanismanFilter]
		public ActionResult Duzenle(int id, VOgrenci gelenOgrenci)
		{
			if (ModelState.IsValid)
			{
				Ogrenci ogrenci = ogrenciIslemleri.Bul("ID = " + id);
				ogrenci.Ad = gelenOgrenci.Ad;
				ogrenci.BolumID = gelenOgrenci.BolumID;
				ogrenci.FakulteID = gelenOgrenci.FakulteID;
				ogrenci.OgrenciNo = gelenOgrenci.OgrenciNo;
				ogrenci.OgrenimTipi = gelenOgrenci.OgrenimTipi;
				ogrenci.Sinif = gelenOgrenci.Sinif;
				ogrenci.Soyad = gelenOgrenci.Soyad;
				ogrenci.TCKNO = gelenOgrenci.TCKNO;
				ogrenci.Sifre = gelenOgrenci.Sifre;

				int durum = ogrenciIslemleri.Guncelle("ID = " + id, ogrenci);
				if (durum > 0)
					return RedirectToAction(nameof(Liste));
			}
			ViewData["Fakulte"] = SLOlusturma.FakulteListele();
			ViewData["Bolum"] = SLOlusturma.BolumListele();
			ViewData["Sinif"] = SLOlusturma.SinifListele();
			ViewData["OgrenimTipi"] = SLOlusturma.OgrenimTipiListele();
			return View("EkleDuzenle", gelenOgrenci);
		}

		public ActionResult Sil(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Liste));

			VOgrenci ogrenci;
			switch (AnlikOturum.Kullanici.Yetki)
			{
				case (int)Yetkilendirme.SystemAdmin:
				case (int)Yetkilendirme.Dekan:
					ogrenci = vogrenciIslemleri.Bul("ID = " + id + " And Silindi = 0"); break;
				case (int)Yetkilendirme.BolumBaskani:
					ogrenci = vogrenciIslemleri.Bul("ID = " + id + " AND FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " AND BolumID = " + AnlikOturum.Kullanici.Akademisyen.BolumID + " And Silindi = 0"); break;
				case (int)Yetkilendirme.Danisman:
					ogrenci = vogrenciIslemleri.Bul("ID = " + id + "  AND DanismanID = " + AnlikOturum.Kullanici.Akademisyen.ID + " And Silindi = 0");
					break;
				default: return HttpNotFound();
			}

			if (ogrenci == null)
				return HttpNotFound();

			return View(ogrenci);
		}

		// Silme işlemi asla gerçekleşmiyor. Yalnızca öğrenci silinmiş gibi gösterilerek işlem yapılıyor.

		[HttpPost]
		[ValidateAntiForgeryToken]
		[DanismanFilter]
		public ActionResult Sil(int id, Ogrenci gelenOgrenci)
		{
			Ogrenci ogrenci = ogrenciIslemleri.Bul("ID = " + id);
			int durum = ogrenciIslemleri.Guncelle("ID = " + id, "Silindi", true, typeof(bool));

			if (durum > 0)
				return RedirectToAction(nameof(Liste));

			return View(gelenOgrenci);
		}

		#endregion


		#region OgrenciHareketleri

		/*
		 * 
		 *	Bu işlemler yalnızca öğrenci tarafından gerçekleştirilir. Diğer yetkiye sahip kişiler sadece proje
		 *	controller içerisinde yer alan metotlar ile onaylama ve reddetme işlemi gerçekleştirebilir.
		 *	
		 */

		[OgrenciFilter]
		public ActionResult Proje()
		{
			List<VProje> projeler = vprojeIslemleri.VeriGetir("OgrenciID = " + AnlikOturum.Kullanici.Ogrenci.ID);
			//List<VProjeOneri> projeOnerileri = vprojeOneriIslemleri.VeriGetir("OgrenciID = " + AnlikOturum.Kullanici.NitelikID);

			//ViewData["ProjeOnerileri"] = projeOnerileri;

			return View(projeler);
		}
		
		[OgrenciFilter]
		public ActionResult DanismanDegisiklikTalebi()
		{
			ViewData["Danismanlar"] = SLOlusturma.AkademisyenListele();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken] 
		[OgrenciFilter]
		public ActionResult DanismanDegisiklikTalebi(int danisman)
		{
			if (danisman <= 0)
				return RedirectToAction("Anasayfa", "Panel");
			else
			{
				// TODO : Uygun Model Hazırlanacak
			}
			ViewData["Danismanlar"] = SLOlusturma.AkademisyenListele();
			return View(danisman);
		}

		public ActionResult RaporTeslim(int? id)
		{
			if (id == null)
				return RedirectToAction("Anasayfa", "Panel");

			Proje proje = projeIslemleri.Bul("ID = " + id + " And Silindi = 0");

			if (proje == null)
				return HttpNotFound();

			return View(proje);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[OgrenciFilter]
		public ActionResult RaporTeslim(int id, HttpPostedFileBase rapor)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (rapor.ContentLength > 0)
					{
						string _FileName = Path.GetFileName(rapor.FileName);
						string _path = Path.Combine(Server.MapPath("~/Raporlar"), _FileName);

						Proje proje = projeIslemleri.Bul("ID = " + id);
						proje.Rapor = _path;
						int durum = projeIslemleri.Guncelle("ID = " + id, "Rapor", _path, typeof(string));

						if(durum > 0)
							rapor.SaveAs(_path);
					}
					ViewBag.Message = "Dosya Yükleme İşlemi Başarılı!";
					return View();
				}
				catch
				{
					ViewBag.Hata = "Dosya Yükleme İşlemi Başarısız!";
					return View();
				}
			}
			return View();
		}

		#endregion

		#region ProjeOneriIslemleri

		/*
		 *	Bu işlemleri sadece öğrenciler gerçekleştirebilir. Anasayfada yer alan kısımda bu işlemlere erişebilir.
		 */

		[OgrenciFilter]
		public ActionResult ProjeOnerisiDetay(int? id)
		{
			if (id == null)
				return RedirectToAction("Anasayfa", "Panel");

			ProjeOneri projeOneri = projeOneriIslemleri.Bul("ID = " + id);

			if (projeOneri == null)
				return HttpNotFound();

			return View(projeOneri);
		}

		[OgrenciFilter]
		public ActionResult ProjeOnerisiOlustur()
		{
			return View("ProjeOnerisiIslemleri");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[OgrenciFilter]
		public ActionResult ProjeOnerisiOlustur(ProjeOneri projeOneri)
		{
			if (ModelState.IsValid)
			{
				projeOneri.Tarih = DateTime.Now;
				projeOneriIslemleri.Ekle(projeOneri);

				return RedirectToAction("Anasayfa", "Panel");
			}
			return View("ProjeOnerisiIslemleri", projeOneri);
		}

		[OgrenciFilter]
		public ActionResult ProjeOnerisiDuzenle(int? id)
		{
			if (id == null)
				return RedirectToAction("Anasayfa", "Panel");

			ProjeOneri projeOneri = projeOneriIslemleri.Bul("ID = " + id);

			if (projeOneri == null)
				return HttpNotFound();

			return View("ProjeOnerisiIslemleri", projeOneri);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[OgrenciFilter]
		public ActionResult ProjeOnerisiDuzenle(int id, ProjeOneri gelenOneri)
		{
			if (ModelState.IsValid)
			{
				ProjeOneri projeOneri = projeOneriIslemleri.Bul("ID = " + id);
				projeOneri.CevreselEtkileri = gelenOneri.CevreselEtkileri;
				projeOneri.EtikSakincalari = gelenOneri.EtikSakincalari;
				projeOneri.MaliyetArastirmasi = gelenOneri.MaliyetArastirmasi;
				projeOneri.ProjeAdi = gelenOneri.ProjeAdi;
				projeOneri.ProjeKonusuAmaci = gelenOneri.ProjeKonusuAmaci;
				projeOneri.YararlanilanKaynaklar = gelenOneri.YararlanilanKaynaklar;

				int durum = projeOneriIslemleri.Guncelle("ID = " + id, projeOneri);

				if (durum > 0)
					return RedirectToAction("Anasayfa", "Panel");
				else
					ViewBag.Hata = "İşleminiz gerçekleştirilemedi. Lütfen tekrar deneyiniz.";
			}
			return View("ProjeOnerisiIslemleri", gelenOneri);
		}

		[OgrenciFilter]
		public ActionResult ProjeOnerisiSil(int? id)
		{
			if (id == null)
				return RedirectToAction("Anasayfa", "Panel");

			ProjeOneri projeOneri = projeOneriIslemleri.Bul("ID = " + id);

			if (projeOneri == null || projeOneri.Durum == (int)ProjeOneriDurumu.Onaylandi)
			{
				ViewBag.Hata = "Onaylanan proje önerilerine silme işlemi gerçekleştirilemez.";
				return RedirectToAction("Anasayfa", "Panel");
			}

			return View(projeOneri);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[OgrenciFilter]
		public ActionResult ProjeOnerisiSil(int id, ProjeOneri projeOneri)
		{
			if (projeOneriIslemleri.Sil("ID = " + id) > 0)
			{
				ViewBag.Mesaj = "İşleminiz başarılı bir şekilde gerçekleşmiştir.";
				return RedirectToAction("Anasayfa", "Panel");
			}
			return View();
		}

		#endregion
	}
}