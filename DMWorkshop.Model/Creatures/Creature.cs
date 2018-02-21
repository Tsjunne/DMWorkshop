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
        private readonly int[] _characteristics;

        public Creature(string name, IEnumerable<int> characteristics, Size size, int level, IEnumerable<string> gear, Dictionary<Skill, int> skills)
        {
            _abilityScores = characteristics.AsAbilityScores();
            _hitDie = DetermineHitDie(size);

            Name = name;
            Characteristics = characteristics;
            Size = size;
            Level = level;
            Gear = gear;
            Skills = skills ?? new Dictionary<Skill, int>();
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
                int skill;
                return 10 + (Skills.TryGetValue(Skill.Perception, out skill) ? skill : _abilityScores[Ability.Wisdom].Modifier);
            }
        }

        public int MaxHP => Convert.ToInt32(Math.Floor(_hitDie.Average * Level) + (_abilityScores[Ability.Constitution].Modifier * Level));
        
        public string Name { get; }
        public IEnumerable<int> Characteristics { get; }
        public Size Size { get; }
        public int Level { get; }
        public IEnumerable<string> Gear { get; }
        public Dictionary<Skill, int> Skills { get; }

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
    }
}
