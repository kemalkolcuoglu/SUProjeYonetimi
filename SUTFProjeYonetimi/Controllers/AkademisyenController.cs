using SUTFProjeYonetimi.Filters;
using SUTFProjeYonetimi.Models;
using SUTFProjeYonetimi.Models.EkModel;
using SUTFProjeYonetimi.Models.Enum;
using SUTFProjeYonetimi.Models.ViewModel;
using System.Collections.Generic;
using System.Web.Mvc;
using static SUTFProjeYonetimi.App_Start.Tanimlamalar;

namespace SUTFProjeYonetimi.Controllers
{
	[AnlikOturumFilter]
	[DanismanFilter]
	public class AkademisyenController : Controller
	{
		public ActionResult Liste()
		{
			List<Akademisyen> akademisyen;
			switch (AnlikOturum.Kullanici.Yetki)
			{
				case (int)Yetkilendirme.SystemAdmin:
					akademisyen = akademisyenIslemleri.VeriGetir(); break;
				case (int)Yetkilendirme.Dekan:
					akademisyen = akademisyenIslemleri.VeriGetir("Silindi = 0"); break;
				case (int)Yetkilendirme.BolumBaskani:
					akademisyen = akademisyenIslemleri.VeriGetir("Silindi = 0 AND FakulteID = " + AnlikOturum.Kullanici.AFakulteID + " AND BolumID = " + AnlikOturum.Kullanici.ABolumID); break;
				case (int)Yetkilendirme.Danisman:
				case (int)Yetkilendirme.Ogrenci:
					return RedirectToAction("Anasayfa", "Panel");
				default: return HttpNotFound();
			}
			return View(akademisyen);
		}

		public ActionResult Detay(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Liste));

			Akademisyen akademisyen;
			switch (AnlikOturum.Kullanici.Yetki)
			{
				case (int)Yetkilendirme.SystemAdmin:
					akademisyen = akademisyenIslemleri.Bul("ID = " + id); break;
				case (int)Yetkilendirme.Dekan:
					akademisyen = akademisyenIslemleri.Bul("ID = " + id + " AND Silindi = 0");
					break;
				case (int)Yetkilendirme.BolumBaskani:
					akademisyen = akademisyenIslemleri.Bul("ID = " + id + " AND Silindi = 0 AND FakulteID = " + AnlikOturum.Kullanici.AFakulteID + " AND BolumID = " + AnlikOturum.Kullanici.ABolumID);
					break;
				case (int)Yetkilendirme.Danisman:
				case (int)Yetkilendirme.Ogrenci:
					return RedirectToAction("Anasayfa", "Panel");
				default: return HttpNotFound();
			}

			if (akademisyen == null)
				return HttpNotFound();

			return View(akademisyen);
		}

		public ActionResult Ekle()
		{
			ViewData["Fakulte"] = SLOlusturma.FakulteListele();
			ViewData["Bolum"] = SLOlusturma.BolumListele();
			return View("EkleDuzenle");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Ekle(Akademisyen akademisyen)
		{
			if (ModelState.IsValid)
			{
				int durum = akademisyenIslemleri.Ekle(akademisyen);

				if (durum > 0)
					return RedirectToAction(nameof(Liste));

				ViewBag.Hata = "İşleminiz gerçekleştirilemedi. Lütfen tekrar deneyiniz";
			}
			ViewData["Fakulte"] = SLOlusturma.FakulteListele();
			ViewData["Bolum"] = SLOlusturma.BolumListele();
			return View("EkleDuzenle", akademisyen);
		}

		public ActionResult Duzenle(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Liste));

			Akademisyen akademisyen = akademisyenIslemleri.Bul("ID = " + id);

			if (akademisyen == null)
				return HttpNotFound();

			ViewData["Fakulte"] = SLOlusturma.FakulteListele();
			ViewData["Bolum"] = SLOlusturma.BolumListele();

			return View("EkleDuzenle", akademisyen);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Duzenle(int id, Akademisyen gelenAkademisyen)
		{
			if(ModelState.IsValid)
			{
				Akademisyen akademisyen = akademisyenIslemleri.Bul("ID = " + id);
				akademisyen.Ad = gelenAkademisyen.Ad;
				akademisyen.BolumID = gelenAkademisyen.BolumID;
				akademisyen.Etkin = gelenAkademisyen.Etkin;
				akademisyen.FakulteID = gelenAkademisyen.FakulteID;
				akademisyen.Soyad = gelenAkademisyen.Soyad;
				akademisyen.TCKNO = gelenAkademisyen.TCKNO;
				akademisyen.Unvan = gelenAkademisyen.Unvan;

				int durum = akademisyenIslemleri.Guncelle("ID = " + id, akademisyen);
			}
			ViewData["Fakulte"] = SLOlusturma.FakulteListele();
			ViewData["Bolum"] = SLOlusturma.BolumListele();
			return View("EkleDuzenle", gelenAkademisyen);
		}

		public ActionResult Sil(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Liste));

			Proje proje = projeIslemleri.Bul("ID = " + id);

			if (proje == null)
				return HttpNotFound();

			return View(proje);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Sil(int id, Akademisyen gelenAkademisyen)
		{
			if (projeIslemleri.Guncelle("ID = " + id, "Silindi", false, typeof(bool)) > 0)
				return RedirectToAction(nameof(Liste));

			ViewBag.Hata = "İşleminiz gerçekleştirilemedi. Lütfen tekrar deneyiniz.";
			return View();
		}

		#region OgrenciDanismanIslemleri

		public ActionResult OgrenciDanismanListesi()
		{
			List<VOgrenciDanisman> ogrenciDanisman = vogrenciDanismanIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.OFakulteID + " And BolumID = " + AnlikOturum.Kullanici.OBolumID);

			return View(ogrenciDanisman);
		}

		public ActionResult DanismanAtama()
		{
			ViewData["Akademisyenler"] = SLOlusturma.AkademisyenListele();
			ViewData["Ogrenciler"] = SLOlusturma.OgrenciListele();

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DanismanAtama(OgrenciDanisman ogrenciDanisman)
		{
			if (ModelState.IsValid)
			{
				ogrenciDanisman.DonemID = AnlikOturum.Donem.ID;
				int durum = ogrenciDanismanIslemleri.Ekle(ogrenciDanisman);

				if (durum > 0)
					return RedirectToAction(nameof(OgrenciDanismanListesi));
			}
			ViewData["Akademisyenler"] = SLOlusturma.AkademisyenListele();
			ViewData["Ogrenciler"] = SLOlusturma.OgrenciListele();
			return View(ogrenciDanisman);
		}

		public ActionResult DanismanDuzenle(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(OgrenciDanismanListesi));

			VOgrenciDanisman ogrenciDanisman = vogrenciDanismanIslemleri.Bul("ID = " + id);

			if (ogrenciDanisman == null)
				return HttpNotFound();

			return View("DanismanAtama", ogrenciDanisman);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DanismanDuzenle(int id, OgrenciDanisman gelenOgrDan)
		{
			if (ModelState.IsValid)
			{
				OgrenciDanisman ogrenciDanisman = ogrenciDanismanIslemleri.Bul("ID = " + id);
				ogrenciDanisman.DanismanID = gelenOgrDan.DanismanID;
				ogrenciDanisman.OgrenciID = gelenOgrDan.OgrenciID;

				int durum = ogrenciDanismanIslemleri.Guncelle("ID = " + id, ogrenciDanisman);

				if (durum > 0)
					return RedirectToAction(nameof(OgrenciDanismanListesi));
			}
			ViewData["Akademisyenler"] = SLOlusturma.AkademisyenListele();
			ViewData["Ogrenciler"] = SLOlusturma.OgrenciListele();
			return View("DanismanAtama", gelenOgrDan);
		} 

		#endregion
	}
}