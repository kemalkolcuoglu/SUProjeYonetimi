namespace SUTFProjeYonetimi.Models
{
	public class ProjeTipi
	{
		public int ID { get; set; }
		public string Ad { get; set; }

		[System.ComponentModel.DisplayName("Fakülte")]
		public int FakulteID { get; set; }
		public bool Etkin { get; set; }
		public bool Silindi { get; set; }
	}
}