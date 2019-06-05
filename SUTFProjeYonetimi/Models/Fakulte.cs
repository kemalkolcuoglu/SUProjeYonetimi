using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUTFProjeYonetimi.Models
{
	public class Fakulte
	{
		[Key]
		public int ID { get; set; }

		[Required, MaxLength(50), DisplayName("Fakülte Adı")]
		public string Ad { get; set; }

		[Required, MaxLength(15), DisplayName("Bölüm Kısa Kodu")]
		public string KisaKod { get; set; }
	}
}