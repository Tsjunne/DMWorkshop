using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DMWorkshop.DTO.Creatures
{
    public class GetCreatureImageQuery : IRequest<Stream>
    {
        public string Name { get; set; }
    }
}
