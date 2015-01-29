using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Models
{
	public class ArtistDetails
	{
		[Key()]
		[ForeignKey("Artist")]
		public int ArtistID { get; set; }
		public string Bio { get; set; }
		public virtual Artist Artist { get; set; }
	}
}
