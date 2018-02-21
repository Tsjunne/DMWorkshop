using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMWorkshop.Model.Core
{
    public enum Ability
    {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma
    }

    public static class AbilityExtensions
    {
        public static Dictionary<Ability, AbilityScore> AsAbilityScores(this IEnumerable<int> scores)
        {
            if (scores.Count() != 6) throw new ArgumentException("Need 6 scores");
            
            return scores.Select((s, i) => new AbilityScore((Ability)i, s)).ToDictionary(x => x.Ability);
        }
    }
}
