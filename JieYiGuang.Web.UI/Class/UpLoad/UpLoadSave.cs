using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO;
using System.Web;
using JieYiGuang.Common;

namespace JieYiGuang.Web.UI.Class.UpLoad
{

    public class UpLoadSave
    {

        public static bool SaveFile(UploadModel FileModel, out string SaveFileUrl)
        {
            string NewFileUrl = string.Empty;
            string SmallFileUrl = string.Empty;
            SaveFileUrl = string.Empty; ;

            try
            {
                if (FileModel.Upload_FileData != null)
                {
                    //上传目录
                    int FileLength = FileModel.Upload_FileData.ContentLength;
                    string FileType = FileModel.Upload_FileData.ContentType;

                    string FilePath = FileHelper.GetDateOfFolder(FileModel.Upload_IsWeekFolder, FileModel.Upload_Folder);

                    string FileName = Path.GetFileName(FileModel.Upload_FileData.FileName);//获得文件名
                    string Name = Path.GetFileNameWithoutExtension(FileModel.Upload_FileData.FileName);//获得文件名
                    string Ext = Path.GetExtension(FileModel.Upload_FileData.FileName);//获得文件扩展名

                    //自动命名
                    if (FileModel.Upload_IsAutoName)
                    {
                        FileName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + Ext;
                    }
                    NewFileUrl = FilePath + FileName;
                    FileModel.Upload_FileData.SaveAs(HttpContext.Current.Request.MapPath(NewFileUrl));
                    SaveFileUrl = NewFileUrl;
                    //自动缩略图
                    if (FileModel.Upload_IsSmall)
                    {
                        SmallFileUrl = FileHelper.GetDateOfFolder(FileModel.Upload_IsWeekFolder, FileModel.Upload_Folder + "/S/") + FileName;
                        JieYiGuang.Common.ImageUtils.MakeThumbnail(HttpContext.Current.Request.MapPath(NewFileUrl), HttpContext.Current.Request.MapPath(SmallFileUrl), FileModel.Upload_Width, FileModel.Upload_Height, "CUTHW");
                        SaveFileUrl = SmallFileUrl;
                    }
                    //水印

                    SaveFileUrl = SaveFileUrl.Replace("~", "");
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                SaveFileUrl = "";
                return false;
                throw new ApplicationException(e.Message);
            }

        }
    }
}
