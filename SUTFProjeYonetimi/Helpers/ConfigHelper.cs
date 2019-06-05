using System;
using System.Configuration;

namespace SUTFProjeYonetimi.Helpers
{
	public static class ConfigHelper
	{
		public static T Get<T>(string key)
		{
			return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
		}
	}
}