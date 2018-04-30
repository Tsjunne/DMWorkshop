using AutoMapper;
using DMWorkshop.DTO.Items;
using DMWorkshop.Model.Items;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMWorkshop.Handlers.Items
{
    public class FindArmorQueryHandler : IRequestHandler<FindArmorQuery, ArmorInfo[]>
    {
        private readonly IMongoDatabase _database;
        private readonly IMapper _mapper;

        public FindArmorQueryHandler(IMongoDatabase database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<ArmorInfo[]> Handle(FindArmorQuery request, CancellationToken cancellationToken)
        {
            var query = _database.GetCollection<Armor>("gear").AsQueryable();

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => x.Name.ToLower().Contains(request.Search.ToLower()));
            }

            var armor = await query.ToListAsync();

            return _mapper.Map<ArmorInfo[]>(armor);
        }
    }
}
