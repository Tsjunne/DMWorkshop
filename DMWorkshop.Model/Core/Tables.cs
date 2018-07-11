using DMWorkshop.DTO.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMWorkshop.Model.Core
{
    public static class Tables
    {
        public static IDictionary<double, int> XpByCr = new Dictionary<double, int>
        {
            { 0, 10},
            { 0.125, 25},
            { 0.25, 50},
            { 0.50, 100},
            { 1, 200},
            { 2, 450},
            { 3, 700},
            { 4, 1100},
            { 5, 1800},
            { 6, 2300},
            { 7, 2900},
            { 8, 3900},
            { 9, 5000},
            { 10, 5900},
            { 11, 7200},
            { 12, 8400},
            { 13, 10000},
            { 14, 11500},
            { 15, 13000},
            { 16, 15000},
            { 17, 18000},
            { 18, 20000},
            { 19, 22000},
            { 20, 25000},
            { 21, 33000},
            { 22, 41000},
            { 23, 50000},
            { 24, 62000},
            { 25, 75000},
            { 26, 90000},
            { 27, 105000},
            { 28, 120000},
            { 29, 135000},
            { 30, 155000}
        };

        public static IDictionary<Size, Die> DieBySize = new Dictionary<Size, Die>
        {
            { Size.Tiny, new Die(4)},
            { Size.Small, new Die(6)},
            { Size.Medium, new Die(8)},
            { Size.Large, new Die(10)},
            { Size.Huge, new Die(12)},
            { Size.Gargantuan, new Die(20)}
        };

        public static IDictionary<Classes, Die> DieByClass = new Dictionary<Classes, Die>
        {
            { Classes.Barbarian, new Die(12) },
            { Classes.Bard, new Die(8) },
            { Classes.BloodHunter, new Die(10) },
            { Classes.Cleric, new Die(8) },
            { Classes.Druid, new Die(8) },
            { Classes.Fighter, new Die(10) },
            { Classes.Monk, new Die(8) },
            { Classes.Paladin, new Die(10) },
            { Classes.Ranger, new Die(10) },
            { Classes.Rogue, new Die(8) },
            { Classes.Sorcerer, new Die(6) },
            { Classes.Warlock, new Die(8) },
            { Classes.Wizard, new Die(6) },
        };


        public static IDictionary<Classes, IEnumerable<Ability>> SavesByClass = new Dictionary<Classes, IEnumerable<Ability>>
        {
            { Classes.Barbarian, new [] { Ability.Strength, Ability.Constitution } },
            { Classes.Bard, new [] { Ability.Dexterity, Ability.Charisma } },
            { Classes.BloodHunter, new [] { Ability.Strength, Ability.Wisdom } },
            { Classes.Cleric, new [] { Ability.Wisdom, Ability.Charisma } },
            { Classes.Druid, new [] { Ability.Intelligence, Ability.Wisdom } },
            { Classes.Fighter, new [] { Ability.Strength, Ability.Constitution } },
            { Classes.Monk, new [] { Ability.Strength, Ability.Dexterity } },
            { Classes.Paladin, new [] { Ability.Wisdom, Ability.Charisma } },
            { Classes.Ranger, new [] { Ability.Strength, Ability.Dexterity } },
            { Classes.Rogue, new [] { Ability.Intelligence, Ability.Dexterity } },
            { Classes.Sorcerer, new [] { Ability.Constitution, Ability.Charisma } },
            { Classes.Warlock, new [] { Ability.Wisdom, Ability.Charisma } },
            { Classes.Wizard, new [] { Ability.Intelligence, Ability.Wisdom } },
        };

        public static IDictionary<string, Race> RacesByName = new[]
        {
            new Race("Dwarf", Size.Medium, 25, 60),
            new Race("Elf", Size.Medium, 30, 60),
            new Race("Halfling", Size.Small, 25),
            new Race("Human", Size.Medium, 30),
            new Race("Dragonborn", Size.Medium, 30),
            new Race("Gnome", Size.Small, 25, 60),
            new Race("Half-Elf", Size.Medium, 30, 60),
            new Race("Half-Orc", Size.Medium, 30, 60),
            new Race("Tiefling", Size.Medium, 30, 60),
            new Race("Aasimar", Size.Medium, 30, 60),
            new Race("Firbolg", Size.Medium, 30),
            new Race("Goliath", Size.Medium, 30),
            new Race("Kenku", Size.Medium, 30),
            new Race("Lizardfolk", Size.Medium,
                new Dictionary<Speed, int>
                {
                    { Speed.Walk, 30 },
                    { Speed.Swim, 30 }
                }),
            new Race("Tabaxi", Size.Medium, 30, 60),
            new Race("Triton", Size.Medium,
                new Dictionary<Speed, int>
                {
                    { Speed.Walk, 30 },
                    { Speed.Swim, 30 }
                })
        }.ToDictionary(x => x.Name);

        public static IDictionary<Skill, Ability> StandardSkillAbilities = new Dictionary<Skill, Ability>
        {
            {Skill.Acrobatics,Ability.Dexterity },
            {Skill.AnimalHandling, Ability.Wisdom },
            {Skill.Arcana, Ability.Intelligence },
            {Skill.Athletics,Ability.Strength },
            {Skill.Deception,Ability.Charisma },
            {Skill.History,Ability.Intelligence },
            {Skill.Initiative,Ability.Dexterity },
            {Skill.Insight,Ability.Wisdom },
            {Skill.Intimidation,Ability.Charisma },
            {Skill.Investigation,Ability.Intelligence },
            {Skill.Medicine,Ability.Wisdom },
            {Skill.Nature,Ability.Intelligence },
            {Skill.Perception,Ability.Wisdom },
            {Skill.Performance,Ability.Charisma },
            {Skill.Persuasion,Ability.Charisma },
            {Skill.Religion,Ability.Intelligence },
            {Skill.SleightOfHand,Ability.Dexterity },
            {Skill.Stealth,Ability.Dexterity },
            {Skill.Survival,Ability.Wisdom }
        };
    }
}
