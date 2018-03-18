using DMWorkshop.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMWorkshop.Model.Items
{
    public class Armor : Gear
    {
        public Armor(string name, ItemSlot armorSlot, int ac, int? dexModLimit = null, IEnumerable<Ability> additionalModifiers = null)
            :base(name, armorSlot)
        {
            AC = ac;
            DexModLimit = dexModLimit;
            AdditionalModifiers = additionalModifiers ?? new Ability[] { };
            ArmorSlot = armorSlot;
        }

        public int AC { get; }
        public int? DexModLimit { get; }
        public IEnumerable<Ability> AdditionalModifiers { get; }
        public ItemSlot ArmorSlot { get; }

        public int ModifyAC(IDictionary<Ability, AbilityScore> abilityScores)
        {
            if (ArmorSlot == ItemSlot.Chest)
            {
                var dexMod = abilityScores[Ability.Dexterity].Modifier;
                var limit = DexModLimit ?? dexMod;

                var ac = AC + Math.Min(dexMod, limit);

                foreach (var modifier in AdditionalModifiers)
                {
                    ac += abilityScores[modifier].Modifier;
                }

                return ac;
            }
            
            return AC;
        }
    }

    public abstract class ArmorModifier
    {
        public abstract int ModifyAC(int ac, IDictionary<Ability, AbilityScore> abilityScores);
    }
}
