using DMWorkshop.Model.Core;
using System;
using System.Linq;
using Xunit;

namespace DMWorkshop.Tests
{
    public class DiceTest
    {
        [Fact]
        public void ADieRollsWithinItsRange()
        {
            var die = new Die(6);

            var rolls = Enumerable.Repeat(true, 100).Select(_ => die.Roll()).ToArray();
            Assert.True(rolls.All(roll => 1 <= roll && roll <= 6));
        }
    }
}
