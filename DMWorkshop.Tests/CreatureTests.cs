using DMWorkshop.Model.Core;
using DMWorkshop.Model.Creatures;
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
                new Armor("leather armor", ItemSlot.Chest, 1),
                new Armor("shield", ItemSlot.LeftHand, 2)
            };

            var goblin = new Creature("Goblin", scores, Size.Small, 2, 0.25, Enumerable.Empty<string>(), Enumerable.Empty<Ability>(), Enumerable.Empty<Skill>());

            goblin.Equip(gear);

            Assert.Equal(7, goblin.MaxHP);
            Assert.Equal(15, goblin.AC);
            Assert.Equal(9, goblin.PassivePerception);
        }
    }
}
