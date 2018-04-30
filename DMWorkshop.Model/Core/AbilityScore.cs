using DMWorkshop.DTO.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.Model.Core
{
    public class AbilityScore
    {
        public AbilityScore(Ability ability, int score)
        {
            Ability = ability;
            Score = score;
        }

        public Ability Ability { get; protected set; }
        public int Score { get; protected set; }

        public int Modifier
        {
            get
            {
                return (int)Math.Floor((Score - 10) / 2d);
            }
        }
    }
}
