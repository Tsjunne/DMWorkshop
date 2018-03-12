using DMWorkshop.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.Model.Characters
{
    public class Race
    {
        public Race(string name, Size size, int speed)
        {
            Name = name;
            Size = size;
            Speed = new Dictionary<Speed, int> { { Characters.Speed.Walk, speed } };
        }

        public Race(string name, Size size, IDictionary<Speed, int> speed)
        {
            Name = name;
            Size = size;
            Speed = speed;
        }

        public string Name { get; }
        public Size Size { get; }
        public IDictionary<Speed, int> Speed { get; }
    }
}
