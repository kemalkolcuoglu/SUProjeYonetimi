using System.ComponentModel;

namespace SUTFProjeYonetimi.Models
{
	public class OgrenciDanisman
	{
		public int ID { get; set; }

		[DisplayName("Dönemi")]
		public int DonemID { get; set; }

		[DisplayName("Öğrenci")]
		public int OgrenciID { get; set; }

		[DisplayName("Danışman")]
		public int DanismanID { get; set; }
	}
}