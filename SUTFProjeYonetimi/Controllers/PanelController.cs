using SUTFProjeYonetimi.Filters;
using SUTFProjeYonetimi.Models;
using SUTFProjeYonetimi.Models.EkModel;
using System.Web.Mvc;
using static SUTFProjeYonetimi.App_Start.Tanimlamalar;

namespace SUTFProjeYonetimi.Controllers
{
	public class PanelController : Controller
    {
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
    }
}