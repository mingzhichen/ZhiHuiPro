using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JieYiGuang.Model
{
    /// <summary>
    /// 字典
    /// </summary>
    public class Dictionary
    {
        /// <summary>
        /// 性别
        /// </summary>
        public static List<ListItem> Sex = new List<ListItem>() { 
            new ListItem() { Name = "Start", Value = "0", Text = "女", Color = "#8668ff"  ,IsShow=true }, 
            new ListItem() { Name = "Lock", Value = "1", Text = "男", Color = "#ff0000"  ,IsShow=true }
        };

        /// <summary>
        /// 省/市
        /// </summary>
        public static List<ListItem> Province = new List<ListItem>() { 
            new ListItem() { Name = "Province", Value = "0", Text = "省/市", Color = "#8668ff"  ,IsShow=true }
        };

        /// <summary>
        /// 市/地区
        /// </summary>
        public static List<ListItem> City = new List<ListItem>() { 
            new ListItem() { Name = "City", Value = "0", Text = "市/地区", Color = "#8668ff"  ,IsShow=true }
        };

        /// <summary>
        /// 区/县
        /// </summary>
        public static List<ListItem> Area = new List<ListItem>() { 
            new ListItem() { Name = "Area", Value = "0", Text = "区/县", Color = "#8668ff"  ,IsShow=true }
        };


        /// <summary>
        /// 设计收费
        /// </summary>
        public static List<ListItem> Price = new List<ListItem>() { 
            new ListItem() { Name = "Start", Value = "0", Text = "不限²", Color = "#8668ff"  ,IsShow=true }, 
            new ListItem() { Name = "Start", Value = "10", Text = "0-60元/m²", Color = "#8668ff"  ,IsShow=true }, 
            new ListItem() { Name = "Lock", Value = "20", Text = "60-100元/m²", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "Lock", Value = "30", Text = "100-150元/m²", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "Lock", Value = "40", Text = "150-200元/m²", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "Lock", Value = "50", Text = "300-600元/m²", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "Lock", Value = "60", Text = "600元/m²以上", Color = "#ff0000"  ,IsShow=true }
        };

        /// <summary>
        /// 专业类型
        /// </summary>
        public static List<ListItem> ProType = new List<ListItem>() { 
            new ListItem() { Name = "Start", Value = "0", Text = "室内设计²", Color = "#8668ff"  ,IsShow=true }, 
            new ListItem() { Name = "Start", Value = "1", Text = "软装设计", Color = "#8668ff"  ,IsShow=true }, 
            new ListItem() { Name = "Lock", Value = "2", Text = "建筑设计", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "Lock", Value = "3", Text = "园林设计", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "Lock", Value = "4", Text = "装修设计", Color = "#ff0000"  ,IsShow=true }
        };

        /// <summary>
        /// 状态属性
        /// </summary>
        public static List<ListItem> IsStatus = new List<ListItem>() { 
            new ListItem() { Name = "Started", Value = "0", Text = "正常", Color = "#8668ff"  ,IsShow=true }, 
            new ListItem() { Name = "Locking", Value = "10", Text = "锁定", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "Checking", Value = "20", Text = "待审核", Color = "#fe6306"  ,IsShow=true },
            new ListItem() { Name = "Overdued", Value = "30", Text = "过期", Color = "#1a8b00"  ,IsShow=true }, 
            new ListItem() { Name = "Stopped", Value = "40", Text = "停用", Color = ""  ,IsShow=true },
            new ListItem() { Name = "Delete", Value = "-1", Text = "删除", Color = ""  ,IsShow=true }
        };

        /// <summary>
        /// 项目属性
        /// </summary>
        public static List<ListItem> Type = new List<ListItem>() { 
            new ListItem() { Name = "Started", Value = "分享", Text = "分享", Color = "#8668ff"  ,IsShow=true },
            new ListItem() { Name = "Started", Value = "原创", Text = "原创", Color = "#8668ff"  ,IsShow=true }
        };

        /// <summary>
        /// 项目属性
        /// </summary>
        public static List<ListItem> Item = new List<ListItem>() { 
            new ListItem() { Name = "0", Value = "0", Text = " ", Color = "#8668ff"  ,IsShow=true },
            new ListItem() { Name = "10", Value = "10", Text = "头条", Color = "#8668ff" ,IsShow=false }, 
            new ListItem() { Name = "20", Value = "20", Text = "热点", Color = "#ff0000" ,IsShow=true },
            new ListItem() { Name = "30", Value = "30", Text = "经验", Color = "#fe6306" ,IsShow=true },
            new ListItem() { Name = "40", Value = "40", Text = "视频", Color = "#1a8b00" ,IsShow=true },
            new ListItem() { Name = "50", Value = "50", Text = "案例", Color = "#1a8b00" ,IsShow=true },
            new ListItem() { Name = "60", Value = "60", Text = "活动", Color = "#1a8b00" ,IsShow=true }
        };

        /// <summary>
        /// 状态属性
        /// </summary>
        public static List<ListItem> IsCheckStatus = new List<ListItem>() { 
            new ListItem() { Name = "Checking", Value = "0", Text = "待审核", Color = "#fe6306" ,IsShow=true  },
            new ListItem() { Name = "Started", Value = "10", Text = "正常", Color = "#fe6306" ,IsShow=true  }, 
            new ListItem() { Name = "Delete", Value = "-1", Text = "删除", Color = "" ,IsShow=true  }
        };

        /// <summary>
        /// 状态属性
        /// </summary>
        public static List<ListItem> IsFlags = new List<ListItem>() { 
            new ListItem() { Name = "H", Value = "H", Text = "首页[H]", Color = "#8668ff" ,IsShow=true  },
            new ListItem() { Name = "I", Value = "I", Text = "频道[I]", Color = "#8668ff" ,IsShow=true  },
            new ListItem() { Name = "C", Value = "C", Text = "推荐[C]", Color = "#ff0000" ,IsShow=true  }, 
            new ListItem() { Name = "F", Value = "F", Text = "幻灯[F]", Color = "#fe6306" ,IsShow=true  }, 
            new ListItem() { Name = "A", Value = "A", Text = "特荐[A]", Color = "#1a8b00" ,IsShow=true  }, 
            new ListItem() { Name = "S", Value = "S", Text = "滚动[S]", Color = "" ,IsShow=true  },
            new ListItem() { Name = "P", Value = "P", Text = "图片[P]", Color = "" ,IsShow=true  }
        };

        /// <summary>
        /// 招标状态
        /// </summary>
        public static List<ListItem> ActivityStatus = new List<ListItem>() { 
            new ListItem() { Name = "", Value = "0", Text = "待审核", Color = "#8668ff" ,IsShow=true  }, 
            new ListItem() { Name = "", Value = "1", Text = "未处理", Color = "#ff0000" ,IsShow=true  },
            new ListItem() { Name = "", Value = "2", Text = "未接通", Color = "#fe6306" ,IsShow=true  },
            new ListItem() { Name = "", Value = "3", Text = "未开通", Color = "#8668ff" ,IsShow=true  }, 
            new ListItem() { Name = "", Value = "4", Text = "推送", Color = "#ff0000" ,IsShow=true  },
            new ListItem() { Name = "", Value = "5", Text = "无效", Color = "#fe6306" ,IsShow=true  },
            new ListItem() { Name = "", Value = "6", Text = "待定", Color = "#8668ff" ,IsShow=true  }, 
            new ListItem() { Name = "", Value = "7", Text = "有效", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "", Value = "8", Text = "已审核", Color = "#fe6306"  ,IsShow=true },
            new ListItem() { Name = "", Value = "-1", Text = "已删除", Color = ""  ,IsShow=true }
        };
        /// <summary>
        /// 订单状态
        /// </summary>
        public static List<ListItem> OrderStatus = new List<ListItem>() { 
            new ListItem() { Name = "", Value = "10", Text = "生成订单", Color = "#8668ff"  ,IsShow=true }, 
            new ListItem() { Name = "", Value = "20", Text = "分派企业", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "", Value = "23", Text = "企业确认", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "", Value = "26", Text = "分派设计师", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "", Value = "30", Text = "量房报价", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "", Value = "31", Text = "业主确认", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "", Value = "40", Text = "签合同", Color = "#fe6306"  ,IsShow=true },
            new ListItem() { Name = "", Value = "50", Text = "设计装修", Color = "#8668ff"  ,IsShow=true }, 
            new ListItem() { Name = "", Value = "63", Text = "装修完成", Color = "#fe6306"  ,IsShow=true },
            new ListItem() { Name = "", Value = "100", Text = "验收通过", Color = "#fe6306"  ,IsShow=true },
            new ListItem() { Name = "", Value = "-1", Text = "订单删除", Color = ""  ,IsShow=true }
        };
        /// <summary>
        /// 星级
        /// </summary>
        public static List<ListItem> StarLevel = new List<ListItem>() { 
            new ListItem() { Name = "", Value = "1", Text = "★", Color = "#8668ff"  ,IsShow=true }, 
            new ListItem() { Name = "", Value = "2", Text = "★★", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "", Value = "3", Text = "★★★", Color = "#fe6306"  ,IsShow=true },
            new ListItem() { Name = "", Value = "4", Text = "★★★★", Color = "#8668ff"  ,IsShow=true }, 
            new ListItem() { Name = "", Value = "5", Text = "★★★★★", Color = "#fe6306"  ,IsShow=true }
        };
        /// <summary>
        /// 等级编号
        /// </summary>
        public static List<ListItem> Grade = new List<ListItem>() { 
            new ListItem() { Name = "", Value = "0", Text = "全部", IsShow=true }, 
            new ListItem() { Name = "", Value = "1", Text = "A级", IsShow=true },
            new ListItem() { Name = "", Value = "2", Text = "B级", IsShow=true},
            new ListItem() { Name = "", Value = "3", Text = "C级", IsShow=true}, 
            new ListItem() { Name = "", Value = "4", Text = "D级"  ,IsShow=true }
        };
        /// <summary>
        /// 派单状态
        /// </summary>
        public static List<ListItem> ServiceStatus = new List<ListItem>() { 
            new ListItem() { Name = "", Value = "0", Text = "全部"  ,IsShow=true }, 
            new ListItem() { Name = "", Value = "1", Text = "派单"  ,IsShow=true },
            new ListItem() { Name = "", Value = "2", Text = "不派单",IsShow=true}
        };
        /// <summary>
        /// 认证状态
        /// </summary>
        public static List<ListItem> IsCertification = new List<ListItem>() { 
            new ListItem() { Name = "", Value = "0", Text = "全部"  ,IsShow=true }, 
            new ListItem() { Name = "", Value = "1", Text = "未认证",IsShow=true},
            new ListItem() { Name = "", Value = "2", Text = "已提交材料",IsShow=true },
            new ListItem() { Name = "", Value = "3", Text = "已认证",IsShow=true}
        };

        /// <summary>
        /// 投诉状态属性
        /// </summary>
        public static List<ListItem> ComplaintStatus = new List<ListItem>() { 
            new ListItem() { Name = "", Value = "0", Text = "未受理", Color = "#8668ff"  ,IsShow=true }, 
            new ListItem() { Name = "", Value = "10", Text = "已受理", Color = "#ff0000"  ,IsShow=true },
            new ListItem() { Name = "", Value = "20", Text = "已完成", Color = "#fe6306"  ,IsShow=true },
            new ListItem() { Name = "", Value = "-1", Text = "不受理", Color = ""  ,IsShow=true }
        };

    }
}
