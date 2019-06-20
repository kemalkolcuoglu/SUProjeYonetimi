namespace SUTFProjeYonetimi.Models
{
	public class ProjeOgrenciDanisman
	{
		[System.ComponentModel.DisplayName("Dönemi")]
		public int DonemID { get; set; }

		public int ID { get; set; }
		public int ProjeID { get; set; }
		public int DanismanID { get; set; }
		public int OgrenciID { get; set; }
	}
}