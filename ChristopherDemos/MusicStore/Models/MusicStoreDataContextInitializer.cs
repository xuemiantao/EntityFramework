using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Models
{
	public class MusicStoreDataContextInitializer : DropCreateDatabaseAlways<MusicStoreDataContext>
	{
		protected override void Seed(MusicStoreDataContext context)
		{
			Artist artist = new Artist() { Name = "First Artist"};
			context.Artists.Add(artist);

			context.Albums.Add(new Album() { Artist = artist, Title = "First Album" });
			context.Albums.Add(new Album() { Artist = artist, Title = "Second Album" });

			context.Albums.Add(new Album() {
				Artist = context.Artists.Add(new Artist() {Name = "Second Artist"}),
				Title = "Third Album"
			});

			context.Artists.Add(new SoloArtist() { Name = "Solo Artist", Instrument = "Piano" });

			context.SaveChanges();
		}
	}
}
