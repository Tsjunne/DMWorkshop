using DMWorkshop.DTO.Campaign;
using DMWorkshop.Model.Campaign;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMWorkshop.Handlers.Campaign
{
    public class DeletePlayerCommandHandler : AsyncRequestHandler<DeletePlayerCommand>
    {
        private readonly IMongoDatabase _database;

        public DeletePlayerCommandHandler(IMongoDatabase database)
        {
            _database = database;
        }

        protected override async Task Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<Party>("players");
            await collection.DeleteOneAsync(x => x.Name == request.Name, cancellationToken);

            var bucket = new GridFSBucket(_database, new GridFSBucketOptions
            {
                BucketName = "portraits"
            });

            var filter = Builders<GridFSFileInfo>.Filter.And(
                Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, request.Name));

            var files = await bucket.FindAsync(filter, null, cancellationToken);
            var file = await files.FirstOrDefaultAsync(cancellationToken);

            if (file != null)
            {
                await bucket.DeleteAsync(file.Id, cancellationToken);
            }
        }
    }
}
