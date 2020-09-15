using System;
using System.Collections.Generic;
using V3Lib.Filters.Abstractions;

namespace V3Lib.Models.Conditions
{
    public class DefinedCondition : Condition
    {
        /// <summary>
        /// 是否隱藏
        /// </summary>
        public Hide Hide { get; set; }

        /// <summary>
        /// 開始時間條件
        /// </summary>
        public StartDateTime Start { get; set; }

        /// <summary>
        /// 結束時間條件
        /// </summary>
        public EndDateTime End { get; set; }

        /// <summary>
        /// 社區Id
        /// </summary>
        public HashSet<Community> Communities { get; set; } = new HashSet<Community>();

        /// <summary>
        /// 城市
        /// </summary>
        public HashSet<City> Cities { get; set; } = new HashSet<City>();

        public List<Location> GetLocations()
        {
            var locations = new List<Location>();
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

        public override void Pass(IFilter filter)
        {
            if (filter.Verify(Hide))
            {
                _relationComponent.Isolated();
                return;
            }

            if (filter.Verify(Start))
            {
                _relationComponent.Isolated();
                return;
            }

            if (filter.Verify(End))
            {
                _relationComponent.Isolated();
                return;
            }

            foreach (var community in Communities)
            {
                if (filter.Verify(community))
                {
                    _relationComponent.Isolated();
                    return;
                }
            }

            var locations = GetLocations();
            foreach (var location in locations)
            {
                if (filter.Verify(location))
                {
                    _relationComponent.Isolated();
                    return;
                }
            }
        }
    }
}