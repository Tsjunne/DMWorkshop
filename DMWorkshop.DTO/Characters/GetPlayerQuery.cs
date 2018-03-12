using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Characters
{
    public class GetPlayerQuery : IRequest<CreatureReadModel>
    {
        public string Name { get; set; }
    }
}
