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
    public partial class T_News
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public T_News()
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
        /// 类型 ID
        /// </summary>
        public int TypeID { get; set; }


        /// <summary>
        /// 地区 ID
        /// </summary>
        public int AreaID { get; set; }


        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }


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
        /// 来源
        /// </summary>
        public string Source { get; set; }


        /// <summary>
        /// 编辑
        /// </summary>
        public string Author { get; set; }


        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }


        /// <summary>
        /// 属性
        /// </summary>
        public string Flags { get; set; }
        
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }


        /// <summary>
        /// 缩略图
        /// </summary>
        public string Images { get; set; }


        /// <summary>
        /// 图片集
        /// </summary>
        public string ArrayImages { get; set; }


        /// <summary>
        /// 文件集
        /// </summary>
        public string ArrayFiles { get; set; }


        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkUrl { get; set; }


        /// <summary>
        /// 简介
        /// </summary>
        public string Descriptions { get; set; }


        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }


        /// <summary>
        /// 时间 开始
        /// </summary>
        public DateTime? TimeBegin { get; set; }


        /// <summary>
        /// 时间 结束
        /// </summary>
        public DateTime? TimeEnd { get; set; }


        /// <summary>
        /// 模板
        /// </summary>
        public string HtmlTemplate { get; set; }


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


        /// <summary>
        /// 是否 评论
        /// </summary>
        public bool IsComment { get; set; }


        /// <summary>
        /// 数量 评论
        /// </summary>
        public int NumComment { get; set; }


        /// <summary>
        /// 数量 收藏
        /// </summary>
        public int NumFavorite { get; set; }


        /// <summary>
        /// 数量 赞
        /// </summary>
        public int NumGood { get; set; }


        /// <summary>
        /// 数量 项目
        /// </summary>
        public int NumItem { get; set; }

        /// <summary>
        /// 扩展分类
        /// </summary>
        public string FromClassID { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Lang { get; set; }
        /// <summary>
        /// 资源标题
        /// </summary>
        public string ItemTitle { get; set; }


        /// <summary>
        /// 对应资讯ID
        /// </summary>
        public int RefId { get; set; }


        #endregion

        #region 扩展字段
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

        /// <summary>
        /// 图集列表
        /// </summary>
        public List<T_NewsItem> ItemList { get; set; }
        public T_ClassNews ClassNews { get; set; }
        public string E_ClassTitle { get { return ClassNews == null ? "未设置" : ClassNews.Title; } }
        #endregion
    }


    /// <summary>
    /// 资讯类别表
    /// </summary>
    [Serializable]
    public partial class T_NewsEn
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public T_NewsEn()
        {
        }
        #region 属性

        /// <summary>
        /// SEO标题（长度250）
        /// </summary>
        public string SeoTitleEn { get; set; }
        /// <summary>
        /// SEO关键字（长度250）
        /// </summary>
        public string SeoKeywordsEn { get; set; }
        /// <summary>
        /// SEO描述（长度1000）
        /// </summary>
        public string SeoDescriptionEn { get; set; }


        /// <summary>
        /// 标题
        /// </summary>
        public string TitleEn { get; set; }


        /// <summary>
        /// 小标题
        /// </summary>
        public string SubTitleEn { get; set; }


        /// <summary>
        /// 缩略图
        /// </summary>
        public string ImagesEn { get; set; }


        /// <summary>
        /// 简介
        /// </summary>
        public string DescriptionsEn { get; set; }


        /// <summary>
        /// 内容
        /// </summary>
        public string ContentsEn { get; set; }


        #endregion
    }
}