using DMWorkshop.DTO.Creatures;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMWorkshop.Handlers.Creatures
{
    public class GetCreatureImageQueryHandler : IRequestHandler<GetCreatureImageQuery, Stream>
    {
        private IMongoDatabase _database;

        public GetCreatureImageQueryHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Stream> Handle(GetCreatureImageQuery query, CancellationToken cancellationToken)
        {
            var bucket = new GridFSBucket(_database, new GridFSBucketOptions
            {
                BucketName = "creatures"
            });
            
            try
            {
                return await bucket.OpenDownloadStreamByNameAsync(query.Name, null, cancellationToken);
            }
            catch (GridFSFileNotFoundException)
            {
                var assembly = Assembly.GetExecutingAssembly();
                string[] names = assembly.GetManifestResourceNames();
                Stream resource = assembly.GetManifestResourceStream("DMWorkshop.Handlers.defaultcreature.jpg");
                return resource;
            }
        }
    }
}
