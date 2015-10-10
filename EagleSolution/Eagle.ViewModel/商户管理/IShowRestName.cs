using System;

namespace Eagle.ViewModel
{
    public interface IShowRestName
    {

        /// <summary>
        /// 主键
        /// </summary>
        Guid ID { get; set; }

        /// <summary>
        /// 餐厅名称
        /// </summary>
        string Name { get; set; }
    }
}