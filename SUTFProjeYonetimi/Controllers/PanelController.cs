using SUTFProjeYonetimi.Filters;
using SUTFProjeYonetimi.Models;
using SUTFProjeYonetimi.Models.EkModel;
using System.Web.Mvc;
using static SUTFProjeYonetimi.App_Start.Tanimlamalar;
using System.Collections.Generic;
using System;
using SUTFProjeYonetimi.Models.Enum;
using SUTFProjeYonetimi.Models.ViewModel;

namespace SUTFProjeYonetimi.Controllers
{
	[HataFilter]
	public class PanelController : Controller
	{
		[AnlikOturumFilter]
		public ActionResult Anasayfa()
		{
			List<Duyuru> duyurular;
			switch (AnlikOturum.Kullanici.Yetki)
			{
				case (int)Yetkilendirme.SystemAdmin:
					duyurular = duyuruIslemleri.VeriGetir("Etkin = 1 Order By ID Desc Limit 10"); break;
				case (int)Yetkilendirme.Dekan:
					duyurular = duyuruIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " And Etkin = 1 Order By ID Desc Limit 10"); break;
				case (int)Yetkilendirme.BolumBaskani:
				case (int)Yetkilendirme.Danisman:
					duyurular = duyuruIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " And BolumID = " + AnlikOturum.Kullanici.Akademisyen.BolumID + " And Etkin = 1 Order By ID Desc Limit 10"); break;
				case (int)Yetkilendirme.Ogrenci:
					duyurular = duyuruIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.Ogrenci.FakulteID + " And BolumID = " + AnlikOturum.Kullanici.Ogrenci.BolumID + " And Etkin = 1 Order By ID Desc Limit 10"); break;
				default:
					duyurular = new List<Duyuru>(); break;
			}
			ViewData["Duyurular"] = duyurular;

			return View();
		}

		public ActionResult DuyuruDetay(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Anasayfa));

			Duyuru duyuru = duyuruIslemleri.Bul("ID = " + id + " And Etkin = 1");

			if (duyuru == null)
				return HttpNotFound();

			return View(duyuru);
		}

		public ActionResult Hata()
		{
			return View();
		}

		//[AnlikOturumFilter]
		//public ActionResult Profil()
		//{
		//	Kullanici kullanici = kullaniciIslemleri.Bul("ID = " + AnlikOturum.Kullanici.ID);

		//	if (kullanici == null)
		//		return HttpNotFound();

		//	return View(kullanici);
		//}

		//[AnlikOturumFilter]
		//public ActionResult ProfilDuzenle()
		//{
		//	Kullanici kullanici = kullaniciIslemleri.Bul("ID = " + AnlikOturum.Kullanici.ID);

		//	if (kullanici == null)
		//		return HttpNotFound();

		//	return View(kullanici);
		//}

		//[HttpPost]
		//[AnlikOturumFilter]
		//[ValidateAntiForgeryToken]
		//public ActionResult ProfilDuzenle(string sifre)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		int durum = kullaniciIslemleri.Guncelle("ID = " + AnlikOturum.Kullanici.ID, "Sifre", sifre, typeof(string));

		//		if (durum > 0)
		//			return RedirectToAction(nameof(Profil));
		//	}
		//	return View();
		//}

		public ActionResult GirisYap()
		{
			if (AnlikOturum.Kullanici != null)
				return RedirectToAction(nameof(Anasayfa));
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult GirisYap(KullaniciGiris kullaniciGiris)
		{
			Kullanici kullanici;
			if (ModelState.IsValid)
			{
				Ogrenci ogrenci = null;
				Akademisyen akademisyen = null;

				ogrenci = ogrenciIslemleri.Bul("OgrenciNo = '" + kullaniciGiris.OgrenciNo + "' And Sifre = '" + kullaniciGiris.Sifre + "'");

				if (ogrenci == null)
				{
					akademisyen = akademisyenIslemleri.Bul("TCKNO = '" + kullaniciGiris.OgrenciNo + "' And Sifre = '" + kullaniciGiris.Sifre + "'");

					if (akademisyen == null)
					{
						ViewBag.Hata = "Girdiğiniz değerlerle eşleşen bir kullanıcı bulunamadı. Lütfen tekrar deneyiniz...";
						return View(kullaniciGiris);
					}
					else
					{
						kullanici = new Kullanici()
						{
							Akademisyen = akademisyen,
							Ogrenci = null,
							Yetki = akademisyen.Yetki
						};
					}
				}
				else
				{
					kullanici = new Kullanici()
					{
						Akademisyen = null,
						Ogrenci = ogrenci,
						Yetki = (int)Yetkilendirme.Ogrenci
					};
				}

				if (kullanici == null)
				{
					ViewBag.Hata = "Girdiğiniz değerlerle eşleşen bir kullanıcı bulunamadı. Lütfen tekrar deneyiniz...";
					return View(kullaniciGiris);
				}

				Donem donem = donemIslemleri.Bul("Etkin = 1");

				Session["Donem"] = donem;
				Session["Kullanici"] = kullanici;
				Session.Timeout = 120;
				return RedirectToAction(nameof(Anasayfa));
			}
			return View(kullaniciGiris);
		}

		[AnlikOturumFilter]
		public ActionResult CikisYap()
		{
			AnlikOturum.Clear();
			return RedirectToAction(nameof(GirisYap));
		}

		#region DuyuruIslemleri

		[AnlikOturumFilter]
		[DanismanFilter]
		public ActionResult Duyuru()
		{
			List<Duyuru> duyurular;

			switch (AnlikOturum.Kullanici.Yetki)
			{
				case (int)Yetkilendirme.SystemAdmin:
					duyurular = duyuruIslemleri.VeriGetir(); break;
				case (int)Yetkilendirme.Dekan:
					duyurular = duyuruIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID); break;
				case (int)Yetkilendirme.BolumBaskani:
				case (int)Yetkilendirme.Danisman:
					duyurular = duyuruIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " AND BolumID = " + AnlikOturum.Kullanici.Akademisyen.BolumID); break;
				case (int)Yetkilendirme.Ogrenci:
					return RedirectToAction("Anasayfa", "Panel");
				default: return HttpNotFound();
			}
			return View(duyurular);
		}

		[AnlikOturumFilter]
		[DanismanFilter]
		public ActionResult DuyuruEkle()
		{
			ViewData["Fakulte"] = SLOlusturma.FakulteListele();
			ViewData["Bolum"] = SLOlusturma.BolumListele();

			return View("DuyuruEkleDuzenle");
		}

		[AnlikOturumFilter]
		[DanismanFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DuyuruEkle(Duyuru duyuru)
		{
			if (ModelState.IsValid)
			{
				duyuru.Tarih = DateTime.Now;

				int durum = duyuruIslemleri.Ekle(duyuru);

				if (durum > 0)
					return RedirectToAction(nameof(Duyuru));
			}

			return View("DuyuruEkleDuzenle", duyuru);
		}

		[AnlikOturumFilter]
		[DanismanFilter]
		public ActionResult DuyuruDuzenle(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Duyuru));

			Duyuru duyuru = duyuruIslemleri.Bul("ID = " + id);

			if (duyuru == null)
				return HttpNotFound();

			return View("DuyuruEkleDuzenle", duyuru);
		}

		[AnlikOturumFilter]
		[DanismanFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DuyuruDuzenle(int id, Duyuru gelenDuyuru)
		{
			if (ModelState.IsValid)
			{
				Duyuru duyuru = duyuruIslemleri.Bul("ID = " + id);
				duyuru.Baslik = gelenDuyuru.Baslik;
				duyuru.BolumID = gelenDuyuru.BolumID;
				duyuru.FakulteID = gelenDuyuru.FakulteID;
				duyuru.Metin = gelenDuyuru.Metin;
				duyuru.Tarih = gelenDuyuru.Tarih;

				int durum = duyuruIslemleri.Guncelle("ID = " + id, duyuru);

				if (durum > 0)
					return RedirectToAction(nameof(Duyuru));
			}
			return View("DuyuruEkleDuzenle", gelenDuyuru);
		}

		[AnlikOturumFilter]
		[DanismanFilter]
		public ActionResult DuyuruSil(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Duyuru));

			Duyuru duyuru = duyuruIslemleri.Bul("ID = " + id);

			if (duyuru == null)
				return HttpNotFound();

			return View();
		}

		[AnlikOturumFilter]
		[DanismanFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DuyuruSil(int id, Duyuru duyuru)
		{
			int durum = duyuruIslemleri.Sil("ID = " + id);

			if (durum > 0)
				return RedirectToAction(nameof(Duyuru));

			return View();
		}

		#endregion

		#region FakulteVeBolum

		[SysAdminFilter]
		public ActionResult FakulteListesi()
		{
			List<VFakulte> fakulteler = vfakulteIslemleri.VeriGetir("Silindi = 0");

			return View(fakulteler);
		}

		[SysAdminFilter]
		public ActionResult FakulteEkle()
		{
			ViewData["Dekanlar"] = SLOlusturma.DekanListele();

			return View("FakulteEkleDuzenle");
		}

		[SysAdminFilter]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult FakulteEkle(Fakulte fakulte)
		{
			if (ModelState.IsValid)
			{
				fakulte.Silindi = false;
				int durum = fakulteIslemleri.Ekle(fakulte);

				if (durum > 0)
					return RedirectToAction(nameof(FakulteListesi));
				else
					ViewBag.Hata = "İşlem Gerçekleştirilemedi! Lütfen tekrar deneyiniz.";
			}
			ViewData["Dekanlar"] = SLOlusturma.DekanListele();
			return View("FakulteEkleDuzenle", fakulte);
		}

		[SysAdminFilter]
		public ActionResult FakulteDuzenle(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(FakulteListesi));

			Fakulte fakulte = fakulteIslemleri.Bul("ID = " + id);

			if (fakulte == null)
				return HttpNotFound();

			ViewData["Dekanlar"] = SLOlusturma.DekanListele();

			return View("FakulteEkleDuzenle", fakulte);
		}

		[SysAdminFilter]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult FakulteDuzenle(int id, Fakulte gelenFakulte)
		{
			if (ModelState.IsValid)
			{
				Fakulte fakulte = fakulteIslemleri.Bul("ID = " + id);
				fakulte.Ad = gelenFakulte.Ad;
				fakulte.KisaKod = gelenFakulte.KisaKod;

				int durum = fakulteIslemleri.Guncelle("ID = " + id, fakulte);

				if (durum > 0)
					return RedirectToAction(nameof(FakulteListesi));
				else
					ViewBag.Hata = "İşlem Gerçekleştirilemedi! Lütfen tekrar deneyiniz.";
			}
			ViewData["Dekanlar"] = SLOlusturma.DekanListele();
			return View("FakulteEkleDuzenle", gelenFakulte);
		}

		[SysAdminFilter]
		public ActionResult FakulteSil(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(FakulteListesi));

			VFakulte fakulte = vfakulteIslemleri.Bul("ID = " + id);

			if (fakulte == null)
				return HttpNotFound();

			return View(fakulte);
		}

		[SysAdminFilter]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult FakulteSil(int id, VFakulte fakulte)
		{
			int durum = fakulteIslemleri.Guncelle("ID = " + id, "Silidi", true, typeof(bool));

			if (durum > 0)
				return RedirectToAction(nameof(FakulteListesi));
			else
				ViewBag.Hata = "İşlem Gerçekleştirilemedi! Lütfen tekrar deneyiniz.";

			return View(fakulte);
		}

		[SysAdminFilter]
		public ActionResult BolumListesi()
		{
			List<VBolum> bolumler = vbolumIslemleri.VeriGetir("Silindi = 0");

			return View(bolumler);
		}

		[SysAdminFilter]
		public ActionResult BolumEkle()
		{
			ViewData["BolumBaskanlari"] = SLOlusturma.BolumBaskaniListele();
			ViewData["Fakulteler"] = SLOlusturma.FakulteListele();
			return View("BolumEkleDuzenle");
		}

		[SysAdminFilter]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult BolumEkle(Bolum bolum)
		{
			if (ModelState.IsValid)
			{
				bolum.Silindi = false;
				int durum = bolumIslemleri.Ekle(bolum);

				if (durum > 0)
					return RedirectToAction(nameof(BolumListesi));
				else
					ViewBag.Hata = "İşlem Gerçekleştirilemedi! Lütfen tekrar deneyiniz.";
			}
			ViewData["BolumBaskanlari"] = SLOlusturma.BolumBaskaniListele();
			ViewData["Fakulteler"] = SLOlusturma.FakulteListele();
			return View("BolumEkleDuzenle", bolum);
		}

		[SysAdminFilter]
		public ActionResult BolumDuzenle(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(BolumListesi));

			Bolum bolum = bolumIslemleri.Bul("ID = " + id);

			if (bolum == null)
				return HttpNotFound();

			ViewData["BolumBaskanlari"] = SLOlusturma.BolumBaskaniListele();
			ViewData["Fakulteler"] = SLOlusturma.FakulteListele();
			return View("BolumEkleDuzenle", bolum);
		}

		[SysAdminFilter]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult BolumDuzenle(int id, Bolum gelenBolum)
		{
			if (ModelState.IsValid)
			{
				Bolum bolum = bolumIslemleri.Bul("ID = " + id);
				bolum.Ad = gelenBolum.Ad;
				bolum.KisaKod = gelenBolum.KisaKod;

				int durum = bolumIslemleri.Guncelle("ID = " + id, bolum);

				if (durum > 0)
					return RedirectToAction(nameof(BolumListesi));
				else
					ViewBag.Hata = "İşlem Gerçekleştirilemedi! Lütfen tekrar deneyiniz.";
			}
			ViewData["BolumBaskanlari"] = SLOlusturma.BolumBaskaniListele();
			ViewData["Fakulteler"] = SLOlusturma.FakulteListele();
			return View("BolumEkleDuzenle", gelenBolum);
		}

		[SysAdminFilter]
		public ActionResult BolumSil(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(BolumListesi));

			VBolum bolum = vbolumIslemleri.Bul("ID = " + id);

			if (bolum == null)
				return HttpNotFound();

			return View(bolum);
		}

		[SysAdminFilter]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult BolumSil(int id, VBolum bolum)
		{
			int durum = bolumIslemleri.Guncelle("ID = " + id, "Silidi", true, typeof(bool));

			if (durum > 0)
				return RedirectToAction(nameof(BolumListesi));
			else
				ViewBag.Hata = "İşlem Gerçekleştirilemedi! Lütfen tekrar deneyiniz.";

			return View(bolum);
		}

		#endregion

		#region DonemIslemleri

		[SysAdminFilter]
		public ActionResult Donemler()
		{
			List<Donem> donemler = donemIslemleri.VeriGetir("Silindi = 0");
			return View(donemler);
		}

		[SysAdminFilter]
		public ActionResult DonemEkle()
		{
			return View("DonemEkleDuzenle");
		}

		[SysAdminFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DonemEkle(Donem donem)
		{
			if (ModelState.IsValid)
			{
				int durum = donemIslemleri.Ekle(donem);

				if (durum > 0)
					return RedirectToAction(nameof(Donemler));
			}
			return View("DonemEkleDuzenle", donem);
		}

		[SysAdminFilter]
		public ActionResult DonemDuzenle(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Donemler));

			Donem donem = donemIslemleri.Bul("ID = " + id);

			if (donem == null)
				return HttpNotFound();

			return View("DonemEkleDuzenle", donem);
		}

		[SysAdminFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DonemDuzenle(int? id, Donem gelenDonem)
		{
			if (ModelState.IsValid)
			{
				Donem donem = donemIslemleri.Bul("ID = " + id);
				donem.Ad = gelenDonem.Ad;
				donem.BaslangicTarihi = gelenDonem.BaslangicTarihi;
				donem.BitisTarihi = gelenDonem.BitisTarihi;

				int durum = donemIslemleri.Guncelle("ID = " + id, donem);

				if (durum > 0)
					return RedirectToAction(nameof(Donemler));
			}
			return View("DonemEkleDuzenle", gelenDonem);
		}

		[SysAdminFilter]
		public ActionResult DonemSil(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Donemler));

			Donem donem = donemIslemleri.Bul("ID = " + id);

			if (donem == null)
				return HttpNotFound();

			return View(donem);
		}

		[SysAdminFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DonemSil(int id, Donem donem)
		{
			int durum = donemIslemleri.Guncelle("ID = " + id, "Silindi", true, typeof(bool));

			if (durum > 0)
				return RedirectToAction(nameof(Donemler));

			return View(donem);
		}

		public ActionResult EtkinDonemAtama(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Donemler));

			Donem donem = donemIslemleri.Bul("ID = " + id);

			if (donem == null)
				return HttpNotFound();

			return View(donem);
		}

		[HttpPost]
		public ActionResult EtkinDonemAtama(int id, Donem donem)
		{
			donemIslemleri.HamSorgu("Update " + donemIslemleri.TabloAdi + " Set Etkin = 0");
			int durum = donemIslemleri.HamSorgu("Update " + donemIslemleri.TabloAdi + " Set Etkin = 1 Where ID = " + id);

			if (durum > 0)
				return RedirectToAction(nameof(Donemler));

			return View();
		}

		#endregion

		/* #region KullaniciIslemleri

		[SysAdminFilter]
		public ActionResult Kullanicilar()
		{
			List<Kullanici> kullanicilar = kullaniciIslemleri.VeriGetir("Silindi = 0");
			return View(kullanicilar);
		}

		[SysAdminFilter]
		public ActionResult KullaniciEkle()
		{
			ViewData["Yetkiler"] = SLOlusturma.YetkiListele();
			ViewData["Akademisyen"] = SLOlusturma.EklenecekKullaniciListele();
			return View("KullaniciEkleDuzenle");
		}

		[SysAdminFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult KullaniciEkle(Kullanici kullanici)
		{
			if (ModelState.IsValid)
			{
				kullanici.Silindi = false;
				int durum = kullaniciIslemleri.Ekle(kullanici);

				if (durum > 0)
					return RedirectToAction(nameof(Kullanicilar));
			}
			ViewData["Yetkiler"] = SLOlusturma.YetkiListele();
			ViewData["Akademisyen"] = SLOlusturma.EklenecekKullaniciListele();
			return View("KullaniciEkleDuzenle", kullanici);
		}

		[SysAdminFilter]
		public ActionResult KullaniciDuzenle(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Kullanicilar));

			Kullanici kullanici = kullaniciIslemleri.Bul("ID = " + id);

			if (kullanici == null)
				return HttpNotFound();

			ViewData["Yetkiler"] = SLOlusturma.YetkiListele();
			ViewData["Akademisyen"] = SLOlusturma.EklenecekKullaniciListele();
			return View("KullaniciEkleDuzenle", kullanici);
		}

		[SysAdminFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult KullaniciDuzenle(int id, Kullanici gelenKullanici)
		{
			if (ModelState.IsValid)
			{
				Kullanici kullanici = kullaniciIslemleri.Bul("ID = " + id);
				kullanici.Etkin = gelenKullanici.Etkin;
				kullanici.KullaniciAdi = gelenKullanici.KullaniciAdi;
				kullanici.NitelikID = gelenKullanici.NitelikID;
				kullanici.Sifre = gelenKullanici.Sifre;
				kullanici.Yetki = gelenKullanici.Yetki;

				int durum = kullaniciIslemleri.Guncelle("ID = " + id, kullanici);

				if (durum > 0)
					return RedirectToAction(nameof(Kullanicilar));
			}
			ViewData["Yetkiler"] = SLOlusturma.YetkiListele();
			ViewData["Akademisyen"] = SLOlusturma.EklenecekKullaniciListele();
			return View("KullaniciEkleDuzenle", gelenKullanici);
		}

		[SysAdminFilter]
		public ActionResult KullaniciSil(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Kullanicilar));

			Kullanici kullanici = kullaniciIslemleri.Bul("ID = " + id);

			if (kullanici == null)
				return HttpNotFound();

			return View(kullanici);
		}

		[SysAdminFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult KullaniciSil(int id, Kullanici kullanici)
		{
			int durum = kullaniciIslemleri.Guncelle("ID = " + id, "Silindi", true, typeof(bool));

			if(durum > 0)
				return RedirectToAction(nameof(Kullanicilar));

			return View();
		}

		public ActionResult YetkiyeGoreKullanici(int yetki)
		{
			if (yetki == (int)Yetkilendirme.Ogrenci)
			{
				List<Ogrenci> ogrenciler = ogrenciIslemleri.VeriGetir("Silindi = 0 And Etkin = 1");
				return Json(ogrenciler, JsonRequestBehavior.AllowGet);
			}
			else if (yetki <= (int)Yetkilendirme.Danisman)
			{
				List<Akademisyen> akademisyenler = akademisyenIslemleri.VeriGetir("Silindi = 0 And Etkin = 1");
				return Json(akademisyenler, JsonRequestBehavior.AllowGet);
			}
			return null;
		}

		#endregion */ 
	}
}