using SUTFProjeYonetimi.Models.EkModel;
using System.Web.Mvc;

namespace SUTFProjeYonetimi.Filters
{
	public class AnlikOturumFilter : FilterAttribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			if (AnlikOturum.Kullanici == null) 
				filterContext.Result = new RedirectResult("/Panel/GirisYap");
		}
	}
}