<%@ WebHandler Language="C#" Class="Upload" %>

/**
 * KindEditor ASP.NET
 *
 * 本ASP.NET程序是演示程序，建议不要直接在实际项目中使用。
 * 如果您确定直接使用本程序，使用之前请仔细确认相关安全设置。
 *
 */

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;

public class Upload : IHttpHandler
{
    private HttpContext context;

    public void ProcessRequest(HttpContext context)
    {
        //文件保存目录路径
        var rootPath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"];
        var thisPath = context.Request["Path"];
        //文件保存目录URL
        String saveUrl = rootPath.Replace("~", "");

        //定义允许上传的文件扩展名
        Hashtable extTable = new Hashtable();
        extTable.Add("image", System.Configuration.ConfigurationManager.AppSettings["Image"].DefaultIsNullOrEmpty("gif,jpg,jpeg,png,bmp"));//"gif,jpg,jpeg,png,bmp"
        extTable.Add("flash", System.Configuration.ConfigurationManager.AppSettings["Flash"].DefaultIsNullOrEmpty("swf,flv"));//"swf,flv"
        extTable.Add("media", System.Configuration.ConfigurationManager.AppSettings["Media"].DefaultIsNullOrEmpty("swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb"));//"swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb"
        extTable.Add("file", System.Configuration.ConfigurationManager.AppSettings["File"].DefaultIsNullOrEmpty("doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2,pdf")); //"doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2,pdf"

        //最大文件大小
        int maxSize = 8000000;
        this.context = context;

        HttpPostedFile imgFile = context.Request.Files["imgFile"];
        if (imgFile == null)
        {
            showError("请选择文件。");
        }

        String dirPath = context.Server.MapPath(rootPath);
        if (!Directory.Exists(dirPath))
        {
            showError("上传目录不存在。");
        }

        String dirName = context.Request.QueryString["dir"];
        if (String.IsNullOrEmpty(dirName))
        {
            dirName = "image";
        }
        if (!extTable.ContainsKey(dirName))
        {
            showError("目录名不正确。");
        }

        String fileName = imgFile.FileName;
        String fileExt = Path.GetExtension(fileName).ToLower();

        if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
        {
            showError("上传文件大小超过限制。");
        }

        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
        {
            showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
        }

        System.Drawing.Image imgT = System.Drawing.Image.FromStream(imgFile.InputStream);
        if (IsCMYK(imgT))
        {
            showError("抱歉，上传失败，你上传的图片为CMYK印刷格式的图片，请上传" + ((String)extTable[dirName]) + "格式的图片。");
            return;
        }

        //创建文件夹
        dirPath += dirName + "/";
        saveUrl += dirName + "/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        String ymd = DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MM-dd");
        dirPath += ymd + "/";
        saveUrl += ymd + "/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        //生成文件名
        String newFileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
        String filePath = dirPath + newFileName;

        imgFile.SaveAs(filePath);

        String fileUrl = saveUrl + newFileName;

        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        hash["url"] = fileUrl;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }

    private void showError(string message)
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 1;
        hash["message"] = message;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }

    #region 验证图片CMYK模式
    /// <summary>
    /// 验证图片CMYK模式
    /// </summary>
    /// <param name="img"></param>
    /// <returns></returns>
    public static bool IsCMYK(System.Drawing.Image img)
    {
        bool isCmyk;

        if ((GetImageFlags(img).IndexOf("Ycck ") > -1) || (GetImageFlags(img).IndexOf("Cmyk ") > -1))
        { isCmyk = true; }
        else
        { isCmyk = false; }

        return isCmyk;
    }
    public static string GetImageFlags(System.Drawing.Image img)
    {
        System.Drawing.Imaging.ImageFlags FlagVals = (System.Drawing.Imaging.ImageFlags)Enum.Parse(typeof(System.Drawing.Imaging.ImageFlags), img.Flags.ToString());
        return FlagVals.ToString();
    }
    #endregion
}
