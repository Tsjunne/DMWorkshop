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
            var scores = new[] { 8, 14, 10, 10, 8, 8 };

            var gear = new[]
            {
                new Armor("Leather armor", ItemSlot.Chest, 1),
                new Armor("Shield", ItemSlot.LeftHand, 2)
            };

            var goblin = new Creature("Goblin", scores, Size.Small, new Dictionary<Speed, int>() { { Speed.Walk, 30} }, 2, 0.25, Enumerable.Empty<string>(), Enumerable.Empty<Ability>(), Enumerable.Empty<Skill>(), new[] { Skill.Stealth}, new Dictionary<Vision, int>());

            goblin.Equip(gear);
            
            Assert.Equal(7, goblin.MaxHP);
            Assert.Equal(15, goblin.AC);
            Assert.Equal(9, goblin.PassivePerception);
            Assert.Equal(6, goblin.SkillModifiers[Skill.Stealth]);
        }
    }
}
