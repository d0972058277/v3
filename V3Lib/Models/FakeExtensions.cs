using System;
using System.Collections.Generic;
using System.Linq;
using KnstArchitecture.SequentialGuid;
using V3Lib.Models.Additionals;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Models.Medias;
using V3Lib.Models.Operations;
using V3Lib.Models.Reactions;
using V3Lib.Models.Styles;

namespace V3Lib.Models
{
    public static class FakeExtensions
    {
        public static string RandomName => SequentialGuid.NewGuid().ToString();

        public static bool RandomBoolean
        {
            get
            {
                Random random = new Random(SequentialGuid.NewGuid().GetHashCode());
                return random.Next(100) <= 50;
            }
        }

        public static int RandomNumber
        {
            get
            {
                Random random = new Random(SequentialGuid.NewGuid().GetHashCode());
                return random.Next(-10, 10);
            }
        }

        public static T RandomPeek<T>(this IEnumerable<T> enumable)
        {
            return enumable.OrderBy(arg => SequentialGuid.NewGuid()).Take(1).Single();
        }

        public static MemberAdditional Fake(this MemberAdditional extension)
        {
            var fake = extension.Clone();

            if (RandomBoolean) fake.GA = new GoogleAnalytics { Value = RandomName };

            if (RandomBoolean) fake.Point = new Point();

            if (RandomBoolean) fake.Number = new Number { Value = RandomNumber };

            while (RandomBoolean)
            {
                fake.Tags.Add(new Tag { Value = RandomName });
            }

            if (RandomBoolean) fake.Ad = new Ad();

            if (RandomBoolean) fake.Start = new StartDateTime() { DateTime = DateTime.Now.AddHours(RandomNumber) };

            if (RandomBoolean) fake.End = new EndDateTime() { DateTime = DateTime.Now.AddHours(RandomNumber) };

            return fake;
        }

        public static DefinedCondition Fake(this DefinedCondition condition)
        {
            var fake = condition.Clone();

            if (RandomBoolean) fake.Hide = new Hide();

            if (RandomBoolean) fake.Start = new StartDateTimeCondit() { DateTime = DateTime.Now };

            if (RandomBoolean) fake.End = new EndDateTimeCondit() { DateTime = DateTime.Now };

            while (RandomBoolean)
            {
                fake.Communities.Add(new Community { Id = RandomName });
            }

            while (RandomBoolean)
            {
                var city = new City { Name = RandomName };
                while (RandomBoolean)
                {
                    var area = new Area { Name = RandomName };
                    while (RandomBoolean)
                    {
                        area.Villages.Add(new Village { Name = RandomName });
                    }
                    city.Areas.Add(area);
                }
                fake.Cities.Add(city);
            }

            return fake;
        }

        public static MemberComponent Fake(this MemberComponent component, Dictionary<string, DefinedCondition> conditions, int subNumber)
        {
            var fake = component.Fake(conditions);

            for (int i = 0; i < subNumber; i++)
            {
                fake.AddLowerLayer(component.Fake(conditions));
            }

            return fake;
        }

        public static MemberComponent Fake(this MemberComponent component, Dictionary<string, DefinedCondition> conditions)
        {
            var fake = component.Clone();
            fake.ComponentId = SequentialGuid.NewGuid();

            if (RandomBoolean)
            {
                fake.Condition = new ReferenceCondition { Ref = conditions.RandomPeek().Key };
            }
            else
            {
                fake.Condition = new DefinedCondition().Fake();
            }

            if (RandomBoolean) fake.Title = RandomName;

            if (RandomBoolean) fake.SubTitle = RandomName;

            if (RandomBoolean) fake.Description = RandomName;

            if (RandomBoolean)
            {
                var style = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.IsSubclassOf(typeof(Style))).RandomPeek();
                fake.Style = (Style) Activator.CreateInstance(style);
            }

            if (RandomBoolean)
            {
                fake.Additional = new MemberAdditional().Fake();
            }

            if (RandomBoolean)
            {
                var reaction = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.IsSubclassOf(typeof(Reaction))).RandomPeek();
                fake.Reaction = (Reaction) Activator.CreateInstance(reaction);
                fake.Reaction.Path = RandomName;
            }

            while (RandomBoolean)
            {
                var media = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.IsSubclassOf(typeof(Media))).RandomPeek();
                var instance = (Media) Activator.CreateInstance(media);
                instance.Title = RandomName;
                instance.Path = RandomName;
                fake.Medias.Add(instance);
            }

            if (RandomBoolean)
            {
                fake.Operations.Add(new MoreOperation());
            }

            return fake;
        }

        public static LazyComponent Fake(this LazyComponent component, Dictionary<string, DefinedCondition> conditions)
        {
            var fake = component.Clone();
            fake.ComponentId = SequentialGuid.NewGuid();

            fake.Path = RandomName;

            if (RandomBoolean)
            {
                fake.Condition = new ReferenceCondition { Ref = conditions.RandomPeek().Key };
            }
            else
            {
                fake.Condition = new DefinedCondition().Fake();
            }

            return fake;
        }

        public static Component Fake(this Component component, Dictionary<string, DefinedCondition> conditions, int subNumber)
        {
            if (RandomBoolean)
            {
                component = new MemberComponent().Fake(conditions, subNumber);
            }
            else
            {
                component = new LazyComponent().Fake(conditions);
            }

            return component;
        }

        public static ConfigPageComponent Fake(this ConfigPageComponent component)
        {
            var fake = component.Clone();

            fake.ComponentId = SequentialGuid.NewGuid();
            fake.Title = RandomName;
            fake.SubTitle = RandomName;

            return fake;
        }
    }
}