using DMWorkshop.DTO.Core;
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
        public Size Size { get; set; }
        public IDictionary<Speed, int> Speed { get; set; }
        public IDictionary<Skill, int> SkillModifiers { get; set; }
        public IDictionary<Ability, int> SavingThrows { get; set; }
        public IDictionary<Senses, int> Senses { get; set; }
        public IEnumerable<AttackReadModel> Attacks { get; set; }
        public IEnumerable<SpecialAbilityInfo> SpecialAbilities { get; set; }
        public IEnumerable<Condition> ConditionImmunities { get; set; }
        public IEnumerable<DamageType> DamageImmunities { get; set; }
        public IEnumerable<DamageType> DamageResistances { get; set; }
        public IEnumerable<DamageType> DamageVulnerabilities { get; set; }
        public int HitDie { get; set; }
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
        public bool? Legendary { get; set; }
    }
}
