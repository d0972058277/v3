using System;
using System.Collections.Generic;

namespace V3Lib.Models.Conditions
{
    public class DefinedCondition : Condition
    {
        /// <summary>
        /// 是否隱藏
        /// </summary>
        public Hide Hide { get; set; }

        /// <summary>
        /// 有效開始時間 (代表可輸出開始時間)
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// 有效結束時間 (代表可輸出結束時間)
        /// </summary>
        public DateTime? End { get; set; }

        /// <summary>
        /// 社區Id
        /// </summary>
        public HashSet<Community> Communities { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public HashSet<City> Cities { get; set; }
    }
}