using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Creatures
{
    public class FindCreaturesQuery : IRequest<IEnumerable<CreatureReadModel>>
    {
        public string CreatureSet { get; set; }
    }
}
