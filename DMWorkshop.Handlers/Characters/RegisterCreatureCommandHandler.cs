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

namespace DMWorkshop.Handlers.Characters
{
    public class RegisterCreatureCommandHandler : IRequestHandler<RegisterCreatureCommand>
    {
        private readonly IMongoDatabase _database;
        private readonly IMapper _mapper;

        public RegisterCreatureCommandHandler(IMongoDatabase database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public Task Handle(RegisterCreatureCommand command, CancellationToken cancellationToken)
        {
            var creature = new Creature(
                command.Name,
                command.Scores,
                Enum.Parse<Size>(command.Size, true),
                command.Speed.ToDictionary((KeyValuePair<string, int> x) => Enum.Parse<Speed>(x.Key, true), (KeyValuePair<string, int> x) => x.Value),
                command.Level,
                command.CR ?? command.Level,
                command.Gear,
                command.Saves.Select(x => Enum.Parse<Ability>(x, true)),
                command.Skills.Select(x => Enum.Parse<Skill>(x, true)),
                command.Expertise.Select(x => Enum.Parse<Skill>(x, true)),
                command.Senses.ToDictionary((KeyValuePair<string, int> x) => Enum.Parse<Senses>(x.Key, true), (KeyValuePair<string, int> x) => x.Value),
                _mapper.Map<IEnumerable<Attack>>(command.Attacks),
                _mapper.Map<IEnumerable<SpecialAbility>>(command.SpecialAbilities),
                string.IsNullOrEmpty(command.CastingAbility) ? default(Ability?) : Enum.Parse<Ability>(command.CastingAbility),
                command.ConditionImmunities.Select(x => Enum.Parse<Condition>(x, true)),
                command.DamageImmunities.Select(x => Enum.Parse<DamageType>(x, true)),
                command.DamageResistances.Select(x => Enum.Parse<DamageType>(x, true)),
                command.DamageVulnerabilities.Select(x => Enum.Parse<DamageType>(x, true))
                );

            return _database.Save("creatures", x => x.Name == creature.Name, creature, cancellationToken);
        }
    }
}
