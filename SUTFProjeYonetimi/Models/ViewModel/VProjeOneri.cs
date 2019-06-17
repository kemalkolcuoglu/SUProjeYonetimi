namespace SUTFProjeYonetimi.Models.ViewModel
{
	public class VProjeOneri : ProjeOneri
	{
		public virtual int FakulteID { get; set; }
		public virtual int BolumID { get; set; }
		public virtual string OgrenciNo { get; set; }
		public virtual string OgrenciAd { get; set; }
		public virtual string OgrenciSoyad { get; set; }
		public virtual string DanismanAd { get; set; }
		public virtual string DanismanSoyad { get; set; }
	}
}