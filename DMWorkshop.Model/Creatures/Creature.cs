using DMWorkshop.Model.Core;
using DMWorkshop.Model.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMWorkshop.Model.Creatures
{
    public class Creature
    {
        private readonly Dictionary<Ability, AbilityScore> _abilityScores;
        private readonly Dictionary<ItemSlot, Gear> _equipedGear = new Dictionary<ItemSlot, Gear>();
        private readonly Die _hitDie;
        private readonly int _proficiency;

        public Creature(string name, IEnumerable<int> scores, Size size, int level, double cr, IEnumerable<string> gear, IEnumerable<Ability> saves, IEnumerable<Skill> skills, IEnumerable<Skill> expertise)
        {
            Name = name;
            Scores = scores;
            Size = size;
            Level = level;
            CR = cr;
            Gear = gear;
            Saves = saves;
            Skills = skills;
            Expertise = expertise;
            _abilityScores = scores.AsAbilityScores();
            _hitDie = DetermineHitDie(size);
            _proficiency = DetermineProficiencyBonus(cr);
            var clothes = new Armor("Clothes", ItemSlot.Chest, 0);
            Equip(clothes);
        }

        public int AC
        {
            get
            {
                var ac = 10;

                foreach (var armor in _equipedGear.Values.OfType<Armor>().Distinct())
                {
                    ac += armor.ModifyAC(_abilityScores);
                }

                return ac;
            }
        }
        public int PassivePerception
        {
            get
            {
                var proficiencyBonus = 0;

                if (Skills.Contains(Skill.Perception)) proficiencyBonus = _proficiency;
                if (Expertise.Contains(Skill.Perception)) proficiencyBonus = _proficiency * 2;

                return 10 + _abilityScores[Ability.Wisdom].Modifier + proficiencyBonus;
            }
        }

        public int MaxHP => Convert.ToInt32(Math.Floor(_hitDie.Average * Level) + (_abilityScores[Ability.Constitution].Modifier * Level));
        
        public string Name { get; }
        public IEnumerable<int> Scores { get; }
        public IEnumerable<int> Modifiers => _abilityScores.Values.Select(x => x.Modifier);
        public Size Size { get; }
        public int Level { get; }
        public double CR { get; }
        public int XP => _xpTable[CR];
        public IEnumerable<string> Gear { get; }
        public IEnumerable<Ability> Saves { get; }
        public IEnumerable<Skill> Skills { get; }
        public IEnumerable<Skill> Expertise { get; }

        public int Check(Ability ability)
        {
            var d20 = new Die(20);

            return d20.Roll() + _abilityScores[ability].Modifier;
        }

        public void Equip(params Gear[] gear)
        {
            Equip(gear.AsEnumerable());
        }

        public void Equip(IEnumerable<Gear> gear)
        {
            foreach (var item in gear)
            {
                foreach (var slot in item.Slots)
                {
                    if (_equipedGear.ContainsKey(slot))
                    {
                        Unequip(_equipedGear[slot]);
                    }

                    _equipedGear.Add(slot, item);
                }
            }
        }

        public void Unequip(Gear gear)
        {
            foreach (var slot in gear.Slots)
            {
                _equipedGear.Remove(slot);
            }
        }

        private Die DetermineHitDie(Size size)
        {
            switch (size)
            {
                case Size.Tiny:
                    return new Die(4);
                case Size.Small:
                    return new Die(6);
                case Size.Medium:
                    return new Die(8);
                case Size.Large:
                    return new Die(10);
                case Size.Huge:
                    return new Die(12);
                case Size.Gargantuan:
                    return new Die(20);
                default:
                    throw new ArgumentException("unknown size", nameof(size));
            }
        }

        private int DetermineProficiencyBonus(double cr)
        {
            if (cr >= 29) return 9;
            if (cr >= 25) return 8;
            if (cr >= 21) return 7;
            if (cr >= 17) return 6;
            if (cr >= 13) return 5;
            if (cr >= 9) return 4;
            if (cr >= 5) return 3;

            return 2;
        }
        
        private static IDictionary<double, int> _xpTable = new Dictionary<double, int>
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
    }
}
