using DMWorkshop.DTO.Core;
using DMWorkshop.Model.Core;
using System;
using System.Linq;

namespace DMWorkshop.Model.Characters
{
    public class Damage
    {
        public int DieCount { get; set; }
        public int DieSize { get; set; }
        public int Bonus { get; set; }
        public DamageType Type { get; set; }
        public int Average => Convert.ToInt32(Math.Floor(new Die(DieSize).Average * DieCount)) + Bonus;

        public bool IsPhysical => new[] { DamageType.Bludgeoning, DamageType.Piercing, DamageType.Slashing }.Contains(Type);
    }
}
