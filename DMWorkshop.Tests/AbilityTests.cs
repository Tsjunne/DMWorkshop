﻿using DMWorkshop.DTO.Core;
using DMWorkshop.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DMWorkshop.Tests
{
    public class AbilityTests
    {
        [Fact]
        public void ModifiersAreCalculatedCorrecly()
        {
            var expectations = new (int score, int modifier)[]
            {
                (1, -5),
                (2, -4),
                (3, -4),
                (4, -3),
                (5, -3),
                (6, -2),
                (7, -2),
                (8, -1),
                (9, -1),
                (10, 0),
                (11, 0),
                (12, 1),
                (13, 1),
                (14, 2),
                (15, 2),
                (16, 3),
                (17, 3),
                (18, 4),
                (19, 4),
                (20, 5)
            };

            foreach (var expectation in expectations)
            {
                Assert.Equal(new AbilityScore(Ability.Strength, expectation.score).Modifier, expectation.modifier);
            }
        }
    }
}
