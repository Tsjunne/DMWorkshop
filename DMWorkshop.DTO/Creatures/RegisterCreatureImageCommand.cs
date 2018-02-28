using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DMWorkshop.DTO.Creatures
{
    public class RegisterCreatureImageCommand : IRequest
    {
        public string Name { get; set; }
        public Stream Image { get; set; }
    }
}
