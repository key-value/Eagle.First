using System;

namespace Eagle.ViewModel
{
    public class ShowTable
    {

        /// <summary>
        ///     餐桌ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        ///     餐桌名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        ///     假删
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        ///     包厢之类
        /// </summary>
        public string TableType { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}