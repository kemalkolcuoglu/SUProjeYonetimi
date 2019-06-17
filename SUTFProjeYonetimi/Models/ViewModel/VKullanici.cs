namespace SUTFProjeYonetimi.Models.ViewModel
{
	public class VKullanici : Kullanici
	{
		public virtual int AFakulteID { get; set; }
		public virtual int ABolumID { get; set; }
		public virtual int OFakulteID { get; set; }
		public virtual int OBolumID { get; set; }
	}
}