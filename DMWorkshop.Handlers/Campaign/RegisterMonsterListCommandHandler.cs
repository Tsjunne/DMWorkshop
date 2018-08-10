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
    public class RegisterMonsterListCommandHandler : AsyncRequestHandler<RegisterMonsterListCommand>
    {
        private readonly IMongoDatabase _database;

        public RegisterMonsterListCommandHandler(IMongoDatabase database)
        {
            _database = database;
        }

        protected override Task Handle(RegisterMonsterListCommand command, CancellationToken cancellationToken)
        {
            var list = new MonsterList(
                command.Name,
                command.Members
                );

            return _database.Save("monsterLists", x => x.Name == list.Name, list, cancellationToken);
        }
    }
}
