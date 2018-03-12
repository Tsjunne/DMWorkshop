using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DMWorkshop.DTO.Characters
{
    public class GetPortraitQuery : IRequest<Stream>
    {
        public string Name { get; set; }
    }
}
