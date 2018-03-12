using DMWorkshop.Model.Core;
using DMWorkshop.Model.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMWorkshop.Model.Characters
{
    public class Creature : Character
    {
        public Creature(string name, IEnumerable<int> scores, Size size, IDictionary<Speed, int> speed, int level, double cr, IEnumerable<string> gear, IEnumerable<Ability> saves, IEnumerable<Skill> skills, IEnumerable<Skill> expertise)
            : base(name, scores, level, cr, gear, skills, expertise)
        {
            _saves = saves;
            Speed = speed;
            Size = size;
        }

        public IEnumerable<Ability> Saves => _saves;
        public int MaxHP => Convert.ToInt32(Math.Floor(Tables.DieBySize[Size].Average * Level) + (AbilityScores[Ability.Constitution].Modifier * Level));
    }
}
