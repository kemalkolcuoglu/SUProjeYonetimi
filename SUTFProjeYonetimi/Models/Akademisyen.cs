using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUTFProjeYonetimi.Models
{
	public class Akademisyen
	{
		[Key]
		public int ID{ get; set; }

		public int Yetki { get; set; }

		[DisplayName("Fakülte")]
		public int FakulteID { get; set; }

		[DisplayName("Bölüm")]
		public int BolumID { get; set; }

		[Required, MaxLength(11), DisplayName("T.C. Kimlik No.")]
		public string TCKNO { get; set; }

		[Required, MaxLength(50), DisplayName("Ünvan")]
		public string Unvan { get; set; }

		[Required, MaxLength(50)]
		public string Ad { get; set; }

		[Required, MaxLength(50)]
		public string Soyad { get; set; }

		[DisplayName("Şifre")]
		public string Sifre { get; set; }

		public bool Etkin { get; set; }
	}
}