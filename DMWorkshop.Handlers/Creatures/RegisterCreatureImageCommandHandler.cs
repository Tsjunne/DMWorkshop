using DMWorkshop.DTO.Creatures;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMWorkshop.Handlers.Creatures
{
    public class RegisterCreatureImageCommandHandler : IRequestHandler<RegisterCreatureImageCommand>
    {
        private IMongoDatabase _database;

        public RegisterCreatureImageCommandHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task Handle(RegisterCreatureImageCommand command, CancellationToken cancellationToken)
        {
            var bucket = new GridFSBucket(_database, new GridFSBucketOptions
            {
                BucketName = "creatures"
            });

            await bucket.UploadFromStreamAsync(command.Name, command.Image, null, cancellationToken);
        }
    }
}
