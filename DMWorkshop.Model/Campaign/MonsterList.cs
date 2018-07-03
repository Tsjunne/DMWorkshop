using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.Model.Campaign
{
    public class MonsterList
    {
        public MonsterList(string name, IEnumerable<string> members)
        {
            Name = name;
            Members = members;
        }

        public string Name { get; }
        public IEnumerable<string> Members { get; }
    }
}
