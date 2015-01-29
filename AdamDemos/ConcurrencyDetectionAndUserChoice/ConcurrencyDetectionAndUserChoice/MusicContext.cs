using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConcurrencyDetectionAndUserChoice
{
    public class MusicContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
    }
}