using DMWorkshop.DTO.Characters;
using DMWorkshop.Handlers.Characters;
using DMWorkshop.Handlers.Mapping;
using MongoDB.Driver;
using System;
using System.Threading;

namespace DMWorkshop.DataMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoMapping.Configure();
            
            var azure = GetDb("***REMOVED***");
            var mongolab = GetDb("***REMOVED***");

            var handler = new CopyContentCommandHandler(azure, mongolab);
            handler.Handle(new CopyContentCommand(), CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();            
        }

        static IMongoDatabase GetDb(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("dmworkshop");

            return database;
        }
    }
}
