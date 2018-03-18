using AutoMapper;
using DMWorkshop.DTO.Characters;
using DMWorkshop.Model.Campaign;
using DMWorkshop.Model.Characters;
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

namespace DMWorkshop.Handlers.Characters
{
    public class PlayerQueryHandler : 
        IRequestHandler<GetPlayerQuery, CreatureReadModel>,
        IRequestHandler<FindPartyQuery, IEnumerable<CreatureReadModel>>
    {
        private readonly IMongoDatabase _database;
        private readonly IMapper _mapper;

        public PlayerQueryHandler(IMongoDatabase database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<CreatureReadModel> Handle(GetPlayerQuery query, CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<Player>("players");

            var player = await collection.AsQueryable()
                .Where(c => c.Name == query.Name)
                .FirstAsync(cancellationToken);

            await LoadGear(player);

            return _mapper.Map<CreatureReadModel>(player);
        }

        public async Task<IEnumerable<CreatureReadModel>> Handle(FindPartyQuery query, CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<Player>("players");

            IList<Player> players;

            if (query.Party == "All")
            {
                players = await collection.AsQueryable()
                    .OrderBy(x => x.Name)
                    .ToListAsync(cancellationToken);
            }
            else
            {
                var party = await _database.GetCollection<Party>("parties").AsQueryable()
                    .Where(x => x.Name == query.Party)
                    .SingleAsync(cancellationToken);

                players = await collection.AsQueryable()
                    .Where(x => party.Members.Contains(x.Name))
                    .OrderBy(x => x.Name)
                    .ToListAsync(cancellationToken);
            }

            await LoadGear(players.ToArray());

            return _mapper.Map<IEnumerable<CreatureReadModel>>(players);
        }

        private async Task LoadGear(params Player[] players)
        {
            var gearNames = players.SelectMany(c => c.Gear).Distinct();

            var gear = await _database.GetCollection<Gear>("gear").AsQueryable()
                .Where(x => gearNames.Contains(x.Name))
                .ToListAsync();

            foreach (var player in players)
            {
                var equipment = gear.Where(g => player.Gear.Any(x => g.Name == x));

                player.Equip(equipment);
            }
        }
    }
}
