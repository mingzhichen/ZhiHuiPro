using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JieYiGuangCommon.Function
{
    /// <summary>
    /// 获取Controller.ModelState中的所有错误信息
    /// </summary>
    public static class ModelStateExtensions
    {
        //循环遍历
        public static string ExpendErrors(this System.Web.Mvc.Controller controller)
        {
            System.Text.StringBuilder sbErrors = new System.Text.StringBuilder();
            foreach (var item in controller.ModelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    for (int i = item.Errors.Count - 1; i >= 0; i--)
                    {
                        sbErrors.Append(item.Errors[i].ErrorMessage);
                        sbErrors.Append("\n\r");
                    }
                }
            }
            return sbErrors.ToString();
        }

        //Linq版,效率不如循环遍历，有待优化
        public static string ExpendErrors2(this System.Web.Mvc.Controller controller)
        {
            System.Text.StringBuilder sbError = new System.Text.StringBuilder();
            (from key in controller.ModelState.Keys
             where controller.ModelState[key].Errors.Count > 0
             select key)
             .Aggregate(sbError, (sb, key) =>
             {
                 return controller.ModelState[key].Errors
                     .Aggregate(sb, (sbChild, error) => sbChild.AppendFormat("{0} \n\r", error.ErrorMessage));
             });
            return sbError.ToString();
        }

        
    }

}
