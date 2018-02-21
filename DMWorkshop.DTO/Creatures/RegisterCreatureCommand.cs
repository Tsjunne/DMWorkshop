using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Creatures
{
    public class RegisterCreatureCommand : IRequest
    {
        public int[] Scores { get; set; }
        public string Size { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string[] Gear { get; set; } = new string[] { };
        public Dictionary<string, int> Skills { get; set; } = new Dictionary<string, int>();
    }
}
