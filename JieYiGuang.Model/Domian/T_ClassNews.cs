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
    public partial class T_ClassNews
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public T_ClassNews()
        {
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
            this.GUID = Guid.NewGuid();
        }
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
        /// 首页标识
        /// </summary>
        public byte MarkIndex { get; set; }
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

        #region 属性

        /// <summary>
        /// 类别名称
        /// </summary>
        public string ClassName { get; set; }


        /// <summary>
        /// 所属上级ID
        /// </summary>
        public int ParentID { get; set; }


        /// <summary>
        /// 所属上级
        /// </summary>
        public Guid? ParentGUID { get; set; }


        /// <summary>
        /// 父级路径
        /// </summary>
        public string ParentPath { get; set; }


        /// <summary>
        /// 排列顺序
        /// </summary>
        public int OrderNum { get; set; }


        /// <summary>
        /// 父 等级
        /// </summary>
        public int ParentLevels { get; set; }


        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// 小标题
        /// </summary>
        public string SubTitle { get; set; }


        /// <summary>
        /// 字母
        /// </summary>
        public string Letter { get; set; }


        /// <summary>
        /// 首字母
        /// </summary>
        public string Initial { get; set; }


        /// <summary>
        /// 属性
        /// </summary>
        public string Flags { get; set; }


        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }


        /// <summary>
        /// 样式
        /// </summary>
        public string Style { get; set; }


        /// <summary>
        /// 图片
        /// </summary>
        public string Images { get; set; }


        /// <summary>
        /// 注释
        /// </summary>
        public string Descriptions { get; set; }


        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }


        /// <summary>
        /// 路径
        /// </summary>
        public string Url { get; set; }


        /// <summary>
        /// 查看
        /// </summary>
        public int Hits { get; set; }


        /// <summary>
        /// 数量 项目
        /// </summary>
        public int NumCount { get; set; }


        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }


        /// <summary>
        /// 值 类型
        /// </summary>
        public string ValueType { get; set; }


        /// <summary>
        /// 语言
        /// </summary>
        public string Lang { get; set; }


        /// <summary>
        /// 标记 推荐
        /// </summary>
        public int MarkHot { get; set; }
        #endregion

        #region 扩展字段
        public IList children { get; set; }

        /// <summary>
        /// Tree状态
        /// </summary>
        public virtual string state
        {
            get
            {
                return (this.childcount == 0) ? "open" : "closed";
            }
        }

        /// <summary>
        /// Tree状态
        /// </summary>
        public virtual int _parentId
        {
            get
            {
                return this.ParentID;
            }
        }

        /// <summary>
        /// 子集数量
        /// </summary>
        public virtual int childcount
        {
            get
            {
                return (this.children == null) ? 0 : this.children.Count;
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

        #endregion
    }
}