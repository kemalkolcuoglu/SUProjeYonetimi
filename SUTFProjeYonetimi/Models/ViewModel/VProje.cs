namespace SUTFProjeYonetimi.Models.ViewModel
{
	public class VProje : Proje
	{
		public virtual int OgrenciID { get; set; }
		public virtual string OgrenciNo { get; set; }
		public virtual string OgrenciAd { get; set; }
		public virtual string OgrenciSoyad { get; set; }
		public virtual int FakulteID { get; set; }
		public virtual int BolumID { get; set; }
		public virtual int DanismanID { get; set; }
	}
}