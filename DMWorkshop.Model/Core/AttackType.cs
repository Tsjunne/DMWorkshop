using System;

namespace DMWorkshop.Model.Core
{
    [Flags]
    public enum AttackType
    {
        Melee = 1,
        Ranged = 2,
        Weapon = 4,
        Spell = 8
    }
}
