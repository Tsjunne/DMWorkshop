using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Creatures
{
    public class CreatureReadModel
    {
        public string Name { get; set; }
        public int MaxHP { get; set; }
        public int AC { get; set; }
        public int PassivePerception { get; set; }
    }
}
