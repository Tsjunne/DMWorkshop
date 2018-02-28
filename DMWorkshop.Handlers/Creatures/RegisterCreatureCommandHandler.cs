using DMWorkshop.DTO.Creatures;
using DMWorkshop.Model.Creatures;
using DMWorkshop.Model.Core;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DMWorkshop.Handlers.Creatures
{
    public class RegisterCreatureCommandHandler : IRequestHandler<RegisterCreatureCommand>
    {
        private readonly IMongoDatabase _database;

        public RegisterCreatureCommandHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public Task Handle(RegisterCreatureCommand command, CancellationToken cancellationToken)
        {
            var creature = new Creature(
                command.Name,
                command.Scores,
                Enum.Parse<Size>(command.Size),
                command.Level,
                command.CR ?? command.Level,
                command.Gear,
                command.Saves.Select(x => Enum.Parse<Ability>(x)),
                command.Skills.Select(x => Enum.Parse<Skill>(x)),
                command.Expertise.Select(x => Enum.Parse<Skill>(x))
                );

            return _database.Save("creatures", x => x.Name == creature.Name, creature);
        }
    }
}
