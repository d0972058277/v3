using System.Collections.Generic;

namespace V3Lib.Models.Extensions
{
    public class ExtensionBase : Extension
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
        public HashSet<Tag> Tags { get; set; }

        /// <summary>
        /// 廣告
        /// </summary>
        public Ad Ad { get; set; }

        /// <summary>
        /// 時間設定
        /// </summary>
        public DateTimeSetting DateTimeSetting { get; set; }
    }
}