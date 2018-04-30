using DMWorkshop.DTO.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMWorkshop.DTO.Characters
{
    public class RegisterPlayerCommand : IRequest
    {
        public int[] Scores { get; set; }
        public string Class { get; set; }
        public string Race { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string[] Gear { get; set; } = new string[] { };
        public Skill[] Skills { get; set; } = new Skill[] { };
        public Skill[] Expertise { get; set; } = new Skill[] { };
        public int MaxHp { get; set; }
    }
}
