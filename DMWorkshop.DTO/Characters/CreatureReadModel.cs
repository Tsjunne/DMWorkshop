using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Characters
{
    public class CreatureReadModel
    {
        public string Name { get; set; }
        public int MaxHP { get; set; }
        public int AC { get; set; }
        public int XP { get; set; }
        public int PassivePerception { get; set; }
        public int InitiativeModifier { get; set; }
        public int[] Scores { get; set; }
        public int[] Modifiers { get; set; }
    }
}
