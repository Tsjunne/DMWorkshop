using DMWorkshop.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.Model.Items
{
    public class Armor : Gear
    {
        public Armor(string name, ItemSlot armorSlot, int ac, int? dexModLimit = null)
            :base(name, armorSlot)
        {
            AC = ac;
            DexModLimit = dexModLimit;
            ArmorSlot = armorSlot;
        }

        public int AC { get; }
        public int? DexModLimit { get; }
        public ItemSlot ArmorSlot { get; }

        public int ModifyAC(IDictionary<Ability, AbilityScore> abilityScores)
        {
            if (ArmorSlot == ItemSlot.Chest)
            {
                var dexMod = abilityScores[Ability.Dexterity].Modifier;
                var limit = DexModLimit ?? dexMod;
                return AC + Math.Max(dexMod, limit);
            }

            return AC;
        }
    }

    public abstract class ArmorModifier
    {
        public abstract int ModifyAC(int ac, IDictionary<Ability, AbilityScore> abilityScores);
    }
}
