using SUTFProjeYonetimi.Filters;
using SUTFProjeYonetimi.Models;
using SUTFProjeYonetimi.Models.EkModel;
using SUTFProjeYonetimi.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
			List<Proje> projeler;
			switch (AnlikOturum.Kullanici.Yetki)
			{
				case (int)Yetkilendirme.SystemAdmin: projeler = projeIslemleri.VeriGetir(); break;
				case (int)Yetkilendirme.Dekan:
					projeler = projeIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.AFakulteID + " And Silindi = 0"); break;
				case (int)Yetkilendirme.BolumBaskani:
					projeler = projeIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.AFakulteID + " And BolumID = " + AnlikOturum.Kullanici.ABolumID + " And Silindi = 0"); break;
				case (int)Yetkilendirme.Danisman:
					projeler = projeIslemleri.VeriGetir("FakulteID = " + AnlikOturum.Kullanici.AFakulteID + " And BolumID = " + AnlikOturum.Kullanici.ABolumID + " And  DanismanID = " + AnlikOturum.Kullanici.NitelikID + " And Silindi = 0"); break;
				case (int)Yetkilendirme.Ogrenci:
					return RedirectToAction("Anasayfa", "Panel");
				default:
					projeler = new List<Proje>(); break;
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
					proje = projeIslemleri.Bul("ID = " + id + " And FakulteID = " + AnlikOturum.Kullanici.AFakulteID + " And Silindi = 0"); break;
				case (int)Yetkilendirme.BolumBaskani:
					proje = projeIslemleri.Bul("ID = " + id + " And FakulteID = " + AnlikOturum.Kullanici.AFakulteID + " And BolumID = " + AnlikOturum.Kullanici.ABolumID + " And Silindi = 0"); break;
				case (int)Yetkilendirme.Danisman:
					proje = projeIslemleri.Bul("ID = " + id + " And FakulteID = " + AnlikOturum.Kullanici.AFakulteID + " And BolumID = " + AnlikOturum.Kullanici.ABolumID + " And  DanismanID = " + AnlikOturum.Kullanici.NitelikID + " And Silindi = 0"); break;
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
			return View("EkleDuzenle", proje);
		}

		public ActionResult Duzenle(int? id)
		{
			return View("EkleDuzenle");
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
			List<ProjeOneri> projeOneri;
			switch (AnlikOturum.Kullanici.Yetki)
			{
				case (int)Yetkilendirme.BolumBaskani:
					projeOneri = projeOneriIslemleri.VeriGetirSQL(
						"Select * From vprojeoneri Where FakulteID = " + AnlikOturum.Kullanici.AFakulteID + " And BolumID = " + AnlikOturum.Kullanici.ABolumID + " And Durum = " + (int)ProjeOneriDurumu.DanismanOnayi
					);
					break;
				case (int)Yetkilendirme.Danisman:
					projeOneri = projeOneriIslemleri.VeriGetirSQL(
						"Select * From vprojeoneri Where FakulteID = " + AnlikOturum.Kullanici.AFakulteID + " And BolumID = " + AnlikOturum.Kullanici.ABolumID + " And DanismanID = " + AnlikOturum.Kullanici.NitelikID + " Durum = " + (int)ProjeOneriDurumu.Beklemede
					);
					break;
				default: projeOneri = new List<ProjeOneri>(); break;
			}
			return View(projeOneri);
		}

		public ActionResult OneriOnayi(int? id)
		{
			if (id == null)
				return RedirectToAction(nameof(OneriListesi));

			ProjeOneri projeOneri = projeOneriIslemleri.Bul("ID = " + id);

			if (projeOneri == null)
				return HttpNotFound();

			return View(projeOneri);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult OneriOnayi(int id)
		{
			if (AnlikOturum.Kullanici.Yetki == (int)Yetkilendirme.BolumBaskani)
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
						ProjeOneriID = id,
						Etkin = true,
						ProjeAciklamasi = projeOneri.ProjeKonusuAmaci
					};
					projeIslemleri.Ekle(proje);
				}
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
			}
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult OneriRed(int id)
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

		public ActionResult Duyuru()
		{
			// TODO : Duyuru Yapılacak
			return View();
		}
	}
}