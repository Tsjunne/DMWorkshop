using DMWorkshop.DTO.Characters;
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
using AutoMapper;
using DMWorkshop.DTO.Core;

namespace DMWorkshop.Handlers.Characters
{
    public class RegisterCreatureCommandHandler : AsyncRequestHandler<RegisterCreatureCommand>
    {
        private readonly IMongoDatabase _database;
        private readonly IMapper _mapper;

        public RegisterCreatureCommandHandler(IMongoDatabase database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        protected override Task Handle(RegisterCreatureCommand command, CancellationToken cancellationToken)
        {
            var creature = new Creature(
                command.Name,
                command.Scores,
                command.Size,
                command.Speed,
                command.Level,
                command.CR ?? command.Level,
                command.Gear,
                command.Saves,
                command.Skills,
                command.Expertise,
                command.Senses,
                _mapper.Map<IEnumerable<Attack>>(command.Attacks),
                _mapper.Map<IEnumerable<SpecialAbility>>(command.SpecialAbilities),
                command.CastingAbility,
                command.ConditionImmunities,
                command.DamageImmunities,
                command.DamageResistances,
                command.DamageVulnerabilities
                );

            return _database.Save("creatures", x => x.Name == creature.Name, creature, cancellationToken);
        }
    }
}
