﻿using DMWorkshop.DTO.Characters;
using DMWorkshop.Model.Core;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using DMWorkshop.Model.Characters;

namespace DMWorkshop.Handlers.Characters
{
    public class RegisterPlayerCommandHandler : AsyncRequestHandler<RegisterPlayerCommand>
    {
        private readonly IMongoDatabase _database;

        public RegisterPlayerCommandHandler(IMongoDatabase database)
        {
            _database = database;
        }

        protected override Task Handle(RegisterPlayerCommand command, CancellationToken cancellationToken)
        {
            var player = new Player(
                command.Name,
                command.Scores,
                (Classes)Enum.Parse(typeof(Classes), command.Class),
                command.Race,
                command.MaxHp,
                command.Level,
                command.Gear,
                command.Skills,
                command.Expertise
                );

            return _database.Save("players", x => x.Name == player.Name, player, cancellationToken);
        }
    }
}
