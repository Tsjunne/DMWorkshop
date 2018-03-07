using DMWorkshop.DTO.Creatures;
using DMWorkshop.Model.Creatures;
using DMWorkshop.Model.Items;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMWorkshop.Handlers.Creatures
{
    public class CopyContentCommandHandler : IRequestHandler<CopyContentCommand>
    {
        private readonly IMongoDatabase _source;
        private readonly IMongoDatabase _destination;

        public CopyContentCommandHandler(IMongoDatabase source, IMongoDatabase destination)
        {
            _source = source;
            _destination = destination;
        }

        public async Task Handle(CopyContentCommand message, CancellationToken cancellationToken)
        {
            var gear = await _source.GetCollection<Gear>("gear").AsQueryable()
                .ToListAsync();

            foreach (var item in gear)
            {
                await _destination.Save("gear", x => x.Name == item.Name, item);
            }

            var creatures = await _source.GetCollection<Creature>("creatures").AsQueryable()
                .ToListAsync(cancellationToken);

            foreach (var creature in creatures)
            {
                await _destination.Save("creatures", x => x.Name == creature.Name, creature);
            }


            var source = new GridFSBucket(_source, new GridFSBucketOptions
            {
                BucketName = "creatures"
            });

            using (var cursor = await source.FindAsync(Builders<GridFSFileInfo>.Filter.Empty))
            {
                var files = await cursor.ToListAsync();

                var destination = new GridFSBucket(_destination, new GridFSBucketOptions
                {
                    BucketName = "creatures"
                });

                foreach (var file in files)
                {
                    using (var stream = await source.OpenDownloadStreamByNameAsync(file.Filename, null, cancellationToken))
                    {
                        await destination.UploadFromStreamAsync(file.Filename, stream);
                    }
                }
            }
        }
    }
}
