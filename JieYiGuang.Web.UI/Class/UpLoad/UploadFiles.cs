using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO;
using System.Web;

namespace JieYiGuang.Web.UI.Class.UpLoad
{

    public class UploadFiles : System.Web.UI.Page
    {

        public static string UpLoadButton(string Name, UploadModel Model)
        {
            StringBuilder StrTemp = new StringBuilder();

            StringBuilder ScriptData = new StringBuilder();//参数化
            ScriptData.Append(string.Format("\n\t\t\t\t\t\t'Upload_OldFiles':$('#{0}').val(),", Name));
            ScriptData.Append(string.Format("\n\t\t\t\t\t\t'Upload_IsOldFilesDel':'{0}',", Model.Upload_IsOldFilesDel));
            ScriptData.Append(string.Format("\n\t\t\t\t\t\t'Upload_IsAutoName':'{0}',", Model.Upload_IsAutoName));
            ScriptData.Append(string.Format("\n\t\t\t\t\t\t'Upload_IsWeekFolder':'{0}',", Model.Upload_IsWeekFolder));
            ScriptData.Append(string.Format("\n\t\t\t\t\t\t'Upload_IsWaterMark':'{0}',", Model.Upload_IsWaterMark));
            ScriptData.Append(string.Format("\n\t\t\t\t\t\t'Upload_IsSmall':'{0}',", Model.Upload_IsSmall));
            ScriptData.Append(string.Format("\n\t\t\t\t\t\t'Upload_Folder':'{0}',", Model.Upload_Folder));
            if (Model.Upload_IsSmall)
            {
                ScriptData.Append(string.Format("'Upload_Width':'{0}',", Model.Upload_Width));
                ScriptData.Append(string.Format("'Upload_Height':'{0}',", Model.Upload_Height));
            }


            string UpLoadURL = "/Upload/UploadFiles.aspx";
            StrTemp.Append(string.Format("\n\t\t\t\t<input type=\"file\" id=\"FileUpload{0}\" name=\"FileUpload{0}\" class=\"button w150\" value='选择文件'>", Name));

            //使用 jquery.Uploadify 组件
            if (Model.Uploadify_Is)
            {

                if (!Model.Uploadify_IsAutoUpload)
                {
                    if (Model.Uploadify_FileNum > 1)
                    {
                        StrTemp.Append(string.Format("\n\t\t\t\t&nbsp;&nbsp;<a href=\"javascript:$('#FileUpload{0}').uploadifyUpload();\" class=\"button\">上传文件</a>", Name));
                        StrTemp.Append(string.Format("&nbsp;&nbsp;<a href=\"javascript:$('#FileUpload{0}').uploadifyClearQueue();\" class=\"button\">取消上传</a>", Name));
                    }
                }
                StrTemp.Append("\n\t\t\t\t" + "<script type=\"text/javascript\">");
                StrTemp.Append("\n\t\t\t\t" + "    $(document).ready(function () { ");
                StrTemp.Append("\n\t\t\t\t" + "        $('#FileUpload" + Name + "').uploadify({");
                StrTemp.Append("\n\t\t\t\t" + "            'uploader': '/App_Themes/Scripts/Jquery/plugin/jquery.uploadify/uploadify.swf',");
                StrTemp.Append("\n\t\t\t\t" + "            'auto': " + (Model.Uploadify_IsAutoUpload ? "true" : "false") + ",");//设置为true表示当选择一件附件后，自动开始上传。
                StrTemp.Append("\n\t\t\t\t" + "            'script': '" + UpLoadURL + "',");//指定服务器端上传处理文件
                StrTemp.Append("\n\t\t\t\t" + "            'folder': '" + (string.IsNullOrEmpty(Model.Upload_Folder) ? "/" : Model.Upload_Folder) + "',");//上传文件存放的目录
                StrTemp.Append("\n\t\t\t\t" + "            'buttonImg': '" + Model.Uploadify_ButtonImg + "',");
                StrTemp.Append("\n\t\t\t\t" + "            'width': '" + Model.Uploadify_ButtonImgWidth + "',");
                StrTemp.Append("\n\t\t\t\t" + "            'height': '" + Model.Uploadify_ButtonImgHeight + "',");
                StrTemp.Append("\n\t\t\t\t" + "            'cancelImg': '/App_Themes/Scripts/Jquery/plugin/jquery.uploadify/cancel.png',");
                StrTemp.Append("\n\t\t\t\t" + "            'fileExt' : '" + Model.Upload_FileExt + "',");//控制可上传文件的扩展名，启用本项时需同时声明fileDesc
                StrTemp.Append("\n\t\t\t\t" + "            'fileDesc':'" + Model.Upload_FileDesc + "',");//出现在上传对话框中的文件类型描述
                StrTemp.Append("\n\t\t\t\t" + "            'sizeLimit':1024 * 1024 * " + Model.Upload_FileSize + ",");//控制上传文件的大小，单位byte
                StrTemp.Append("\n\t\t\t\t" + "            'multi': " + (Model.Uploadify_FileNum > 1 ? "true" : "false") + ",");//是否允许同时上传多文件，默认false
                StrTemp.Append((string.IsNullOrEmpty(Model.Uploadify_QueueID) ? "" : string.Format("\n\t\t\t\t            'queueID': '{0}',", Model.Uploadify_QueueID)));//文件队列的ID，该ID与存放文件队列的div的ID一致
                StrTemp.Append("\n\t\t\t\t" + (Model.Uploadify_FileNum > 1 ? "            'Model.simUploadLimit':" + Model.Uploadify_FileNum + "," : ""));//多文件上传时，同时上传文件数目限制           
                StrTemp.Append("\n\t\t\t\t" + "            'scriptData':{" + ScriptData.ToString().Remove(ScriptData.ToString().Length - 1) + "\n\t\t\t\t            },");
                StrTemp.Append(Model.Uploadify_Function);
                StrTemp.Append("\n\t\t\t\t" + "            'onError' : function(event, queueID, fileObj,errorObj){alert(errorObj.type + 'Error:' + errorObj.info);},");
                StrTemp.Append("\n\t\t\t\t" + "            'onComplete': function (event, queueID, fileObj, response, data){");
                if (string.IsNullOrEmpty(Model.Upload_FunctionComplete))
                {
                    StrTemp.Append("" + "var FileJson = jQuery.parseJSON(response);");//返回值
                    StrTemp.Append("" + "if(FileJson) { $('#" + Name + "').val(FileJson.UploadUrl);}else { alert(\"Error！\");}");
                }
                else
                {
                    StrTemp.Append(Model.Upload_FunctionComplete);
                }
                StrTemp.Append("},");
                StrTemp.Append("\n\t\t\t\t" + "            'onAllComplete': function (event, data){");
                StrTemp.Append(Model.Upload_FunctiononAllComplete);
                StrTemp.Append("}");

                StrTemp.Append("\n\t\t\t\t" + "        }); \n\t\t\t\t    }); \n\t\t\t\t</script>\n");
            }
            return StrTemp.ToString();
        }
    }
}
