using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.Model.Core
{
    public class Die
    {
        public Die(int sides)
        {
            Sides = sides;
            Facing = 1;
        }

        public int Sides { get; }

        public int Facing { get; protected set; }

        public double Average => (Sides + 1) / 2d;

        public int Roll()
        {
            Facing = new Random().Next(1, Sides);
            return Facing;
        }
    }

    public static class DieExtensions
    {
    }
}
