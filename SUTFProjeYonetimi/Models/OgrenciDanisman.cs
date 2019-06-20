using System.ComponentModel;

namespace SUTFProjeYonetimi.Models
{
	public class OgrenciDanisman
	{
		public int ID { get; set; }

		[DisplayName("Dönemi")]
		public int DonemID { get; set; }

		public int OgrenciID { get; set; }

		public int DanismanID { get; set; }
	}
}