using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Campaign
{
    public class GetPartiesQuery : IRequest<IEnumerable<PartyReadModel>>
    {
    }
}
