using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Characters
{
    public class FindPartyQuery : IRequest<IEnumerable<CreatureReadModel>>
    {
        public string Party { get; set; }
    }
}
