namespace JieYiGuang.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Collections;

    /// <summary>
    /// 资讯类别表
    /// </summary>
    [Serializable]
    public partial class T_NewsItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public T_NewsItem()
        {
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
            this.GUID = Guid.NewGuid();
        }
        #region 属性

        /// <summary>
        /// 主键（数值自增）
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// GUID
        /// </summary>
        public Guid GUID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人（字符串50，保存创建人名称）
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 修改人（字符串50，保存创建人名称）
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 数据状态
        /// </summary>
        public int MarkStatus { get; set; }
        /// <summary>
        /// 标记名称（字符串50）
        /// </summary>
        public string MarkName { get; set; }
        /// <summary>
        /// SEO标题（长度250）
        /// </summary>
        public string SeoTitle { get; set; }
        /// <summary>
        /// SEO关键字（长度250）
        /// </summary>
        public string SeoKeywords { get; set; }
        /// <summary>
        /// SEO描述（长度1000）
        /// </summary>
        public string SeoDescription { get; set; }
        /// <summary>
        /// 会员GUID编号
        /// </summary>
        public Guid UserGUID { get; set; }
        /// <summary>
        /// 管理员GUID编号
        /// </summary>
        public Guid AdminGUID { get; set; }
        /// <summary>
        /// 是否生成静态
        /// </summary>
        public int MarkHtml { get; set; }
        /// <summary>
        /// 栏目 ID
        /// </summary>
        public int ClassID { get; set; }


        /// <summary>
        /// 项目 ID
        /// </summary>
        public int ItemID { get; set; }


        /// <summary>
        /// 归属 GUID
        /// </summary>
        public Guid FromGUID { get; set; }


        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// 小标题
        /// </summary>
        public string SubTitle { get; set; }


        /// <summary>
        /// 样式
        /// </summary>
        public string Style { get; set; }


        /// <summary>
        /// 属性
        /// </summary>
        public string Flags { get; set; }


        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }


        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }


        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }


        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }


        /// <summary>
        /// 大小
        /// </summary>
        public int Size { get; set; }


        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }


        /// <summary>
        /// 扩展名
        /// </summary>
        public string Exp { get; set; }


        /// <summary>
        /// 简介
        /// </summary>
        public string Descriptions { get; set; }


        /// <summary>
        /// 排序
        /// </summary>
        public int Orders { get; set; }


        /// <summary>
        /// 查看
        /// </summary>
        public int Hits { get; set; }


        /// <summary>
        /// 标记 推荐
        /// </summary>
        public int MarkHot { get; set; }

        public string Contents { get; set; }


        #endregion

        #region 扩展字段

        /// <summary>
        /// 推荐标识
        /// </summary>
        public virtual string E_MarkHotTitle
        {
            get
            {
                if (MarkHot > 0)
                {
                    return string.Format("[<span style='color:#f00;'>置顶{0}</span>]", MarkHot);
                }
                else
                {
                    return "";
                }
            }
        }

        public int E_ClassID { get; set; }


        /// <summary>
        /// 属性标识
        /// </summary>
        public string E_FlagsTitle
        {
            get
            {
                var _flagsTitle = string.Empty;
                if (!string.IsNullOrEmpty(Flags))
                {
                    foreach (var flagsItem in Flags.TrimStart(',').Split(','))
                    {
                        var item = JieYiGuang.Model.Dictionary.IsFlags.Find(delegate(ListItem p) { return p.Value.ToString() == flagsItem; });
                        if (item != null)
                        {
                            _flagsTitle += string.Format(" <span style=\"color:{0}\">{1}</span> ", item.Color, item.Text);
                        }
                    }
                }
                return _flagsTitle;
            }

        }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string E_MarkStatusTitle
        {
            get
            {
                var item = JieYiGuang.Model.Dictionary.IsStatus.Find(delegate(ListItem p) { return int.Parse(p.Value.ToString()) == this.MarkStatus; });
                if (item == null) { item = new ListItem(); item.Color = ""; item.Text = "未知"; }
                return string.Format("<span style=\"color:{0}\">{1}</span>", item.Color, item.Text);
            }
        }
        public T_ClassNews ClassNews { get; set; }
        #endregion
    }
}