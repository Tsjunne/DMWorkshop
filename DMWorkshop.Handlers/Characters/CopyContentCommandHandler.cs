using DMWorkshop.DTO.Characters;
using DMWorkshop.Model.Characters;
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

namespace DMWorkshop.Handlers.Characters
{
    public class CopyContentCommandHandler : AsyncRequestHandler<CopyContentCommand>
    {
        private readonly IMongoDatabase _source;
        private readonly IMongoDatabase _destination;

        public CopyContentCommandHandler(IMongoDatabase source, IMongoDatabase destination)
        {
            _source = source;
            _destination = destination;
        }

        protected override async Task Handle(CopyContentCommand message, CancellationToken cancellationToken)
        {
            await MigrateGear(cancellationToken);
            await MigrateCreatures(cancellationToken);
            await MigratePlayers(cancellationToken);
            await MigratePortraits(cancellationToken);
        }

        private async Task MigratePortraits(CancellationToken cancellationToken)
        {
            var source = new GridFSBucket(_source, new GridFSBucketOptions
            {
                BucketName = "portraits"
            });

            using (var cursor = await source.FindAsync(Builders<GridFSFileInfo>.Filter.Empty))
            {
                var files = await cursor.ToListAsync();

                var destination = new GridFSBucket(_destination, new GridFSBucketOptions
                {
                    BucketName = "portraits"
                });

                foreach (var file in files)
                {
                    using (var stream = await source.OpenDownloadStreamByNameAsync(file.Filename, null, cancellationToken))
                    {
                        await destination.UploadFromStreamAsync(file.Filename, stream, null, cancellationToken);
                    }
                }
            }
        }

        private async Task MigratePlayers(CancellationToken cancellationToken)
        {
            var players = await _source.GetCollection<Player>("players").AsQueryable()
                            .ToListAsync(cancellationToken);

            foreach (var player in players)
            {
                await _destination.Save("players", x => x.Name == player.Name, player, cancellationToken);
            }
        }

        private async Task MigrateCreatures(CancellationToken cancellationToken)
        {
            var creatures = await _source.GetCollection<Creature>("creatures").AsQueryable()
                            .ToListAsync(cancellationToken);

            foreach (var creature in creatures)
            {
                await _destination.Save("creatures", x => x.Name == creature.Name, creature, cancellationToken);
            }
        }

        private async Task MigrateGear(CancellationToken cancellationToken)
        {
            var gear = await _source.GetCollection<Gear>("gear").AsQueryable()
                .ToListAsync(cancellationToken);

            foreach (var item in gear)
            {
                await _destination.Save("gear", x => x.Name == item.Name, item, cancellationToken);
            }
        }
    }
}
