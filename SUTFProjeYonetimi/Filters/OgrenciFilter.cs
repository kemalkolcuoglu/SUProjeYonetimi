using SUTFProjeYonetimi.Models.EkModel;
using SUTFProjeYonetimi.Models.Enum;
using System.Web.Mvc;

namespace SUTFProjeYonetimi.Filters
{
	public class OgrenciFilter : FilterAttribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			if (AnlikOturum.Kullanici == null || AnlikOturum.Kullanici.Yetki != (int)Yetkilendirme.Ogrenci)
				filterContext.Result = new RedirectResult("/Panel/Anasayfa");
		}
	}
}