using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionResiliency
{
    class Program
    {
        static void Main(string[] args)
        {
            //Add an interceptor to force a fake db connection issue
            //so we can see the connection retry at work. This intercepts
            //ReaderExecuting to force the exception.
            DbInterception.Add(new InterceptorForceTransientErrors());

            //Add an interceptor to log our details so we can log
            //out the details each step of the way
            DbInterception.Add(new InterceptorForLogging());


            using (var context = new MusicContext())
            {
                Console.WriteLine("Adding album");
                context.Albums.Add(new Album()
                {
                    Title = "My Album",
                    Price = 9.99m
                });

                context.SaveChanges();

                Console.WriteLine("Attempting query (Interceptor will now fake a connection error to trigger retry)");
                
                //To force our exception, one of our parameters needs to have
                //the word Throw. Thats how our connection interceptor knows to fake an error

                try
                {
                    var count = context.Albums.Where(o => o.Title.Contains("FakeAnError")).Count();

                }
                catch (RetryLimitExceededException)
                {
                    Console.WriteLine("Caught RetryLimitExceededException, connection retry worked.");

                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    return;
                }
                //Shouldn't get here :)
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
    public class ConnectionConfiguration : DbConfiguration
    {
        public ConnectionConfiguration()
        {
           SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy(3, new TimeSpan(0, 0, 5)));
        }
    }

    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }

    public class MusicContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
    }
}
