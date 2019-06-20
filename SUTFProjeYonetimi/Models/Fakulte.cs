using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUTFProjeYonetimi.Models
{
	public class Fakulte
	{
		[Key]
		public int ID { get; set; }

		[Required, MaxLength(50), DisplayName("Fakülte Adı")]
		public string Ad { get; set; }

		[Required, MaxLength(15), DisplayName("Fakülte Kısa Kodu")]
		public string KisaKod { get; set; }

		[Required, DisplayName("Dekan")]
		public int Yetkili { get; set; }

		public bool Etkin{ get; set; }
		public bool Silindi { get; set; }
	}
}