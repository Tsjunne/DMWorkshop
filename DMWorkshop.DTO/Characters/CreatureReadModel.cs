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
        public double CR { get; set; }
        public int Level { get; set; }
        public int PassivePerception { get; set; }
        public int InitiativeModifier { get; set; }
        public int[] Scores { get; set; }
        public int[] Modifiers { get; set; }
        public string Size { get; set; }
        public IDictionary<string, int> Speed { get; set; }
        public IDictionary<string, int> SkillModifiers { get; set; }
        public IDictionary<string, int> SavingThrows { get; set; }
        public IDictionary<string, int> Senses { get; set; }
        public IEnumerable<AttackReadModel> Attacks { get; set; }
        public IEnumerable<SpecialAbilityInfo> SpecialAbilities { get; set; }
        public IEnumerable<string> ConditionImmunities { get; set; }
        public IEnumerable<string> DamageImmunities { get; set; }
        public IEnumerable<string> DamageResistances { get; set; }
        public IEnumerable<string> DamageVulnerabilities { get; set; }
    }

    public class AttackReadModel
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public int Hit { get; set; }
        public IEnumerable<DamageInfo> Damage { get; set; }
        public int Type { get; set; }
        public int Range { get; set; }
        public int? MaxRange { get; set; }
        public bool? Reaction { get; set; }
    }
}
