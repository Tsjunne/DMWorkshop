using DMWorkshop.DTO.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Items
{
    public class ArmorInfo
    {
        public string Name { get; set; }
        public int AC { get; set; }
        public ItemSlot Slot { get; set; }
        public int? DexModLimit { get; set; }

        public IEnumerable<Ability> AdditionalModifiers { get; set; } = new Ability[] { };
    }
}
