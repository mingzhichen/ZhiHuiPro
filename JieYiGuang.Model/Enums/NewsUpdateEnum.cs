using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JieYiGuang.Dto
{
    /// <summary>
    /// 资讯属性更新类别
    /// </summary>
    public enum NewsUpdateEnum
    {
        /// <summary>
        /// 点击数
        /// </summary>
        Hits,
        /// <summary>
        /// 收藏数
        /// </summary>
        Favorite,
        /// <summary>
        /// 评论数
        /// </summary>
        Comment,
        /// <summary>
        /// 好评数
        /// </summary>
        Good,
        /// <summary>
        /// 子集数
        /// </summary>
        ItemNum
    }
}