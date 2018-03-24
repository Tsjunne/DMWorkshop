using DMWorkshop.Model.Core;
using System.Collections.Generic;

namespace DMWorkshop.Model.Characters
{
    public class Attack 
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public IEnumerable<Damage> Damage { get; set; }
        public AttackType Type { get; set; }
        public bool Finesse { get; set; }
        public int Range { get; set; }
        public int? MaxRange { get; set; }
        public bool? Reaction { get; set; }
    }
}
