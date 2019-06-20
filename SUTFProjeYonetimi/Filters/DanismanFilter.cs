using SUTFProjeYonetimi.Models.EkModel;
using SUTFProjeYonetimi.Models.Enum;
using System.Web.Mvc;

namespace SUTFProjeYonetimi.Filters
{
	public class DanismanFilter : FilterAttribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			if (AnlikOturum.Kullanici == null || AnlikOturum.Kullanici.Yetki > (int)Yetkilendirme.Danisman)
				filterContext.Result = new RedirectResult("/Panel/Anasayfa");
		}
	}
}