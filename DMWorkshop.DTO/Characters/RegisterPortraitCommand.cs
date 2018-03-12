using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DMWorkshop.DTO.Characters
{
    public class RegisterPortraitCommand : IRequest
    {
        public string Name { get; set; }
        public Stream Image { get; set; }
    }
}
