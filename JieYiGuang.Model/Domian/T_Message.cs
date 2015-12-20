namespace JieYiGuang.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Collections;
using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 资讯类别表
    /// </summary>
    [Serializable]
    public partial class T_Message
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public T_Message()
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
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 尊称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [Required(ErrorMessage = "手机不能为空")]
        [StringLength(14, ErrorMessage = "手机不能超过14位")]
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage = "邮箱不能为空")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$", ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Required(ErrorMessage = "地址不能为空")]
        public string Address { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FromBrower { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 企业或者机构名 
        /// </summary>
        [Required(ErrorMessage = "企业或者机构名不能为空")]
        public string Company { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        [Required(ErrorMessage = "省不能为空")]
        public string Province { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        [Required(ErrorMessage = "邮编不能为空")]
        public string PostCode { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [Required(ErrorMessage = "城市不能为空")]
        public string City { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        public string WebSite { get; set; }
        /// <summary>
        /// FromIP
        /// </summary>
        public string FromIP { get; set; }

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

    }
}