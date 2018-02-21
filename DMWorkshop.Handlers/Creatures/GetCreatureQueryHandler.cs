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
    public class GetCreatureQueryHandler : IRequestHandler<GetCreatureQuery, CreatureReadModel>
    {
        private readonly IMongoDatabase _database;
        private readonly IMapper _mapper;

        public GetCreatureQueryHandler(IMongoDatabase database, IMapper mapper)
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

            var gear = await _database.GetCollection<Gear>("gear").AsQueryable()
                .Where(c => creature.Gear.Contains(c.Name))
                .ToListAsync();

            creature.Equip(gear);

            return _mapper.Map<CreatureReadModel>(creature);
        }
    }
}
