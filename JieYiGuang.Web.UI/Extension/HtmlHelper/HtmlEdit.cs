using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace System.Web.Mvc
{
    //编辑器调用
    public static class HtmlWebEdit
    {

        public static string Load_WebEditor { get; set; }
        public static string Set_WebEditor { get; set; }


        /// <summary>
        /// 编辑器调用
        /// </summary>
        public static MvcHtmlString WebEditorEn<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string ToolbarSet, int width, int height, string propertyName, string propertyModel)
        {
            var editor = "KindEditor";
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<textarea name=\"{0}\" id=\"{0}\" style=\"width:95%;height:{3}px;\">{1}</textarea>", propertyName, propertyModel, width, height));
            switch (editor)
            {
                case "FCKeditor":
                    sb.Append(string.Format("<script type=\"text/javascript\">"));
                    sb.Append(string.Format(" var oFCKeditor = new FCKeditor(\"{0}\");", propertyName));
                    sb.Append(string.Format(" oFCKeditor.BasePath =\"{0}\";", "/Scripts/Editor/fckeditor/"));
                    sb.Append(string.Format(" oFCKeditor.ToolbarSet=\"{0}\";", ToolbarSet));
                    sb.Append(string.Format(" oFCKeditor.Width	= {0} ;", width));
                    sb.Append(string.Format(" oFCKeditor.Height	= {0} ;", height));
                    sb.Append(string.Format(" oFCKeditor.ReplaceTextarea();"));
                    sb.Append(string.Format("</script>"));
                    sb.Append("<div class=\"WebEditorInput\"><input type=\"button\" name=\"button\" value=\"自动排版\" onclick=\"formatFckContent('Contents');\" /></div>");
                    break;
                case "KindEditor":
                    StringBuilder setWebEditor = new StringBuilder();
                    setWebEditor.Append("\n\t    if(editor_" + propertyName + "!=null){CheckRemoteFile(\"" + propertyName + "\",$(\"#IsLoadImg\").attr(\"checked\"));editor_" + propertyName + ".sync();}");
                    Set_WebEditor = Set_WebEditor + setWebEditor.ToString();
                    StringBuilder loadWebEditor = new StringBuilder();
                    loadWebEditor.Append("\n    var editor_" + propertyName + ";");
                    loadWebEditor.Append("\n    KindEditor.ready(function(K) {");
                    loadWebEditor.Append("\n         if(editor_" + propertyName + " ==null){");
                    loadWebEditor.Append("\n             editor_" + propertyName + " = K.create('textarea[name=\"" + propertyName + "\"]', {");
                    loadWebEditor.Append("\n                 afterChange: function () {");
                    loadWebEditor.Append("\n                   $(\"#word_count_" + propertyName + "\").html(\"您当前输入了<span class='red'>\" + this.count() + \"</span>个字符，<span class='red'>\" + this.count('text') + \"</span>个文字。<span id='PicConfirm_" + propertyName + "'></span>\");");
                    loadWebEditor.Append("\n                 },");
                    loadWebEditor.Append("\n                 uploadJson: '/App_Scripts/Editor/KindEditor/net/upload_json.ashx',");
                    loadWebEditor.Append("\n                 allowFileManager : true,");
                    loadWebEditor.Append("\n                 filterMode : false,");
                    loadWebEditor.Append("\n                 newlineTag: 'p'");
                    loadWebEditor.Append("\n             });");
                    loadWebEditor.Append("\n         }");
                    loadWebEditor.Append("\n     });");
                    Load_WebEditor = Load_WebEditor + loadWebEditor.ToString();
                    sb.Append("<div class=\"WebEditorInput\">");
                    sb.Append("<input type=\"button\" name=\"button\" value=\"自动排版\" onclick=\"formatContent('" + propertyName + "');\" />");
                    sb.Append(string.Format(" <span id=\"word_count_{0}\"></span>", propertyName));
                    sb.Append("</div>");
                    break;
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// 编辑器调用
        /// </summary>
        public static MvcHtmlString WebEditor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string ToolbarSet, int width, int height)
        {
            var editor = "KindEditor";
            var propertyName = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).PropertyName;
            var propertyModel = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<textarea name=\"{0}\" id=\"{0}\" style=\"width:95%;height:{3}px;\">{1}</textarea>", propertyName, propertyModel, width, height));
            switch (editor)
            {
                case "FCKeditor":
                    sb.Append(string.Format("<script type=\"text/javascript\">"));
                    sb.Append(string.Format(" var oFCKeditor = new FCKeditor(\"{0}\");", propertyName));
                    sb.Append(string.Format(" oFCKeditor.BasePath =\"{0}\";", "/Scripts/Editor/fckeditor/"));
                    sb.Append(string.Format(" oFCKeditor.ToolbarSet=\"{0}\";", ToolbarSet));
                    sb.Append(string.Format(" oFCKeditor.Width	= {0} ;", width));
                    sb.Append(string.Format(" oFCKeditor.Height	= {0} ;", height));
                    sb.Append(string.Format(" oFCKeditor.ReplaceTextarea();"));
                    sb.Append(string.Format("</script>"));
                    sb.Append("<div class=\"WebEditorInput\"><input type=\"button\" name=\"button\" value=\"自动排版\" onclick=\"formatFckContent('Contents');\" /></div>");
                    break;
                case "KindEditor":
                    StringBuilder setWebEditor = new StringBuilder();
                    setWebEditor.Append("\n\t    if(editor_" + propertyName + "!=null){CheckRemoteFile(\"" + propertyName + "\",$(\"#IsLoadImg\").attr(\"checked\"));editor_" + propertyName + ".sync();}");
                    Set_WebEditor = Set_WebEditor + setWebEditor.ToString();
                    StringBuilder loadWebEditor = new StringBuilder();
                    loadWebEditor.Append("\n    var editor_" + propertyName + ";");
                    loadWebEditor.Append("\n    KindEditor.ready(function(K) {");
                    loadWebEditor.Append("\n         if(editor_" + propertyName + " ==null){");
                    loadWebEditor.Append("\n             editor_" + propertyName + " = K.create('textarea[name=\"" + propertyName + "\"]', {");
                    loadWebEditor.Append("\n                 afterChange: function () {");
                    loadWebEditor.Append("\n                   $(\"#word_count_" + propertyName + "\").html(\"您当前输入了<span class='red'>\" + this.count() + \"</span>个字符，<span class='red'>\" + this.count('text') + \"</span>个文字。<span id='PicConfirm_" + propertyName + "'></span>\");");
                    loadWebEditor.Append("\n                 },");
                    loadWebEditor.Append("\n                 uploadJson: '/App_Scripts/Editor/KindEditor/net/upload_json.ashx',");
                    loadWebEditor.Append("\n                 allowFileManager : true,");
                    loadWebEditor.Append("\n                 filterMode : false,");
                    loadWebEditor.Append("\n                 newlineTag: 'p'");
                    loadWebEditor.Append("\n             });");
                    loadWebEditor.Append("\n         }");
                    loadWebEditor.Append("\n     });");
                    Load_WebEditor = Load_WebEditor + loadWebEditor.ToString();
                    sb.Append("<div class=\"WebEditorInput\">");
                    sb.Append("<input type=\"button\" name=\"button\" value=\"自动排版\" onclick=\"formatContent('" + propertyName + "');\" />");
                    sb.Append(string.Format(" <span id=\"word_count_{0}\"></span>", propertyName));
                    sb.Append("</div>");
                    break;
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString SetWebEditor(this HtmlHelper helper)
        {
            var text = Set_WebEditor;
            Set_WebEditor = "";
            return MvcHtmlString.Create(text);
        }

        public static MvcHtmlString LoadWebEditor(this HtmlHelper helper)
        {
            var text = Load_WebEditor;
            Load_WebEditor = "";
            return MvcHtmlString.Create(text);
        }
    }
}
