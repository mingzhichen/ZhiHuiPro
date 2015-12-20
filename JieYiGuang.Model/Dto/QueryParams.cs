using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JieYiGuang.Dto
{
    /// <summary>
    /// 查询基类
    /// </summary>
    public class QueryParams
    {
        public QueryParams()
        {
            SearchMarkStatus = 9999;
            SearchTimeBegin = "";
            SearchTimeEnd = "";
        }
        public int SearchClassID { get; set; }
        public int SearchAreaID { get; set; }
        public string SearchName { get; set; }
        public string SearchKey { get; set; }
        public Guid SearchGUID { get; set; }

        public Guid SearchFromGUID { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string[] SearchFlags { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string[] SearchType { get; set; }

        /// <summary>
        /// TAG
        /// </summary>
        public int[] SearchTags { get; set; }
        public string SearchMarkName { get; set; }
        public int SearchMarkStatus { get; set; }
        public string SearchTimeBegin { get; set; }
        public string SearchTimeEnd { get; set; }
        public int[] SearchID { get; set; }
        public int SearchTypeID { get; set; }
        public int SearchUserID { get; set; }
        public int SearchIsReply { get; set; }

        /// <summary>
        /// 用户 GUID
        /// </summary>
        public Guid SearchUserGUID { get; set; }

        /// <summary>
        /// 用户 GUID
        /// </summary>
        public Guid SearchAdminGUID { get; set; }

        /// <summary>
        /// 管理员
        /// </summary>
        public string SearchAdminName { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SearchOrderSort { get; set; }

        /// <summary>
        /// 排序规则
        /// </summary>
        public bool SearchOrder { get; set; }

        /// <summary>
        /// 搜索关联下级类别
        /// </summary>
        public string SearchClassIDStr { get; set; }

        /// <summary>
        /// 搜索推荐状态
        /// </summary>
        public int SearchRecommendID { get; set; }

        /// <summary>
        /// 认证状态
        /// </summary>
        public int SearchIsCertification { get; set; }

        /// <summary>
        /// 等级编号
        /// </summary>
        public int SearchGrade { get; set; }

        /// <summary>
        /// 派单状态
        /// </summary>
        public string SearchServiceStatus { get; set; }
        /// <summary>
        /// 服务城市
        /// </summary>
        public string SearchServiceArea { get; set; }

        /// <summary>
        /// 地理纬度
        /// </summary>
        public double SearchGisLat { get; set; }
        /// <summary>
        /// 地理经度
        /// </summary>
        public double SearchGisLng { get; set; }
        /// <summary>
        /// 距离
        /// </summary>
        public int SearchDistance { get; set; }
        /// <summary>
        /// 是否英文
        /// </summary>
        public bool? IsEn { get; set; }
    }
}
