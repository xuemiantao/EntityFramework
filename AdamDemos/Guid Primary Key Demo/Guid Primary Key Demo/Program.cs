using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guid_Primary_Key_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MusicContext())
            {
                context.Albums.Add(new Album()
                {
                    Title = "My Album",
                    Price = 9.99m
                });

                context.SaveChanges();

                foreach (var album in context.Albums.ToList())
                {
                    Console.WriteLine(
                        string.Format("Id:{0}\nTitle:{1}\nPrice:{2}\n\n", 
                        album.AlbumId, album.Title, album.Price));
                }
            }

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

        }
    }

    public class Album
    {
        /// <summary>
        ///Jusa a Comment!!!
        /// You'll notice when we do an update-database -script the sql that is generated contains
        /// the code to generate a new Guid upon insert
        /// CREATE TABLE [dbo].[Albums] (
        ///    [AlbumId] [uniqueidentifier] NOT NULL DEFAULT newsequentialid(),
        ///    [Title] [nvarchar](max),
        ///    [Price] [decimal](18, 2) NOT NULL,
        ///    CONSTRAINT [PK_dbo.Albums] PRIMARY KEY ([AlbumId])
        ///)
        /// </summary>
        public Guid AlbumId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }

    public class MusicContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().Property(o => o.AlbumId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Album> Albums { get; set; }
    }

}
