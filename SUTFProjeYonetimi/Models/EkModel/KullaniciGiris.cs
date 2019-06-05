using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUTFProjeYonetimi.Models.EkModel
{
	public class KullaniciGiris
	{
		[MaxLength(15), DisplayName("Öğrenci No.")]
		public string OgrenciNo { get; set; }

		[MaxLength(15), DisplayName("Şifre"), DataType(DataType.Password)]
		public string Sifre { get; set; }
	}
}