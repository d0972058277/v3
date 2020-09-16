using System;
using System.Collections.Generic;
using V3Lib.Models.Conditions;

namespace V3Lib
{
    public static class FakeDataFactory
    {
        public static string RandomName => Guid.NewGuid().ToString();

        public static bool RandomBoolean
        {
            get
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                return random.Next(0, 2) > 0;
            }
        }

        public static DefinedCondition FakeDefinedCondition()
        {
            var result = new DefinedCondition();

            if (RandomBoolean) result.Hide = new Hide();
            if (RandomBoolean) result.Start = new StartDateTime() { DateTime = DateTime.Now };
            if (RandomBoolean) result.End = new EndDateTime() { DateTime = DateTime.Now };
            if (RandomBoolean) result.Communities.Add(new Community { Id = RandomName });
            if (RandomBoolean) result.Cities.Add(new City { Name = RandomName });

            return result;
        }

        public static Dictionary<string, DefinedCondition> FakeDefinedConditions()
        {
            var result = new Dictionary<string, DefinedCondition>();

            for (int i = 0; i < 10; i++)
            {
                result.Add(Guid.NewGuid().ToString(), FakeDataFactory.FakeDefinedCondition());
            }

            return result;
        }
    }
}