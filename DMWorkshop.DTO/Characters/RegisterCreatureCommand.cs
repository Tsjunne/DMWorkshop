using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Characters
{
    public class RegisterCreatureCommand : IRequest
    {
        public int[] Scores { get; set; }
        public string Size { get; set; }
        public IDictionary<string, int> Speed { get; set; } = new Dictionary<string, int> { { "Walk", 30} };
        public IDictionary<string, int> Vision { get; set; } = new Dictionary<string, int> { };
        public int Level { get; set; }
        public string Name { get; set; }
        public string[] Gear { get; set; } = new string[] { };
        public string[] Skills { get; set; } = new string[] { };
        public string[] Expertise { get; set; } = new string[] { };
        public string[] Saves { get; set; } = new string[] { };
        public double? CR { get; set; }
        public IEnumerable<AttackInfo> Attacks { get; set; }
        public IEnumerable<SpecialAbilityInfo> SpecialAbilities { get; set; }
    }

    public class DamageInfo
    {
        public int DieCount { get; set; }
        public int DieSize { get; set; }
        public int DamageBonus { get; set; }
        public string DamageType { get; set; }
    }

    public class AttackInfo
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public bool Finesse { get; set; }
        public int Range { get; set; } = 5;
        public int? MaxRange { get; set; }
        public IEnumerable<DamageInfo> Damage { get; set; }
        public string Info { get; set; }
    }

    public class SpecialAbilityInfo
    {
        public string Name { get; set; }
        public string Info { get; set; }
    }
}
