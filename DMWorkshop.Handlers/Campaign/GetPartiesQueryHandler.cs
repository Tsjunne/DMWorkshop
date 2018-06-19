using AutoMapper;
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
    public class GetPartiesQueryHandler : IRequestHandler<GetPartiesQuery, IEnumerable<PartyReadModel>>
    {
        private readonly IMongoDatabase _database;
        private readonly IMapper _mapper;

        public GetPartiesQueryHandler(IMongoDatabase database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PartyReadModel>> Handle(GetPartiesQuery request, CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<Party>("parties");

            var parties = await collection.AsQueryable()
                .ToListAsync(cancellationToken);

            return _mapper.Map< IEnumerable<PartyReadModel>>(parties);
        }
    }
}
