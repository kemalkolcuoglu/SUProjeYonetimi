using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUTFProjeYonetimi.Models
{
	public class Ogrenci
	{
		[Key]
		public int ID { get; set; }

		[MaxLength(15), DisplayName("Öğrenci No.")]
		public string OgrenciNo { get; set; }

		[DisplayName("Fakülte")]
		public int FakulteID { get; set; }

		[DisplayName("Bölüm")]
		public int BolumID { get; set; }

		[Required, MaxLength(11), DisplayName("T.C. Kimlik No.")]
		public string TCKNO { get; set; }

		[Required, MaxLength(50)]
		public string Ad { get; set; }

		[Required, MaxLength(50)]
		public string Soyad { get; set; }

		[DisplayName("Sınıf")]
		public int Sinif { get; set; }

		[DisplayName("Öğrenim Tipi (NÖ-İÖ)")]
		public int OgrenimTipi { get; set; }

		public bool Etkin { get; set; }

		public bool Silindi { get; set; }

		/* SQL View'dan gelen veriler (vogrencidanisman) */

		public virtual int DanismanID { get; set; }
	}
}