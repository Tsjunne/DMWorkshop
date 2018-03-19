using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.Model.Characters
{
    [Flags]
    public enum AttackType
    {
        Melee = 1,
        Ranged = 2,
        Weapon = 4,
        Spell = 8
    }

    public enum DamageType
    {
        Piercing,
        Slashing,
        Bludgeoning,
        Fire,
        Acid
    }

    public class Damage
    {
        public int DieCount { get; set; }
        public int DieSize { get; set; }
        public int DamageBonus { get; set; }
        public DamageType DamageType { get; set; }
    }

    public class Attack
    {
        public string Name { get; set; }
        public IEnumerable<Damage> Damage { get; set; }
        public AttackType Type { get; set; }
        public bool Finesse { get; set; }
        public int Range { get; set; }
        public int? MaxRange { get; set; }
        public string Info { get; set; }
    }

    public class SpecialAbility
    {
        public string Name { get; set; }
        public string Info { get; set; }
    }
}
