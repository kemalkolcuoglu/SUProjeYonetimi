using SUTFProjeYonetimi.Models.ViewModel;

namespace SUTFProjeYonetimi.Models.EkModel
{
	public class AnlikOturum
	{
		public static Kullanici Kullanici
		{
			get { return Get<Kullanici>("Kullanici"); }
		}

		public static Donem Donem
		{
			get { return Get<Donem>("Donem"); }
		}

		public static void Set<T>(string key, T obj)
		{
			System.Web.HttpContext.Current.Session[key] = obj;
		}

		public static T Get<T>(string key)
		{
			if (System.Web.HttpContext.Current.Session[key] != null)
			{
				return (T)System.Web.HttpContext.Current.Session[key];
			}

			return default(T);
		}

		public static void Remove(string key)
		{
			if (System.Web.HttpContext.Current.Session[key] != null)
			{
				System.Web.HttpContext.Current.Session.Remove(key);
			}
		}

		public static void Clear()
		{
			System.Web.HttpContext.Current.Session.Clear();
		}
	}
}