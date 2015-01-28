using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Models.Repositories
{
	public class ArtistRepository : Repository<Artist>
	{
		public List<Artist> GetByName(String name)
		{
			return DbSet.Where(a => a.Name.Contains(name)).ToList();
		}

		public override void Update(Artist entity)
		{
			base.Update(entity);
			SaveChanges();
			entity.Version++;
			base.Update(entity);
			SaveChanges();
		}

		public List<SoloArtist> GetSoloArtists()
		{
			return DbSet.OfType<SoloArtist>().ToList();
		}
	}
}
