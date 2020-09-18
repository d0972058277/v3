using System;
using System.Collections.Generic;
using MessagePack;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Additionals
{
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public class MemberAdditional : Additional
    {
        /// <summary>
        /// Google 分析
        /// </summary>
        public GoogleAnalytics GA { get; set; }

        /// <summary>
        /// 點
        /// </summary>
        public Point Point { get; set; }

        /// <summary>
        /// 數字
        /// </summary>
        public Number Number { get; set; }

        /// <summary>
        /// 標籤
        /// </summary>
        public HashSet<Tag> Tags { get; set; } = new HashSet<Tag>();

        /// <summary>
        /// 廣告
        /// </summary>
        public Ad Ad { get; set; }

        /// <summary>
        /// 開始顯示
        /// </summary>
        public StartDateTime? Start { get; set; }

        /// <summary>
        /// 結束顯示
        /// </summary>
        public EndDateTime? End { get; set; }
    }
}