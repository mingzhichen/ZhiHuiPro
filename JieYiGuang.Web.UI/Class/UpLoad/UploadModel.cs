using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace JieYiGuang.Web.UI.Class.UpLoad
{
    /// <summary>
    /// 上传文件类型 参数化
    /// </summary>
    public enum UploadType
    {
        Image = 1,
        Media = 2,
        File = 3,
    }

    //上传参数
    public class UploadModel
    {

        /// <summary>
        /// 上传文件类型
        /// </summary>
        public UploadType? Upload_FileType { get; set; }

        /// <summary>
        /// 是否自动命名 例：true
        /// </summary>
        public bool Upload_IsAutoName { get; set; }

        /// <summary>
        /// 文件保存路径 例: /Images/
        /// </summary>
        public string Upload_Folder { get; set; }

        /// <summary>
        /// 是否日期路径
        /// </summary>
        public bool Upload_IsWeekFolder { get; set; }

        /// <summary>
        /// 数据流
        /// </summary>
        public HttpPostedFileBase Upload_FileData { get; set; }

        /// <summary>
        /// 是否缩略图
        /// </summary>
        public bool Upload_IsSmall { get; set; }

        /// <summary>
        /// 是否水印
        /// </summary>
        public bool Upload_IsWaterMark { get; set; }

        /// <summary>
        /// 缩略图宽度
        /// </summary>
        public int Upload_Width { get; set; }

        /// <summary>
        /// 缩略图高度
        /// </summary>
        public int Upload_Height { get; set; }

        /// <summary>
        /// 旧文件名
        /// </summary>
        public string Upload_OldFiles { get; set; }

        /// <summary>
        /// 删除旧文件
        /// </summary>
        public bool Upload_IsOldFilesDel { get; set; }

        /// <summary>
        /// 上传文件大小 MB 例：4
        /// </summary>
        public int Upload_FileSize { get; set; }

        /// <summary>
        /// 上传文件类型 例: *.jpg;*.gif;*.png;*.bmp
        /// </summary>
        public string Upload_FileExt { get; set; }

        /// <summary>
        /// 上传文件说明 例: Images Files(*.jpg;*.gif;*.png;*.bmp)
        /// </summary>
        public string Upload_FileDesc { get; set; }

        /// <summary>
        /// 单上传完成调用方法 
        /// </summary>
        public string Upload_FunctionComplete { get; set; }

        /// <summary>
        /// 全部上传完成调用方法 
        /// </summary>
        public string Upload_FunctiononAllComplete { get; set; }

        /// <summary>
        /// 是否使用组件上传 例：true
        /// </summary>
        public bool Uploadify_Is { get; set; }

        /// <summary>
        /// 是否自动开始上传 例：true
        /// </summary>
        public bool Uploadify_IsAutoUpload { get; set; }

        /// <summary>
        /// 上传按钮图标
        /// </summary>
        public string Uploadify_ButtonImg { get; set; }

        /// <summary>
        /// 上传按钮宽度
        /// </summary>
        public int Uploadify_ButtonImgWidth { get; set; }

        /// <summary>
        /// 上传按钮高度
        /// </summary>
        public int Uploadify_ButtonImgHeight { get; set; }

        /// <summary>
        /// 同时上传文件数目  例：2
        /// </summary>
        public int Uploadify_FileNum { get; set; }

        /// <summary>
        /// 文件队列的ID，该ID与存放文件队列的div的ID一致
        /// </summary>
        public string Uploadify_QueueID { get; set; }

        /// <summary>
        /// 其他自定义方法 
        /// </summary>
        public string Uploadify_Function { get; set; }

        /// <summary>
        /// 初始化上传控件
        /// </summary>
        /// <param name="uploadType"></param>
        public UploadModel()
        {

            this.Upload_IsAutoName = false;

            this.Upload_IsWeekFolder = false;
            this.Upload_FileType = UploadType.Image;
            this.Upload_IsOldFilesDel = false;
            this.Uploadify_Is = true;
            this.Uploadify_IsAutoUpload = true;
            this.Uploadify_ButtonImg = "/App_Themes/Scripts/Jquery/plugin/jquery.uploadify/selectfile.png";
            this.Uploadify_ButtonImgWidth = 100;
            this.Uploadify_ButtonImgHeight = 24;
            this.Uploadify_FileNum = 1;

        }

        public static UploadModel UpdateFileType(UploadType? Type)
        {
            var model = new UploadModel();
            //文件类型初始化
            if (model.Upload_FileType != null)
            {
                switch (Type)
                {
                    case UploadType.Image:
                        //图片上传
                        model.Uploadify_ButtonImg = "/App_Themes/Skins/Default/Images/btn/btnUpLoadImages.png";
                        model.Upload_FileExt = "*.jpg;*.gif;*.png;*.bmp;*.jpeg";
                        model.Upload_FileDesc = "Image Files(" + model.Upload_FileExt + ")";
                        model.Upload_Folder = "image";
                        model.Upload_FileSize = 2;
                        break;
                    case UploadType.Media:
                        //视频上传
                        model.Uploadify_ButtonImg = "/App_Themes/Skins/Default/Images/btn/btnSelectFile.jpg";
                        model.Upload_FileExt = "*.mp3;*.mp4;*.wav;*.wmv;*.swf;*.flv";
                        model.Upload_FileDesc = "Media Files(" + model.Upload_FileExt + ")";
                        model.Upload_Folder = "media";
                        model.Upload_FileSize = 50;
                        break;
                    case UploadType.File:
                        //文件上传
                        model.Uploadify_ButtonImg = "/App_Themes/Skins/Default/Images/btn/btnSelectFile.jpg";
                        model.Upload_FileExt = "*.doc;*.zip;*.xls;*.ppt;*.pdf;*.rar;*.flv;*.swf";
                        model.Upload_FileDesc = "File Files(" + model.Upload_FileExt + ")";
                        model.Upload_Folder = "file";
                        model.Upload_FileSize = 100;
                        break;
                }
                model.Uploadify_IsAutoUpload = true;
            }
            return model;
        }

    }


}
