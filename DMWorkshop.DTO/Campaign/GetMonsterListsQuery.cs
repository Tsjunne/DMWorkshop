using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Campaign
{
    public class GetMonsterListsQuery : IRequest<IEnumerable<CreatureListReadModel>>
    {
    }
}
