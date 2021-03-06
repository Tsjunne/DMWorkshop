﻿using DMWorkshop.DTO.Core;
using DMWorkshop.DTO.Items;
using DMWorkshop.Model.Core;
using DMWorkshop.Model.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMWorkshop.Model.Characters
{
    public abstract class Character
    {
        private readonly Dictionary<ItemSlot, Gear> _equipedGear = new Dictionary<ItemSlot, Gear>();
        protected IEnumerable<Ability> _saves;

        public Character(string name, IEnumerable<int> scores, int level, double cr, IEnumerable<string> gear, IEnumerable<Skill> skills, IEnumerable<Skill> expertise)
        {
            Name = name;
            Scores = scores;
            Level = level;
            CR = cr;
            Gear = gear;
            Skills = skills;
            Expertise = expertise;
            AbilityScores = scores.AsAbilityScores();
            Proficiency = DetermineProficiencyBonus(cr);
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
                    ac += armor.ModifyAC(AbilityScores);
                }

                return ac;
            }
        }
        public int PassivePerception => 10 + GetSkillModifier(Ability.Wisdom, Skill.Perception);
        public int InitiativeModifier => GetSkillModifier(Ability.Dexterity, Skill.Initiative);

        public string Name { get; }
        public IEnumerable<int> Scores { get; }
        public IEnumerable<int> Modifiers => AbilityScores.Values.Select(x => x.Modifier);

        public Dictionary<Ability, AbilityScore> AbilityScores { get; }
        public int Proficiency { get; }
        public Size Size { get; protected set; }
        public IDictionary<Speed, int> Speed { get; protected set; }
        public int Level { get; }
        public double CR { get; }
        public int XP => Tables.XpByCr[CR];
        public IEnumerable<string> Gear { get; }
        public IEnumerable<Skill> Skills { get; }
        public IEnumerable<Skill> Expertise { get; }
        public IDictionary<Senses, int> Senses { get; protected set; }

        public IDictionary<Skill, int> SkillModifiers => Skills.Concat(Expertise).ToDictionary(x => x, x => GetSkillModifier(Tables.StandardSkillAbilities[x], x));
        public IDictionary<Ability, int> SavingThrows => _saves.ToDictionary(x => x, x => GetAbilityModifier(x));

        public abstract Die HitDie { get; }
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

        private int GetSkillModifier(Ability ability, Skill skill)
        {
            var proficiencyBonus = 0;

            if (Skills.Contains(skill)) proficiencyBonus = Proficiency;
            if (Expertise.Contains(skill)) proficiencyBonus = Proficiency * 2;

            return AbilityScores[ability].Modifier + proficiencyBonus;
        }

        protected int GetAbilityModifier(Ability ability)
        {
            var proficiencyBonus = 0;

            if (_saves.Contains(ability)) proficiencyBonus = Proficiency;

            return AbilityScores[ability].Modifier + proficiencyBonus;
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

    }
}
