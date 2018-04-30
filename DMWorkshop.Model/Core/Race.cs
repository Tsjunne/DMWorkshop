using DMWorkshop.DTO.Core;
using DMWorkshop.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.Model.Core
{
    public class Race
    {
        public Race(string name, Size size, int speed, int? darkvision = null)
            : this(name, size, new Dictionary<Speed, int> { { DTO.Core.Speed.Walk, speed } }, darkvision)
        {
        }

        public Race(string name, Size size, IDictionary<Speed, int> speed, int? darkvision = null)
        {
            Name = name;
            Size = size;
            Speed = speed;
            Vision = new Dictionary<Senses, int>();

            if (darkvision.HasValue) Vision.Add(Senses.Darkvision, darkvision.Value);
        }

        public string Name { get; }
        public Size Size { get; }
        public IDictionary<Speed, int> Speed { get; }
        public IDictionary<Senses, int> Vision { get; }
    }
}
