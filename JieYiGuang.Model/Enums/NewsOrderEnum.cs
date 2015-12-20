using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JieYiGuang.Dto
{
    /// <summary>
    /// 资讯排序枚举，默认排序（MarkHot Desc,Hits Desc,NumFavorite Desc,NumGood Desc,NumComment Desc,Orders,CreateTime Desc）
    /// 创建人：梁志远
    /// 创建时间：2015-05-26
    /// </summary>
    public enum NewsOrderEnum : int
    {
        /// <summary>
        /// 热门降序
        /// </summary>
        MarkHot = 11,
        /// <summary>
        /// 排序号升序
        /// </summary>
        Orders = 21,
        /// <summary>
        /// 创建时间降序
        /// </summary>
        CreateTime = 12,
        /// <summary>
        /// 点击数降序
        /// </summary>
        Hits = 13,
        /// <summary>
        /// 评论数降序
        /// </summary>
        Comment = 14,
        /// <summary>
        /// 收藏数降序
        /// </summary>
        Favorite = 15,
        /// <summary>
        /// 好评数降序
        /// </summary>
        Good = 16
    }
}
