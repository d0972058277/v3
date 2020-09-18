using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using Newtonsoft.Json;
using V3Lib.Creationals.Abstractions;
using V3Lib.Filters.Abstractions;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Conditions
{
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public class DefinedCondition : Condition
    {
        /// <summary>
        /// 是否隱藏
        /// </summary>
        public Hide Hide { get; set; } = null;

        /// <summary>
        /// 開始時間條件
        /// </summary>
        public StartDateTimeCondit Start { get; set; }

        /// <summary>
        /// 結束時間條件
        /// </summary>
        public EndDateTimeCondit End { get; set; }

        /// <summary>
        /// 社區Id
        /// </summary>
        public HashSet<Community> Communities { get; set; } = new HashSet<Community>();

        /// <summary>
        /// 城市
        /// </summary>
        public HashSet<City> Cities { get; set; } = new HashSet<City>();

        protected List<IConditionField> GetConditionFields()
        {
            var conditionFields = new List<IConditionField>();

            if (Hide != null) conditionFields.Add(Hide);
            if (Start != null) conditionFields.Add(Start);
            if (End != null) conditionFields.Add(End);

            var communities = GetCommunities();
            if (communities.Any()) conditionFields.Add(communities);

            var locations = GetLocations();
            if (locations.Any()) conditionFields.Add(locations);

            return conditionFields;
        }

        public CommunityStructs GetCommunities()
        {
            var communities = new CommunityStructs();

            foreach (var community in Communities)
            {
                communities.Add(new CommunityStruct { Id = community.Id });
            }

            return communities;
        }

        public Locations GetLocations()
        {
            var locations = new Locations();

            foreach (var city in Cities)
            {
                var location = new Location();
                location.City = city.Name;

                foreach (var area in city.Areas)
                {
                    location.Area = area.Name;

                    foreach (var village in area.Villages)
                    {
                        location.Village = village.Name;
                    }
                }

                locations.Add(location);
            }

            return locations;
        }

        public override bool Pass(IFilter filter)
        {
            foreach (var conditionField in GetConditionFields())
            {
                if (filter.Filter(conditionField) == false)
                    return false;
            }
            return true;
        }
    }
}