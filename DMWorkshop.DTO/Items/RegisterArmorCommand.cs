using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Items
{
    public class RegisterArmorCommand : IRequest
    {
        public string Name { get; set; }
        public int AC { get; set; }
        public string Slot { get; set; }
        public int? DexModLimit { get; set; }

        public IEnumerable<string> AdditionalModifiers { get; set; } = new string[] { };
    }
}
