using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JieYiGuang.Dto;

namespace JieYiGuang.DTO
{
    /// <summary>
    /// 查询参数模型
    /// </summary>
    public class NewsParam
    {
        /// <summary>
        /// 类别编号
        /// </summary>
        public int ClassID { get; set; }

        /// <summary>
        /// 栏目编号
        /// </summary>
        public int ItemID { get; set; }

        /// <summary>
        /// 资讯标记类别
        /// </summary>
        public string MarkName { get; set; }

        /// <summary>
        /// 资讯标签（以“,”分隔）
        /// </summary>
        public string Flags { get; set; }

        /// <summary>
        /// 资讯标签（以“,”分隔）
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 搜索关键自
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 起始记录号
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// 返回记录数
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// 类别数组（以“,”分隔）
        /// </summary>
        public string ClassIDStr { get; set; }

        /// <summary>
        /// 资讯查询的排序
        /// </summary>
        public NewsOrderEnum[] OrderEnum { get; set; }

        /// <summary>
        /// 推荐的标识ID
        /// </summary>
        public int RecommendID { get; set; }

        /// <summary>
        /// 关注用户GUID
        /// </summary>
        public Guid UserGUID { get; set; }

        /// <summary>
        /// 信息GUID
        /// </summary>
        public Guid FromGUID { get; set; }

        /// <summary>
        /// 是否英文版
        /// </summary>
        public bool IsEn { get; set; }

        /// <summary>
        /// 推荐首页
        /// </summary>
        public int MarkIndex { get; set; }
    }
}