using DMWorkshop.DTO.Characters;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMWorkshop.Handlers.Characters
{
    public class RegisterPortraitCommandHandler : IRequestHandler<RegisterPortraitCommand>
    {
        private IMongoDatabase _database;

        public RegisterPortraitCommandHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task Handle(RegisterPortraitCommand command, CancellationToken cancellationToken)
        {
            var bucket = new GridFSBucket(_database, new GridFSBucketOptions
            {
                BucketName = "portraits"
            });

            await bucket.UploadFromStreamAsync(command.Name, command.Image, null, cancellationToken);
        }
    }
}
