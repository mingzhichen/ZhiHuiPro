namespace JieYiGuang.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Admin 管理员 T_AdminUser object for EntityFramework mapped table 'T_AdminUser'.
    /// </summary>
    public partial class T_AdminUser
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public T_AdminUser()
        {
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
        }
        /// <summary>
        /// 缓存键
        /// </summary>
        public const string CacheKeyPrefix = "CacheTable_AdminUser";

        /// <summary>
        /// 表名
        /// </summary>
        public const string TableName = "T_AdminUser";

        #region 属性

        /// <summary>
        /// 管理员 ID
        /// </summary>
        [Key]
        [Display(Name = "管理员 ID")]
        public int ID { get; set; }


        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "用户名")]
        [StringLength(20)]
        public string UserName { get; set; }



        //*********************************************************

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "密码")]
        [StringLength(64)]
        public string AdminPassword { get; set; }


        /// <summary>
        /// 名字
        /// </summary>
        [Display(Name = "名字")]
        [StringLength(20)]
        public string Name { get; set; }


        /// <summary>
        /// 手机
        /// </summary>
        [Display(Name = "手机")]
        [StringLength(20)]
        public string Mobile { get; set; }


        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [StringLength(30)]
        public string Email { get; set; }


        /// <summary>
        /// 说明
        /// </summary>
        [Display(Name = "说明")]
        [StringLength(255)]
        public string Note { get; set; }

        /// <summary>
        /// 标识 状态
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "标识 状态")]
        public int MarkStatus { get; set; }


        /// <summary>
        /// 是否锁定
        /// </summary>
        [Display(Name = "是否锁定")]
        public int IsLock { get; set; }

        /// <summary>
        /// 信息 标记
        /// </summary>
        [Display(Name = "信息 标记")]
        [StringLength(50)]
        public string MarkName { get; set; }


        /// <summary>
        /// 创建 时间
        /// </summary>
        [Display(Name = "创建 时间")]
        public DateTime? CreateTime { get; set; }


        /// <summary>
        /// 更新 时间
        /// </summary>
        [Display(Name = "更新 时间")]
        public DateTime? UpdateTime { get; set; }

        #endregion
        
        /// <summary>
        /// 状态属性
        /// </summary>
        public static List<ListItem> IslockItem = new List<ListItem>() { 
            new ListItem() { Name = "NOLOCK", Value = "0", Text = "正常", Color = "#fe6306" ,IsShow=true  },
            new ListItem() { Name = "LOCK", Value = "1", Text = "锁定", Color = "#fe6306" ,IsShow=true  }
        };
    }
}