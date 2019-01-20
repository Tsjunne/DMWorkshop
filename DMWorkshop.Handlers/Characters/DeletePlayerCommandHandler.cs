using DMWorkshop.DTO.Campaign;
using DMWorkshop.Model.Campaign;
using MediatR;
using MongoDB.Driver;
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

        protected override Task Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<Party>("players");
            return collection.DeleteOneAsync(x => x.Name == request.Name, cancellationToken);
        }
    }
}
