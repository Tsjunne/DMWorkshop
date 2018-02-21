using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Creatures
{
    public class GetCreatureQuery : IRequest<CreatureReadModel>
    {
        public string Name { get; set; }
    }
}
