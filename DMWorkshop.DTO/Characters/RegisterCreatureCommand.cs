using DMWorkshop.DTO.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Characters
{
    public class RegisterCreatureCommand : IRequest
    {
        public int[] Scores { get; set; }
        public Size Size { get; set; }
        public IDictionary<Speed, int> Speed { get; set; } = new Dictionary<Speed, int> { { Core.Speed.Walk, 30} };
        public IDictionary<Senses, int> Senses { get; set; } = new Dictionary<Senses, int> { };
        public int Level { get; set; }
        public string Name { get; set; }
        public string[] Gear { get; set; } = new string[] { };
        public Skill[] Skills { get; set; } = new Skill[] { };
        public Skill[] Expertise { get; set; } = new Skill[] { };
        public Ability[] Saves { get; set; } = new Ability[] { };
        public double? CR { get; set; }
        public IEnumerable<AttackInfo> Attacks { get; set; }
        public IEnumerable<SpecialAbilityInfo> SpecialAbilities { get; set; }
        public Ability? CastingAbility { get; set; }
        public Condition[] ConditionImmunities { get; set; } = new Condition[] { };
        public DamageType[] DamageImmunities { get; set; } = new DamageType[] { };
        public DamageType[] DamageResistances { get; set; } = new DamageType[] { };
        public DamageType[] DamageVulnerabilities { get; set; } = new DamageType[] { };
    }

    public class DamageInfo
    {
        public int DieCount { get; set; }
        public int DieSize { get; set; }
        public int Bonus { get; set; }
        public DamageType Type { get; set; }
        public int Average { get; set; }
    }

    public class AttackInfo
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public int Type { get; set; }
        public bool Finesse { get; set; }
        public int? Hit { get; set; }
        public int Range { get; set; } = 5;
        public int? MaxRange { get; set; }
        public IEnumerable<DamageInfo> Damage { get; set; }
        public bool? Reaction { get; set; }
        public int Bonus { get; set; }
    }

    public class SpecialAbilityInfo
    {
        public string Name { get; set; }
        public string Info { get; set; }
    }
}
