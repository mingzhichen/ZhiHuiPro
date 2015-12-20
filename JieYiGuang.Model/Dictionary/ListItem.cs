using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace JieYiGuang.Model
{

    public class ListItem
    {
        public ListItem() { }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 规定天数
        /// </summary>
        public int Day { get; set; }
    }

    public class SelectItem
    {
        /// <summary>
        /// 返回DTO对象给实体
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public static IList<SelectListItem> GetSelectListItem(List<ListItem> itemList)
        {
            IList<SelectListItem> listItem = new List<SelectListItem>();
            foreach (var item in itemList)
            {
                if (item.IsShow == true)
                {
                    listItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
                }
            }
            return listItem;
        }
    }

}
