using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Models
{
	public class Album
	{
		public int AlbumID { get; set; }

		[Required()]
		[StringLength(100, MinimumLength = 2)]
		public String Title { get; set; }

		public int ArtistID { get; set; }
		public virtual Artist Artist { get; set; }
		public virtual List<Reviewer> Reviewers { get; set; }
	}
}
