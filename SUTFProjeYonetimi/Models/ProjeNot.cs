using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SUTFProjeYonetimi.Models
{
	public class ProjeNot
	{
		[Key]
		public int ID { get; set; }

		[DisplayName("Dönemi")]
		public int DonemID { get; set; }

		[DisplayName("Öğrencinin Danışmanı")]
		public int ProjeOgrDanID { get; set; }

		[DisplayName("Vize Notu")]
		public int VizeNotu { get; set; }

		[DisplayName("Final Notu")]
		public int FinalNotu { get; set; }

		[DisplayName("Geçme Durumu")]
		public bool Durum { get; set; }

		[MaxLength(255), DisplayName("Açıklama")]
		public string Aciklama { get; set; }
	}
}