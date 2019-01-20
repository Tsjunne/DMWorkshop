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
    public class DeletePartyCommandHandler : AsyncRequestHandler<DeletePartyCommand>
    {
        private readonly IMongoDatabase _database;

        public DeletePartyCommandHandler(IMongoDatabase database)
        {
            _database = database;
        }

        protected override Task Handle(DeletePartyCommand request, CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<Party>("parties");
            return collection.DeleteOneAsync(x => x.Name == request.Name, cancellationToken);
        }
    }
}
