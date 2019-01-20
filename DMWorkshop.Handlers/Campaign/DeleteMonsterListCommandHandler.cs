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
    public class DeleteMonsterListCommandHandler : AsyncRequestHandler<DeleteMonsterListCommand>
    {
        private readonly IMongoDatabase _database;

        public DeleteMonsterListCommandHandler(IMongoDatabase database)
        {
            _database = database;
        }

        protected override Task Handle(DeleteMonsterListCommand request, CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<MonsterList>("monsterLists");
            return collection.DeleteOneAsync(x => x.Name == request.Name, cancellationToken);
        }
    }
}
