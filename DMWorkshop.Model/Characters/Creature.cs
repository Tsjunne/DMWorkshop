using DMWorkshop.DTO.Core;
using DMWorkshop.Model.Core;
using DMWorkshop.Model.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMWorkshop.Model.Characters
{
    public class Creature : Character
    {
        public Creature(string name, IEnumerable<int> scores, Size size, IDictionary<Speed, int> speed, int level, double cr, IEnumerable<string> gear, IEnumerable<Ability> saves, IEnumerable<Skill> skills, IEnumerable<Skill> expertise, IDictionary<Senses, int> senses, 
            IEnumerable<Attack> attacks, IEnumerable<SpecialAbility> specialAbilities, Ability? castingAbility = null, 
            IEnumerable<Condition> conditionImmunities = null, IEnumerable<DamageType> damageImmunities = null, IEnumerable<DamageType> damageResistances = null, IEnumerable<DamageType> damageVulnerabilities = null)
            : base(name, scores, level, cr, gear, skills, expertise)
        {
            _saves = saves;
            Speed = speed;
            Size = size;
            Senses = senses;
            Attacks = attacks ?? new Attack[] { };
            SpecialAbilities = specialAbilities ?? new SpecialAbility[] { };
            CastingAbility = castingAbility;
            ConditionImmunities = conditionImmunities ?? new Condition[] { };
            DamageImmunities = damageImmunities ?? new DamageType[] { };
            DamageResistances = damageResistances ?? new DamageType[] { };
            DamageVulnerabilities = damageVulnerabilities ?? new DamageType[] { };
            
        }

        public IEnumerable<Ability> Saves => _saves;
        public int MaxHP => Convert.ToInt32(Math.Floor(Tables.DieBySize[Size].Average * Level) + (AbilityScores[Ability.Constitution].Modifier * Level));
        public IEnumerable<Attack> Attacks { get; }
        public IEnumerable<SpecialAbility> SpecialAbilities { get; }
        public Ability? CastingAbility { get; }
        public IEnumerable<Condition> ConditionImmunities { get; }
        public IEnumerable<DamageType> DamageImmunities { get; }
        public IEnumerable<DamageType> DamageResistances { get; }
        public IEnumerable<DamageType> DamageVulnerabilities { get; }

        public IEnumerable<ResultingAttack> ModifiedAttacks
        {
            get
            {
                var modified = from attack in Attacks
                               let ability = (attack.Type.HasFlag(AttackType.Spell) && CastingAbility.HasValue) ? CastingAbility.Value : attack.Finesse || attack.Type.HasFlag(AttackType.Ranged) ? Ability.Dexterity : Ability.Strength
                               select new ResultingAttack
                               {
                                   Name = attack.Name,
                                   Type = attack.Type,
                                   Hit = attack.Bonus + AbilityScores[ability].Modifier + (attack.Proficient == false ? 0 : Proficiency),
                                   Info = attack.Info,
                                   Range = attack.Range,
                                   MaxRange = attack.MaxRange,
                                   Reaction = attack.Reaction,
                                   Damage = attack.Damage.Select((d, i) => new Damage
                                   {
                                       Type = d.Type,
                                       DieCount = d.DieCount,
                                       DieSize = d.DieSize,
                                       Bonus = d.Bonus + (i == 0 ? attack.Bonus + AbilityScores[ability].Modifier : 0)
                                   })
                               };

                return modified;
            }
        }

        public override Die HitDie => Tables.DieBySize[Size];
    }

    public class ResultingAttack
    {
        public string Name { get; set; }
        public int Hit { get; set; }
        public IEnumerable<Damage> Damage { get; set; }
        public AttackType Type { get; set; }
        public int Range { get; set; }
        public int? MaxRange { get; set; }
        public string Info { get; set; }
        public bool? Reaction { get; set; }
    }
}

