using AutoMapper;
using DMWorkshop.DTO.Characters;
using DMWorkshop.DTO.Core;
using DMWorkshop.DTO.Items;
using DMWorkshop.Handlers.Mapping;
using DMWorkshop.Model.Characters;
using DMWorkshop.Model.Core;
using DMWorkshop.Model.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DMWorkshop.Tests
{
    public class CreatureTests
    {
        [Fact]
        public void CanCreateAGoblin()
        {
            var goblin = CreateGoblin();
            
            Assert.Equal(7, goblin.MaxHP);
            Assert.Equal(15, goblin.AC);
            Assert.Equal(9, goblin.PassivePerception);
            Assert.Equal(6, goblin.SkillModifiers[Skill.Stealth]);
        }

        [Fact]
        public void CanMapGoblin()
        {
            var goblin = CreateGoblin();

            Mapper.Initialize(cfg => 
            {
                cfg.AddProfile<AutoMapperMapping>();
            });

            var mapped = Mapper.Map<CreatureReadModel>(goblin);

            Assert.NotNull(mapped);
            Assert.Equal(2, mapped.Attacks.Count());
        }

        private Creature CreateGoblin()
        {
            var scores = new[] { 8, 14, 10, 10, 8, 8 };

            var gear = new[]
            {
                new Armor("Leather armor", ItemSlot.Chest, 1),
                new Armor("Shield", ItemSlot.LeftHand, 2)
            };

            var attacks = new[]
            {
                new Model.Characters.Attack { Name="Scimitar", Type=AttackType.Melee|AttackType.Weapon, Finesse=true, Damage=new[]{ new Damage { DieCount=1, DieSize=6, Type= DamageType.Slashing} } },
                new Model.Characters.Attack { Name="Shortbow", Type=AttackType.Ranged|AttackType.Weapon, Range=80, MaxRange=320, Damage=new[]{ new Damage { DieCount=1, DieSize=6, Type= DamageType.Piercing} } }
            };

            var goblin = new Creature("Goblin", scores, Size.Small, new Dictionary<Speed, int>() { { Speed.Walk, 30 } }, 2, 0.25, 
                Enumerable.Empty<string>(), Enumerable.Empty<Ability>(), Enumerable.Empty<Skill>(), new[] { Skill.Stealth }, 
                new Dictionary<Senses, int>(), attacks, Enumerable.Empty<SpecialAbility>());

            goblin.Equip(gear);

            return goblin;
        }
    }
}
