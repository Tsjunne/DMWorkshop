using DMWorkshop.DTO.Creatures;
using DMWorkshop.Handlers.Creatures;
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
            
            var azure = GetDb("mongodb://dmworkshop:wHKTiqFngOxoM1m62wvY1Myetka2gTFGWaubmX3AAsTFdppQn5eagLv5gMKvVnmKqzPH3wnm6gAhVDjNfD0Ueg==@dmworkshop.documents.azure.com:10255/?ssl=true&replicaSet=globaldb");
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
