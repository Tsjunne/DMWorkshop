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
    public class RegisterPartyCommandHandler : AsyncRequestHandler<RegisterPartyCommand>
    {
        private readonly IMongoDatabase _database;

        public RegisterPartyCommandHandler(IMongoDatabase database)
        {
            _database = database;
        }

        protected override Task Handle(RegisterPartyCommand command, CancellationToken cancellationToken)
        {
            var party = new Party(
                command.Name,
                command.Members
                );

            return _database.Save("parties", x => x.Name == party.Name, party, cancellationToken);
        }
    }
}
