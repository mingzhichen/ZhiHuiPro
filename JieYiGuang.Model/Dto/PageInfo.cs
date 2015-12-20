using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JieYiGuang.DTO
{
    /// <summary>
    /// 分页模型
    /// </summary>
    public class PagerInfo
    {
        public PagerInfo()
        {
            CurrentPageIndex = 1;
            PageSize = 10;
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        private int _RowCount;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RowCount
        {
            set
            {
                this._RowCount = value;
                
                if (this.PageSize == 0)
                {
                    this.PageSize = 10;
                }

                this.PageCount = this._RowCount % this.PageSize == 0 ? this._RowCount / this.PageSize : this._RowCount / this.PageSize + 1;
                
                if (this.CurrentPageIndex <= 0)
                {
                    this.CurrentPageIndex = 1;
                }
                else if (this.CurrentPageIndex > this.PageCount)
                {
                    this.CurrentPageIndex = this.PageCount;
                }
            }
            get
            {
                return _RowCount;
            }
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPageIndex { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
    }
}