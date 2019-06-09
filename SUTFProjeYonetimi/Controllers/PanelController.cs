using SUTFProjeYonetimi.Filters;
using SUTFProjeYonetimi.Models;
using SUTFProjeYonetimi.Models.EkModel;
using System.Web.Mvc;
using static SUTFProjeYonetimi.App_Start.Tanimlamalar;
using System.Collections.Generic;
using System;
using SUTFProjeYonetimi.Models.Enum;

namespace SUTFProjeYonetimi.Controllers
{
	public class PanelController : Controller
	{
		private static Random random = new Random();

		[AnlikOturumFilter]
		public ActionResult Anasayfa()
		{
			return View();
		}

		[AnlikOturumFilter]
		public ActionResult Profil()
		{
			Kullanici kullanici = kullaniciIslemleri.Bul("ID = " + AnlikOturum.Kullanici.ID);

			if (kullanici == null)
				return HttpNotFound();

			return View(kullanici);
		}

		[AnlikOturumFilter]
		public ActionResult ProfilDuzenle()
		{
			Kullanici kullanici = kullaniciIslemleri.Bul("ID = " + AnlikOturum.Kullanici.ID);

			if (kullanici == null)
				return HttpNotFound();

			return View(kullanici);
		}

		[HttpPost]
		[AnlikOturumFilter]
		[ValidateAntiForgeryToken]
		public ActionResult ProfilDuzenle(string sifre)
		{
			if (ModelState.IsValid)
			{
				int durum = kullaniciIslemleri.Guncelle("ID = " + AnlikOturum.Kullanici.ID, "Sifre", sifre, typeof(string));

				if (durum > 0)
					return RedirectToAction(nameof(Profil));
			}
			return View();
		}

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
				kullanici = kullaniciIslemleri.Bul("KullaniciAdi = '" + kullaniciGiris.OgrenciNo + "' And Sifre = " + kullaniciGiris.Sifre);

				if (kullanici == null)
				{
					ViewBag.Hata = "Girdiğiniz değerlerle eşleşen bir kullanıcı bulunamadı. Lütfen tekrar deneyiniz...";
					return View(kullaniciGiris);
				}

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

		public ActionResult OgrenciFake()
		{
			List<Akademisyen> akademisyenler = akademisyenIslemleri.VeriGetir();

			for (int i = 0; i < 100; i++)
			{
				int akd = random.Next(0, akademisyenler.Count);

				Ogrenci ogrenci = new Ogrenci()
				{
					Ad = FakeData.NameData.GetFirstName(),
					Soyad = FakeData.NameData.GetSurname(),
					BolumID = 1,
					FakulteID = 1,
					Etkin = true,
					OgrenciNo = "153311" + random.Next(100, 999),
					OgrenimTipi = (int)OgrenimTipi.NormalOgretim,
					Silindi = false,
					Sinif = random.Next(1, 4),
					TCKNO = "11111111" + random.Next(100, 999),
					ID = i + 1,
					DanismanID = akademisyenler[akd].ID
				};

				ogrenciIslemleri.Ekle(ogrenci);

				Kullanici kullanici = new Kullanici()
				{
					Etkin = true,
					KullaniciAdi = ogrenci.OgrenciNo,
					NitelikID = ogrenci.ID,
					Sifre = "123",
					Yetki = (int)Yetkilendirme.Ogrenci
				};

				kullaniciIslemleri.Ekle(kullanici);
			}

			return RedirectToAction(nameof(Anasayfa));
		}

		public ActionResult AkademisyenFake()
		{
			for (int i = 0; i < 10; i++)
			{
				Akademisyen akademisyen = new Akademisyen()
				{
					Ad = FakeData.NameData.GetFirstName(),
					Soyad = FakeData.NameData.GetSurname(),
					BolumID = 1,
					FakulteID = 1,
					Etkin = true,
					TCKNO = "21111111" + random.Next(100, 999),
					Unvan = "Dr.",
					ID = i + 1,
				};

				akademisyenIslemleri.Ekle(akademisyen);

				Kullanici kullanici = new Kullanici()
				{
					Etkin = true,
					KullaniciAdi = akademisyen.TCKNO,
					NitelikID = akademisyen.ID,
					Sifre = "123",
					Yetki = (int)Yetkilendirme.Danisman
				};

				kullaniciIslemleri.Ekle(kullanici);
			}

			return RedirectToAction(nameof(Anasayfa));
		}

		public ActionResult ProjeFake()
		{
			List<Ogrenci> ogrenciler = ogrenciIslemleri.VeriGetir();

			for (int i = 0; i < 50; i++)
			{
				int ogr = random.Next(0, ogrenciler.Count);

				ProjeOneri projeOneri = new ProjeOneri()
				{
					ID = i + 1,
					BolumBaskaniOnay = true,
					BolumBaskaniOnayTarihi = DateTime.Now.AddDays(-150),
					DanismanOnay = true,
					DanismanOnayTarihi = DateTime.Now.AddDays(-152),
					Durum = (int)ProjeOneriDurumu.Onaylandi,
					ProjeAdi = $"{ogrenciler[ogr].Ad} {ogrenciler[ogr].Soyad} Projesi",
					ProjeKonusuAmaci = "Consectetur adipiscing elit. Integer porta ante eu ultricies tincidunt. Pellentesque eget mi non nunc iaculis luctus consequat in odio. Mauris fermentum ipsum justo, sagittis rhoncus felis dignissim eu. Etiam dui quam, dapibus a urna vitae, mattis placerat nisi. In eget aliquam metus. Proin feugiat blandit dolor sollicitudin tempor. Nam id auctor odio, ut imperdiet augue. In faucibus ipsum nec tincidunt egestas.",
					CevreselEtkileri = "In congue eget enim a pulvinar. Pellentesque dictum ac velit ut ultricies. Etiam quis nibh at tellus interdum vestibulum et rhoncus ipsum. Integer rhoncus vestibulum arcu, quis imperdiet urna lacinia sit amet. Ut laoreet lacus orci, id condimentum est luctus sit amet. Sed cursus semper libero, id malesuada turpis sodales at. Integer ac augue urna. Donec a pharetra velit. Duis at sodales enim, vulputate ultricies sapien. Aenean volutpat erat quis tortor scelerisque, sit amet ultrices turpis sodales. Nullam in congue ex.\nFusce vitae luctus risus, vehicula vehicula odio.Sed varius tempus orci non vehicula.Mauris nec tempus neque.Nulla tincidunt dui quis odio suscipit bibendum.Nunc elit dolor, vehicula non mattis sit amet,	facilisis at justo.Donec rhoncus faucibus est, et sagittis neque convallis sit amet.Nulla vel aliquet enim.Proin maximus pretium risus.In fringilla malesuada orci eu vehicula.Praesent consequat porttitor turpis ac rutrum.",
					EtikSakincalari = "Nulla aliquam, tortor at dapibus tincidunt, augue felis porta erat, nec posuere felis mauris at neque. Curabitur dictum a mauris quis iaculis. Nulla tristique bibendum.",
					MaliyetArastirmasi = "Nunc at orci risus. Integer in aliquam arcu. Aliquam vel vulputate nunc. Sed ut tellus sed felis viverra sagittis. Praesent hendrerit a felis at rutrum. Ut blandit vel ipsum porta egestas. Cras blandit vitae ante vel semper. Vestibulum eu dignissim neque. Mauris sed massa ipsum. Nullam augue ante, tristique quis tincidunt eu, sollicitudin et nibh. Mauris et laoreet orci. Cras sed justo justo. Fusce a.",
					YararlanilanKaynaklar = "Etiam varius varius mauris, sed dapibus turpis gravida id. Cras nec iaculis magna. Donec efficitur dui eros, eu facilisis lorem faucibus a. Vestibulum fermentum quam felis, ut gravida tortor maximus facilisis. Fusce in arcu at metus suscipit consequat sed vel ex. Nullam fringilla diam vel scelerisque mollis. Nunc quis facilisis sem, laoreet porta arcu. Sed dignissim in massa non varius.\nOrci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nullam consectetur justo ut sodales luctus. Ut fermentum arcu fermentum tellus imperdiet, nec euismod purus cursus. Vestibulum pulvinar rutrum nisl. Phasellus convallis ligula ac tellus mollis aliquet. Sed odio risus, auctor a dolor id, accumsan pharetra nulla. Integer at blandit arcu. Ut odio ligula, accumsan sit amet euismod blandit, mattis nec nunc. Fusce magna velit, ullamcorper nec feugiat sit amet, sodales at ex. Etiam vel pellentesque mi.\nAliquam vel tortor congue, pharetra enim ac, ultrices justo. Praesent sed imperdiet libero. Donec pharetra dolor et nisi sodales pretium. Nullam vestibulum enim posuere, pulvinar dolor sed, vestibulum nisl. Vivamus suscipit lorem ex, vehicula ornare sapien fermentum interdum. Proin lectus nibh, consectetur id ex nec, dictum tempor nisl. Pellentesque non vulputate velit.",
					Tarih = DateTime.Now.AddDays(-155),
					OgrenciID = ogrenciler[ogr].ID,
					DanismanID = ogrenciler[ogr].DanismanID
				};

				projeOneriIslemleri.Ekle(projeOneri);

				Proje proje = new Proje()
				{
					ID = i + 1,
					BaslangicTarihi = DateTime.Now.AddDays(-150),
					BitisTarihi = DateTime.Now.AddDays(15),
					BolumID = 1,
					FakulteID = 1,
					Etkin = true,
					ProjeOneriID = projeOneri.ID,
					ProjeAdi = projeOneri.ProjeAdi,
					ProjeAciklamasi = "Sed vitae nulla at ante aliquet vehicula. Morbi non nibh.",
					Silindi = false,
					ProjeTipi = 4,
					ProjeNo = "TF-" + random.Next(1000, 9999)
				};

				projeIslemleri.Ekle(proje);

				ProjeOgrenciDanisman pod = new ProjeOgrenciDanisman()
				{
					DanismanID = projeOneri.DanismanID,
					OgrenciID = ogrenciler[ogr].ID,
					ProjeID = proje.ID,
					ID = i + 1
				};

				projeOgrDanIslemleri.Ekle(pod);

				ogrenciler.Remove(ogrenciler[ogr]);
			}

			return RedirectToAction(nameof(Anasayfa));
		}
	}
}