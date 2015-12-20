<%@ WebHandler Language="C#" Class="uploadpic" %>

using System;
using System.Web;
using System.IO;
using System.Net;
using System.Globalization;
using LitJson;

public class uploadpic : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string returnVal = string.Empty;
        context.Response.ContentType = "text/plain";
        string piclist = context.Request["pic"];
        string uploadUrl = context.Request["uploadUrl"];
        if (string.IsNullOrEmpty(uploadUrl))
        {
            returnVal = "{success:false,Msg:'上传路径不明确！'}";
        }
        else
        {
            if (string.IsNullOrEmpty(piclist))
                returnVal = "{success:false,Msg:'没有远程图片需要上传！'}";
            else
            {
                string[] arr = piclist.Split('|');//分隔条件
                int suc = arr.Length;
                string folder = "/" + uploadUrl + "/" + DateTime.Now.ToString("yyyy")+"/"+DateTime.Now.ToString("MM-dd");
                if (!Directory.Exists(context.Server.MapPath(folder)))
                    Directory.CreateDirectory(context.Server.MapPath(folder));

                string sstr = "";
                for (int i = 0; i < suc; i++)
                {
                    try
                    {
                        string fileName = DownLoadFile(arr[i], folder, context);
                        if (string.IsNullOrEmpty(fileName))
                        {
                            sstr += ",false";
                        }
                        else
                        {
                            sstr += ",'" + folder + "/" + fileName + "'";
                        }
                    }
                    catch
                    {
                        sstr += ",false";
                    }
                }

                returnVal = "{success:true,items:[" + sstr.Trim(',') + "]}";
            }
        }
        context.Response.Write(returnVal); //返回值
    }

    /// 
    /// 通过mime类型获取图片后缀
    /// 
    /// mime类型
    /// 图片后缀
    private string GetExt(string MIME)
    {
        switch (MIME.ToLower().Trim())
        {
            case "image/gif": return "gif";
            case "image/jpeg": return "jpg";
            case "image/png": return "png";
            case "application/x-ms-bmp":
            case "image/nbmp": return "bmp";
            default: return null;
        }
    }

    /// <summary>
    /// 将下载的文件流写入硬盘
    /// </summary>
    /// <param name="FullPath">本地文件的路径</param>
    /// <param name="ns">文件流</param>
    private void WriteToHDD(string FullPath, Stream ns)
    {
        FileStream writer = new FileStream(FullPath, FileMode.OpenOrCreate, FileAccess.Write);
        int bufferSize = 512, readSize = 0;
        byte[] buffer = new byte[bufferSize];
        readSize = ns.Read(buffer, 0, bufferSize);
        while (readSize > 0)
        {
            writer.Write(buffer, 0, readSize);
            readSize = ns.Read(buffer, 0, bufferSize);
        }
        writer.Flush();
        writer.Close();
    }

    /// <summary>
    /// 下载网络图片
    /// </summary>
    /// <param name="url">图片网络地址</param>
    /// <param name="folder">保存到本地图片的时间文件夹</param>
    /// <param name="context">请求对象</param>
    /// <returns>如果是图片则下载并返回生成的文件名，否则返回null</returns>
    private string DownLoadFile(string url, string folder, HttpContext context)
    {
        string fileName = null;
        try
        {
            System.Threading.Thread.Sleep(500);//线程延时
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            string ext = GetExt(response.ContentType)/*根据响应头获取后缀*/;
            if (ext != null)//是图片文件
            {
                fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + "." + ext;
                Stream ns = response.GetResponseStream();
                WriteToHDD(context.Server.MapPath(folder + "/" + fileName), ns);//注意修改保存图片的路径
                ns.Close();
            }
            response.Close();
        }
        catch (Exception ex)
        {
            fileName = null;
        }
        return fileName;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}