using System;

namespace V3Lib.Models.Extensions
{
    public struct DateTimeSetting
    {
        /// <summary>
        /// 開始顯示
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// 結束顯示
        /// </summary>
        public DateTime? End { get; set; }

        /// <summary>
        /// 計時器
        /// </summary>
        public DateTime? Timer { get; set; }
    }
}