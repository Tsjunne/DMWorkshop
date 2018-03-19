using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMWorkshop.Model.Core;

namespace DMWorkshop.Model.Characters
{
    public class Player : Character
    {
        public Player(string name, IEnumerable<int> scores, Classes @class, string race, int maxHp, int level, IEnumerable<string> gear, IEnumerable<Skill> skills, IEnumerable<Skill> expertise)
            : base(name, scores, level, level, gear, skills, expertise)
        {
            MaxHP = maxHp;
            _saves = Tables.SavesByClass[@class];
            Class = @class;
            Race = race;

            var r = Tables.RacesByName[race];
            Speed = r.Speed;
            Size = r.Size;
            Senses = r.Vision;
        }

        public Classes Class { get; }
        public string Race { get; }
        public int MaxHP { get; }
    }
}
