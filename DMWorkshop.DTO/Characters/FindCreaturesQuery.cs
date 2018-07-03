using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Characters
{
    public class FindCreaturesQuery : IRequest<IEnumerable<CreatureReadModel>>
    {
        public string MonsterList { get; set; }
    }
}
