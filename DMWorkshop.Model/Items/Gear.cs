using System.Collections.Generic;
using DMWorkshop.DTO.Items;

namespace DMWorkshop.Model.Items
{
    public abstract class Gear
    {
        private readonly IEnumerable<ItemSlot> _slots;

        public Gear(string name, params ItemSlot[] slots)
        {
            Name = name;
            _slots = slots;
        }

        public IEnumerable<ItemSlot> Slots => _slots;

        public string Name { get; }
    }
}
