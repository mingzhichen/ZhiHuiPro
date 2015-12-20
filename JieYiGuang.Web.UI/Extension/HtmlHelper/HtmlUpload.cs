using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using JieYiGuang.Web.UI.Class.UpLoad;

namespace System.Web.Mvc
{
    public static class HtmlUpload
    {


        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="NameID">关联Input控件ID号</param>
        /// <param name="IsAutoName">是否自动命名</param>
        /// <param name="CompleteFunction">上传成功回调方法内容</param>
        /// <returns></returns>

        public static string UploadDialog(this HtmlHelper helper, int type, string objId, string title, string folder, UploadModel upModel)
        {
            var url = new StringBuilder();
            var StrTemp = new StringBuilder();
            switch (type)
            {
                case 0:
                    url.Append(string.Format("?Upload_FileDesc={0}", upModel.Upload_FileDesc));
                    url.Append(string.Format("&Upload_FileExt={0}", upModel.Upload_FileExt));
                    url.Append(string.Format("&Upload_FileSize={0}", upModel.Upload_FileSize));
                    url.Append(string.Format("&Upload_Folder={0}", upModel.Upload_Folder));
                    url.Append(string.Format("&Upload_IsAutoName={0}", upModel.Upload_IsAutoName));
                    url.Append(string.Format("&Upload_IsOldFilesDel={0}", upModel.Upload_IsOldFilesDel));
                    url.Append(string.Format("&Upload_IsSmall={0}", upModel.Upload_IsSmall));
                    url.Append(string.Format("&Upload_Width={0}", upModel.Upload_Width));
                    url.Append(string.Format("&Upload_Height={0}", upModel.Upload_Height));

                    StrTemp.Append("<a href=\"javascript:;\" class=\"easyui-linkbutton\" onclick=\"upLoadFile()\">" + title + "</a>");
                    StrTemp.Append("<a href=\"javascript:;\" class=\"easyui-linkbutton\" onclick=\"FilePreview('" + objId + "');\">预览</a>");
                    StrTemp.Append("\n\t\t\t\t<script language=\"javascript\" type=\"text/javascript\">");
                    StrTemp.Append("\n\t\t\t\t    var dialogWin;");
                    StrTemp.Append("\n\t\t\t\t    //初始化");
                    StrTemp.Append("\n\t\t\t\t    $(function () {  ");
                    StrTemp.Append("\n\t\t\t\t        dialogWin=$(\"#File-window\").window({ width:600, height:400, modal:true });");
                    StrTemp.Append("\n\t\t\t\t        closeUpload();");
                    StrTemp.Append("\n\t\t\t\t     });");
                    StrTemp.Append("\n\t\t\t\t    //上传赋值");
                    StrTemp.Append("\n\t\t\t\t    function setUpload(value) {");
                    StrTemp.Append("\n\t\t\t\t        $('#" + objId + "').val(value);");
                    StrTemp.Append("\n\t\t\t\t        $('#" + objId + "_Show').attr('src',value);");
                    StrTemp.Append("\n\t\t\t\t        closeUpload();");
                    StrTemp.Append("\n\t\t\t\t    }");
                    StrTemp.Append("\n\t\t\t\t    //关闭编辑窗口");
                    StrTemp.Append("\n\t\t\t\t    function closeUpload(){");
                    StrTemp.Append("\n\t\t\t\t        dialogWin.dialog('close');");
                    StrTemp.Append("\n\t\t\t\t    }");
                    StrTemp.Append("\n\t\t\t\t    //添加文件");
                    StrTemp.Append("\n\t\t\t\t    function upLoadFile(){");
                    StrTemp.Append("\n\t\t\t\t        dialogWin = openWindow(\"#File-window\", \"文件上传\", \"/Upload/UploadFiles.html" + url.ToString() + "\", \"icon-upload\").dialog(\"open\");");
                    StrTemp.Append("\n\t\t\t\t        return false;");
                    StrTemp.Append("\n\t\t\t\t    }");
                    StrTemp.Append("\n\t\t\t</script>");
                    StrTemp.Append("\n\t\t\t<div id=\"File-window\" style=\"padding: 5px;\"></div>");
                    break;
                case 1:
                    //StrTemp.Append("<div style=\"max-width: 100px; max-height: 100px; display: inline-block; border: 1px solid #efefef; padding: 2px;\"><img src=\"\" class=\"screenshot\" border=0 onload=\"alert('$('#" + objId + "').val()'); $(this).attr('src',$('#" + objId + "').val());AutoSize(this,100,100)\" align=\"middle\" style=\"vertical-align: middle;\"></div>");
                    StrTemp.Append("<a href=\"javascript:;\" class=\"easyui-linkbutton\" onclick=\"KindUploadManager('" + objId + "','" + upModel.Upload_Folder + "');\">" + title + "</a>");
                    StrTemp.Append("<a href=\"javascript:;\" class=\"easyui-linkbutton\" onclick=\"FilePreview('" + objId + "');\">预览</a>");

                    StrTemp.Append("\n\t\t\t\t<script language=\"javascript\" type=\"text/javascript\">");
                    StrTemp.Append("\n\t\t\t function KindUploadManager(id, type) {");
                    StrTemp.Append("\n\t\t\t    editor.loadPlugin('dialogupload', function () {");
                    StrTemp.Append("\n\t\t\t        editor.plugin.dialogupload({");
                    StrTemp.Append("\n\t\t\t            fileUrl: $('#' + id).val(),");
                    StrTemp.Append("\n\t\t\t            fileType: type,");
                    StrTemp.Append("\n\t\t\t            clickFn: function (url, title) {");
                    StrTemp.Append("\n\t\t\t                $('#' + id).val(url);");
                    StrTemp.Append("\n\t\t\t                $('#" + objId + "_Show').attr('src',url);");
                    StrTemp.Append("\n\t\t\t                editor.hideDialog();");
                    StrTemp.Append("\n\t\t\t            }");
                    StrTemp.Append("\n\t\t\t        });");
                    StrTemp.Append("\n\t\t\t    });");
                    StrTemp.Append("\n\t\t\t}");
                    StrTemp.Append("\n\t\t\t</script>");
                    break;
                case 3:
                    StrTemp.Append("\n\t\t\t\t<script language=\"javascript\" type=\"text/javascript\">");
                    StrTemp.Append("\n\t\t\t $(function () { ");
                    StrTemp.Append("\n\t\t\t    $('#" + objId + "').JSAjaxFileUploader({");
                    StrTemp.Append("\n\t\t\t        uploadUrl: '/Upload/UploadFile.html?Path=" + upModel.Upload_Folder + "',");
                    StrTemp.Append("\n\t\t\t            inputText: '选择上传文件',");
                    StrTemp.Append("\n\t\t\t            //fileName: 'photo',");
                    StrTemp.Append("\n\t\t\t            maxFileSize: 10 * 1024 * 1024,");
                    StrTemp.Append("\n\t\t\t            allowExt: 'gif|jpg|jpeg|png',");
                    StrTemp.Append("\n\t\t\t            zoomPreview: false,");
                    StrTemp.Append("\n\t\t\t            zoomWidth: 360,");
                    StrTemp.Append("\n\t\t\t            zoomHeight: 360,");
                    StrTemp.Append("\n\t\t\t            success: function (data) {");
                    StrTemp.Append("\n\t\t\t            });");
                    StrTemp.Append("\n\t\t\t       },");
                    StrTemp.Append("\n\t\t\t       error: function (data) {");
                    StrTemp.Append("\n\t\t\t            alert(data.msg);");
                    StrTemp.Append("\n\t\t\t       }");
                    StrTemp.Append("\n\t\t\t    });");
                    StrTemp.Append("\n\t\t\t });");
                    StrTemp.Append("\n\t\t\t</script>");
                    break;

            }
            return StrTemp.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0 [SWFUpload] 1 KindEditor</param>
        /// <param name="objId">对象ID</param>
        /// <param name="title"></param>
        /// <param name="folder"></param>
        /// <param name="upModel"></param>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        public static string UploadDialog(this HtmlHelper helper, int type, string objId, string title, string folder, UploadModel upModel, string buttonName)
        {
            var url = new StringBuilder();
            var StrTemp = new StringBuilder();
            if (type == 0)
            {
                url.Append(string.Format("?Upload_FileDesc={0}", upModel.Upload_FileDesc));
                url.Append(string.Format("&Upload_FileExt={0}", upModel.Upload_FileExt));
                url.Append(string.Format("&Upload_FileSize={0}", upModel.Upload_FileSize));
                url.Append(string.Format("&Upload_Folder={0}", upModel.Upload_Folder));
                url.Append(string.Format("&Upload_IsAutoName={0}", upModel.Upload_IsAutoName));
                url.Append(string.Format("&Upload_IsOldFilesDel={0}", upModel.Upload_IsOldFilesDel));
                url.Append(string.Format("&Upload_IsSmall={0}", upModel.Upload_IsSmall));
                url.Append(string.Format("&Upload_Width={0}", upModel.Upload_Width));
                url.Append(string.Format("&Upload_Height={0}", upModel.Upload_Height));

                StrTemp.Append("<a href=\"javascript:;\" class=\"easyui-linkbutton\" onclick=\"upLoadFile()\">" + title + "</a>");
                StrTemp.Append("<a href=\"javascript:;\" class=\"easyui-linkbutton\" onclick=\"FilePreview('" + objId + "');\">预览</a>");
                StrTemp.Append("\n\t\t\t\t<script language=\"javascript\" type=\"text/javascript\">");
                StrTemp.Append("\n\t\t\t\t    var dialogWin;");
                StrTemp.Append("\n\t\t\t\t    //初始化");
                StrTemp.Append("\n\t\t\t\t    $(function () {  ");
                StrTemp.Append("\n\t\t\t\t        dialogWin=$(\"#File-window\").window({ width:600, height:400, modal:true });");
                StrTemp.Append("\n\t\t\t\t        closeUpload();");
                StrTemp.Append("\n\t\t\t\t     });");
                StrTemp.Append("\n\t\t\t\t    //上传赋值");
                StrTemp.Append("\n\t\t\t\t    function setUpload(value) {");
                StrTemp.Append("\n\t\t\t\t        $('#" + objId + "').val(value);");
                StrTemp.Append("\n\t\t\t\t        $('#" + objId + "_img').attr('src',value);");
                StrTemp.Append("\n\t\t\t\t        closeUpload();");
                StrTemp.Append("\n\t\t\t\t    }");
                StrTemp.Append("\n\t\t\t\t    //关闭编辑窗口");
                StrTemp.Append("\n\t\t\t\t    function closeUpload(){");
                StrTemp.Append("\n\t\t\t\t        dialogWin.dialog('close');");
                StrTemp.Append("\n\t\t\t\t    }");
                StrTemp.Append("\n\t\t\t\t    //添加文件");
                StrTemp.Append("\n\t\t\t\t    function upLoadFile(){");
                StrTemp.Append("\n\t\t\t\t        dialogWin = openWindow(\"#File-window\", \"文件上传\", \"/Upload/UploadFiles.html" + url.ToString() + "\", \"icon-upload\").dialog(\"open\");");
                StrTemp.Append("\n\t\t\t\t        return false;");
                StrTemp.Append("\n\t\t\t\t    }");
                StrTemp.Append("\n\t\t\t</script>");
                StrTemp.Append("\n\t\t\t<div id=\"File-window\" style=\"padding: 5px;\"></div>");

            }
            else
            {
                StrTemp.Append("<script type='text/javascript'>");
                StrTemp.Append("KindEditor.ready(function (K) {");
                StrTemp.Append("var defineUrl = '';");
                StrTemp.Append("var SmallImg = '';");
                StrTemp.Append("if ($('#defineUrl').length > 0) {");
                StrTemp.Append("defineUrl = $('#defineUrl').val();");
                StrTemp.Append(" }");
                StrTemp.Append(" if ($('#SmallImg').length > 0) {");
                StrTemp.Append("SmallImg = $('#SmallImg').val();");
                StrTemp.Append(" }");
                StrTemp.Append("var " + buttonName + " = K.uploadbutton({");
                StrTemp.Append(" button: K('#" + buttonName + "')[0],");
                StrTemp.Append("fieldName: 'imgFile',");
                StrTemp.Append(" url: '/Scripts/Editor/KindEditor/net/upload_json.ashx?dir=image&defineUrl=' + defineUrl + '&SmallImg=' + SmallImg,");
                StrTemp.Append(" afterUpload: function (data) {");
                StrTemp.Append("var id = '" + objId + "';");
                StrTemp.Append("if (data.error === 0) {");
                StrTemp.Append(" var url = K.formatUrl(data.url, 'absolute');");
                StrTemp.Append("  K('#url').val(url);");
                StrTemp.Append("$('#' + id).val(url);");
                StrTemp.Append("if ($('#ShowImg_' + id).length > 0) {");
                StrTemp.Append("$('#ShowImg_' + id).attr('src', url);");
                StrTemp.Append("}");
                StrTemp.Append("} else {");
                StrTemp.Append(" alert(data.message);");
                StrTemp.Append(" }");
                StrTemp.Append("  },");
                StrTemp.Append(" afterError: function (str) {");
                StrTemp.Append(" alert('自定义错误信息: ' + str);");
                StrTemp.Append(" }");
                StrTemp.Append("  });");
                StrTemp.Append(buttonName + ".fileBox.change(function (e) {");
                StrTemp.Append(buttonName + ".submit();");
                StrTemp.Append(" });");
                StrTemp.Append(" });");
                StrTemp.Append("</script>");

                //StrTemp.Append("<a href=\"javascript:;\" class=\"easyui-linkbutton\" onclick=\"KindUploadManager('" + objId + "','" + upModel.Upload_Folder + "');\">" + title + "</a>");

                //StrTemp.Append("<input class='ke-input-text' type='text' id='").Append(objId).Append("' value='' readonly='readonly' />");
                StrTemp.Append("<input type='button' id='").Append(buttonName).Append("' value='图片上传' />");
                StrTemp.Append("<a href=\"javascript:;\" class=\"easyui-linkbutton\" onclick=\"FilePreview('" + objId + "');\">预览</a>");
            }
            return StrTemp.ToString();
        }
        #region 图片上传
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="Name">绑定 input ID</param>
        /// <param name="IsAutoName">是否自动命名</param>
        /// <param name="FilePath">保存路径</param>
        /// <returns></returns>
        public static string TextBoxUpLoadImage(this HtmlHelper helper, string Name, bool IsAutoName, string FilePath)
        {
            //初始化
            var StrTemp = new StringBuilder();
            var UpModel = UploadModel.UpdateFileType(UploadType.Image);

            UpModel.Upload_IsAutoName = IsAutoName;
            if (!string.IsNullOrEmpty(FilePath)) { UpModel.Upload_Folder = FilePath; }
            StrTemp.Append(UploadFiles.UpLoadButton(Name, UpModel));
            return StrTemp.ToString();
        }

        /// <summary>
        /// 图片上传 缩略图
        /// </summary>
        /// <param name="Name">绑定 input ID</param>
        /// <param name="IsAutoName">是否自动命名</param>
        /// <param name="FilePath">保存路径</param>
        /// <param name="Width">缩略图 宽</param>
        /// <param name="Height">缩略图 高</param>
        /// <returns></returns>
        public static string TextBoxUpLoadImage(this HtmlHelper helper, string Name, bool IsAutoName, string FilePath, int Width, int Height)
        {
            //初始化
            var StrTemp = new StringBuilder();
            var UpModel = UploadModel.UpdateFileType(UploadType.Image);

            UpModel.Upload_IsAutoName = IsAutoName;
            if (!string.IsNullOrEmpty(FilePath)) { UpModel.Upload_Folder = FilePath; }
            UpModel.Upload_IsSmall = true;
            UpModel.Upload_Width = Width;
            UpModel.Upload_Height = Height;
            StrTemp.Append(UploadFiles.UpLoadButton(Name, UpModel));
            return StrTemp.ToString();
        }

        /// <summary>
        /// 图片上传 缩略图 显示图片
        /// </summary>
        /// <param name="Name">绑定 input ID</param>
        /// /// <param name="Name">图片地址</param>
        /// <param name="IsAutoName">是否自动命名</param>
        /// <param name="FilePath">保存路径</param>
        /// <param name="Width">缩略图 宽</param>
        /// <param name="Height">缩略图 高</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        //public static string TextBoxUpLoadImage(this HtmlHelper helper, string Name, string Value, string DefaultValue, bool IsAutoName, string FilePath, int Width, int Height, object htmlAttributes)
        //{
        //    StringBuilder StrTemp = new StringBuilder();
        //    TagBuilder tagBuilder = new TagBuilder("input") { };
        //    tagBuilder.MergeAttribute("type", "hidden");
        //    tagBuilder.MergeAttribute("id", Name);
        //    tagBuilder.MergeAttribute("name", Name);
        //    tagBuilder.MergeAttribute("value", Value);

        //    TagBuilder imgBuilder = new TagBuilder("img") { };
        //    imgBuilder.MergeAttribute("id", "ImgUrl" + Name);
        //    imgBuilder.MergeAttribute("src", string.IsNullOrEmpty(Value) ? DefaultValue : Value);
        //    imgBuilder.MergeAttribute("width", Width.ToString());
        //    imgBuilder.MergeAttribute("height", Height.ToString());
        //    imgBuilder.MergeAttribute("class", "UpLoadImage");
        //    imgBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

        //    StrTemp.Append(imgBuilder.ToString(TagRenderMode.Normal));
        //    StrTemp.Append(tagBuilder.ToString(TagRenderMode.Normal));

        //    //初始化
        //    var UpModel = UploadModel.UpdateFileType(UploadType.Image);
        //    UpModel.Upload_IsAutoName = IsAutoName;
        //    if (!string.IsNullOrEmpty(FilePath)) { UpModel.Upload_Folder = FilePath; }
        //    UpModel.Upload_IsSmall = true;
        //    UpModel.Upload_Width = Width;
        //    UpModel.Upload_Height = Height;
        //    UpModel.Upload_FunctionComplete = "var FileJson = jQuery.parseJSON(response);if(FileJson) { $('#" + Name + "').val(FileJson.UploadUrl);$('#ImgUrl" + Name + "').attr('src',FileJson.UploadUrl);}else { alert('Error！');}";
        //    StrTemp.Append(UploadFiles.UpLoadButton(Name, UpModel));
        //    return StrTemp.ToString();
        //}


        /// <summary>
        /// 图片上传 缩略图
        /// </summary>
        /// <param name="Name">绑定 input ID</param>
        /// <param name="IsAutoName">是否自动命名</param>
        /// <param name="FilePath">保存路径</param>
        /// <param name="Width">缩略图 宽</param>
        /// <param name="Height">缩略图 高</param>
        /// <returns></returns>
        public static string TextBoxUpLoadImage(this HtmlHelper helper, string Name, UploadModel upModel)
        {
            //初始化
            var StrTemp = new StringBuilder();
            var UpModel = upModel;
            StrTemp.Append(UploadFiles.UpLoadButton(Name, UpModel));
            return StrTemp.ToString();
        }

        #endregion

        /// <summary>
        /// 视频上传
        /// </summary>
        /// <param name="NameID">关联Input控件ID号</param>
        /// <param name="IsAutoName">是否自动命名</param>
        /// <param name="CompleteFunction">上传成功回调方法内容</param>
        /// <returns></returns>
        public static string TextBoxUpLoadVideo(this HtmlHelper helper, string NameID, bool IsAutoName, string FilePath)
        {
            var StrTemp = new StringBuilder();
            var UpModel = UploadModel.UpdateFileType(UploadType.Media);

            UpModel.Upload_IsAutoName = IsAutoName;
            if (!string.IsNullOrEmpty(FilePath)) { UpModel.Upload_Folder = FilePath; }
            StrTemp.Append(UploadFiles.UpLoadButton(NameID, UpModel));
            return StrTemp.ToString();
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="NameID">关联Input控件ID号</param>
        /// <param name="IsAutoName">是否自动命名</param>
        /// <param name="CompleteFunction">上传成功回调方法内容</param>
        /// <returns></returns>
        public static string TextBoxUpLoadFile(this HtmlHelper helper, string NameID, bool IsAutoName, string FilePath)
        {
            var StrTemp = new StringBuilder();
            var UpModel = UploadModel.UpdateFileType(UploadType.File);

            UpModel.Upload_IsAutoName = IsAutoName;
            if (!string.IsNullOrEmpty(FilePath)) { UpModel.Upload_Folder = FilePath; }
            StrTemp.Append(UploadFiles.UpLoadButton(NameID, UpModel));
            return StrTemp.ToString();
        }

        //public static string FlashUpLoad();

    }
}
