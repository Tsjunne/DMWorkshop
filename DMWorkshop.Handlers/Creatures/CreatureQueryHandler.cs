using AutoMapper;
using DMWorkshop.DTO.Creatures;
using DMWorkshop.Model.Creatures;
using DMWorkshop.Model.Items;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMWorkshop.Handlers.Creatures
{
    public class CreatureQueryHandler : 
        IRequestHandler<GetCreatureQuery, CreatureReadModel>,
        IRequestHandler<FindCreaturesQuery, IEnumerable<CreatureReadModel>>
    {
        private readonly IMongoDatabase _database;
        private readonly IMapper _mapper;

        public CreatureQueryHandler(IMongoDatabase database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<CreatureReadModel> Handle(GetCreatureQuery query, CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<Creature>("creatures");

            var creature = await collection.AsQueryable()
                .Where(c => c.Name == query.Name)
                .FirstAsync(cancellationToken);

            await LoadGear(creature);

            return _mapper.Map<CreatureReadModel>(creature);
        }

        public async Task<IEnumerable<CreatureReadModel>> Handle(FindCreaturesQuery request, CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<Creature>("creatures");

            var creatures = await collection.AsQueryable()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            await LoadGear(creatures.ToArray());

            return _mapper.Map<IEnumerable<CreatureReadModel>>(creatures);
        }

        private async Task LoadGear(params Creature[] creatures)
        {
            var gearNames = creatures.SelectMany(c => c.Gear).Distinct();

            var gear = await _database.GetCollection<Gear>("gear").AsQueryable()
                .Where(x => gearNames.Contains(x.Name))
                .ToListAsync();

            foreach (var creature in creatures)
            {
                var equipment = gear.Where(g => creature.Gear.Any(x => g.Name == x));

                creature.Equip(equipment);
            }
        }
    }
}
