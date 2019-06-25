using SUTFProjeYonetimi.Filters;
using SUTFProjeYonetimi.Models;
using SUTFProjeYonetimi.Models.EkModel;
using SUTFProjeYonetimi.Models.Enum;
using SUTFProjeYonetimi.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using static SUTFProjeYonetimi.App_Start.Tanimlamalar;

namespace SUTFProjeYonetimi.Controllers
{
	[AnlikOturumFilter]
	[DanismanFilter]
	public class ProjeController : Controller
	{
		#region ProjeIslemleri

		public ActionResult Liste()
		{
			List<VProje> projeler;
			switch (AnlikOturum.Kullanici.Yetki)
			{
				case (int)Yetkilendirme.SystemAdmin: projeler = vprojeIslemleri.VeriGetir(); break;
				case (int)Yetkilendirme.Dekan:
					projeler = vprojeIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID); break;
				case (int)Yetkilendirme.BolumBaskani:
					projeler = vprojeIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " And BolumID = " + AnlikOturum.Kullanici.Akademisyen.BolumID); break;
				case (int)Yetkilendirme.Danisman:
					projeler = vprojeIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " And BolumID = " + AnlikOturum.Kullanici.Akademisyen.BolumID + " And  DanismanID = " + AnlikOturum.Kullanici.Akademisyen.ID); break;
				case (int)Yetkilendirme.Ogrenci:
					return RedirectToAction("Panel", "Anasayfa");
				default:
					projeler = new List<VProje>(); break;
			}
			return View(projeler);
		}

		public ActionResult Detay(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Liste));

			Proje proje;
			switch (AnlikOturum.Kullanici.Yetki)
			{
				case (int)Yetkilendirme.SystemAdmin: proje = projeIslemleri.Bul("ID = " + id); break;
				case (int)Yetkilendirme.Dekan:
					proje = projeIslemleri.Bul("ID = " + id + " And FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " And Silindi = 0"); break;
				case (int)Yetkilendirme.BolumBaskani:
					proje = projeIslemleri.Bul("ID = " + id + " And FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " And BolumID = " + AnlikOturum.Kullanici.Akademisyen.BolumID + " And Silindi = 0"); break;
				case (int)Yetkilendirme.Danisman:
					proje = projeIslemleri.Bul("ID = " + id + " And FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " And BolumID = " + AnlikOturum.Kullanici.Akademisyen.BolumID + " And  DanismanID = " + AnlikOturum.Kullanici.Akademisyen.ID + " And Silindi = 0"); break;
				case (int)Yetkilendirme.Ogrenci:
					return RedirectToAction("Anasayfa", "Panel");
				default:
					proje = null; break;
			}
			if (proje == null)
				return HttpNotFound();

			return View(proje);
		}

		public ActionResult Ekle()
		{
			ViewData["ProjeTipi"] = SLOlusturma.ProjeTipiListele();

			return View("EkleDuzenle");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Ekle(Proje proje)
		{
			if (ModelState.IsValid)
			{
				int durum = projeIslemleri.Ekle(proje);

				if (durum > 0)
					return RedirectToAction(nameof(Liste));
			}
			ViewData["ProjeTipi"] = SLOlusturma.ProjeTipiListele();
			return View("EkleDuzenle", proje);
		}

		public ActionResult Duzenle(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Liste));

			Proje proje = projeIslemleri.Bul("ID = " + id);

			if (proje == null)
				return HttpNotFound();

			ViewData["ProjeTipi"] = SLOlusturma.ProjeTipiListele();

			return View("EkleDuzenle", proje);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Duzenle(int id, Proje gelenProje)
		{
			if (ModelState.IsValid)
			{
				Proje proje = projeIslemleri.Bul("ID = " + id);
				proje.BaslangicTarihi = gelenProje.BaslangicTarihi;
				proje.BitisTarihi = gelenProje.BitisTarihi;
				proje.EkAlan1 = gelenProje.EkAlan1;
				proje.EkAlan2 = gelenProje.EkAlan2;
				proje.EkDosya = gelenProje.EkDosya;
				proje.ProjeAciklamasi = gelenProje.ProjeAciklamasi;
				proje.ProjeAdi = gelenProje.ProjeAdi;
				proje.ProjeTipi = gelenProje.ProjeTipi;
				proje.Rapor = gelenProje.Rapor;

				int durum = projeIslemleri.Guncelle("ID = " + id, proje);

				if (durum > 0)
					return RedirectToAction(nameof(Liste));
			}
			ViewData["ProjeTipi"] = SLOlusturma.ProjeTipiListele();
			return View("EkleDuzenle", gelenProje);
		}

		public ActionResult Sil(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(Liste));

			Proje proje = projeIslemleri.Bul("ID = " + id + " And Silindi = 0");

			if (proje == null)
				return HttpNotFound();

			return View(proje);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Sil(int id, Proje gelenProje)
		{
			int durum = projeIslemleri.Guncelle("ID = " + id, "Silindi", true, typeof(bool));
			if (durum > 0)
				return RedirectToAction(nameof(Liste));
			return View();
		}

		#endregion

		#region ProjeOneriIslemleri

		public ActionResult OneriListesi()
		{
			List<VProjeOneri> projeOneri;
			switch (AnlikOturum.Kullanici.Yetki)
			{
				case (int)Yetkilendirme.SystemAdmin:
					projeOneri = vprojeOneriIslemleri.VeriGetir("Durum <= " + (int)ProjeOneriDurumu.Onaylandi); break;
				case (int)Yetkilendirme.BolumBaskani:
					projeOneri = vprojeOneriIslemleri.VeriGetir(
						"FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " And BolumID = " + AnlikOturum.Kullanici.Akademisyen.BolumID + " And Durum = " + (int)ProjeOneriDurumu.DanismanOnayi
					);
					break;
				case (int)Yetkilendirme.Danisman:
					projeOneri = vprojeOneriIslemleri.VeriGetir(
						"FakulteID = " + AnlikOturum.Kullanici.Akademisyen.FakulteID + " And BolumID = " + AnlikOturum.Kullanici.Akademisyen.BolumID + " And DanismanID = " + AnlikOturum.Kullanici.Akademisyen.ID + " And Durum = " + (int)ProjeOneriDurumu.Beklemede
					);
					break;
				default: projeOneri = new List<VProjeOneri>(); break;
			}
			return View(projeOneri);
		}

		public ActionResult OneriOnayi(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(OneriListesi));

			VProjeOneri projeOneri = vprojeOneriIslemleri.Bul("ID = " + id);

			if (projeOneri == null)
				return HttpNotFound();

			return View(projeOneri);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult OneriOnayi(int id, bool onay)
		{
			if (AnlikOturum.Kullanici.Yetki <= (int)Yetkilendirme.BolumBaskani && onay)
			{
				ProjeOneri projeOneri = projeOneriIslemleri.Bul("ID = " + id);
				projeOneri.Durum = (int)ProjeOneriDurumu.Onaylandi;
				projeOneri.BolumBaskaniOnay = true;
				projeOneri.BolumBaskaniOnayTarihi = DateTime.Now;

				int durum = projeOneriIslemleri.Guncelle("ID = " + id, projeOneri);

				if (durum > 0)
				{
					Proje proje = new Proje()
					{
						BaslangicTarihi = DateTime.Now,
						ProjeAdi = projeOneri.ProjeAdi,
						Etkin = true,
						ProjeAciklamasi = projeOneri.ProjeKonusuAmaci
					};
					projeIslemleri.Ekle(proje);

					ProjeOgrenciDanisman projeOgrenciDanisman = new ProjeOgrenciDanisman()
					{
						DonemID = AnlikOturum.Donem.ID,
						OgrenciID = projeOneri.OgrenciID,
						ProjeID = proje.ID,
						DanismanID = projeOneri.DanismanID
					};
					projeOgrDanIslemleri.Ekle(projeOgrenciDanisman);

					return RedirectToAction(nameof(OneriListesi));
				}
				return View(projeOneri);
			}
			else if (AnlikOturum.Kullanici.Yetki == (int)Yetkilendirme.Danisman)
			{
				ProjeOneri projeOneri = projeOneriIslemleri.Bul("ID = " + id);
				projeOneri.DanismanOnay = true;
				projeOneri.DanismanOnayTarihi = DateTime.Now;
				projeOneri.Durum = (int)ProjeOneriDurumu.DanismanOnayi;

				int durum = projeOneriIslemleri.Guncelle("ID = " + id, projeOneri);

				if (durum > 0)
					return RedirectToAction(nameof(OneriListesi));

				return View(projeOneri);
			}
			return RedirectToAction(nameof(OneriListesi));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult OneriRed(int id, bool onay)
		{
			ProjeOneri projeOneri = projeOneriIslemleri.Bul("ID = " + id);

			projeOneri.Durum = (int)ProjeOneriDurumu.Reddedildi;
			int durum = projeOneriIslemleri.Guncelle("ID = " + id, "Durum", projeOneri.Durum, typeof(int));

			if (durum > 0)
				return RedirectToAction(nameof(OneriListesi));

			return RedirectToAction(nameof(OneriOnayi), new { id });
		}

		#endregion

		#region ProjeIlerlemeTakipIslemleri

		public ActionResult IlerlemeListe()
		{
			// TODO : İlerleme Listesi Yapılacak
			return View();
		}

		public ActionResult IlerlemeDetay(int? id)
		{
			// TODO : İlerleme Detay Yapılacak
			return View();
		}

		public ActionResult IlerlemeTakip(int? id)
		{
			// TODO : İlerleme Takip Yapılacak
			return View();
		}

		#endregion

		#region ProjeDegerlendirmeIslemleri

		public ActionResult DegerlendirmeListe()
		{
			// TODO : Değerlendirme Listesi Yapılacak

			return View();
		}

		public ActionResult DegerlendirmeDetay(int? id)
		{
			// TODO : Değerlendirme Detay Yapılacak
			return View();
		}

		#endregion
	}
}