using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUTFProjeYonetimi.Models
{
	public class Bolum
	{
		[Key]
		public int ID { get; set; }

		[Required, DisplayName("Fakülte")]
		public int FakulteID { get; set; }

		[Required, MaxLength(50), DisplayName("Bölüm Adı")]
		public string Ad { get; set; }

		[Required, MaxLength(15), DisplayName("Bölüm Kısa Kodu")]
		public string KisaKod { get; set; }

		[Required, DisplayName("Bölüm Başkanı")]
		public int Yetkili { get; set; }

		public bool Etkin { get; set; }

		public bool Silindi { get; set; }
	}
}