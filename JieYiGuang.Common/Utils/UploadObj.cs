using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Drawing.Imaging;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace JieYiGuang.Common.Helper
{
    /// <summary>
    /// 上传类
    /// </summary>
    public class UploadObj
    {

        public UploadObj()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 允许文件上传的类型枚举
        /// </summary>
        public enum FileType
        {
            jpg, gif, bmp, png, jpeg
        }

        #region 取得文件后缀
        /// <summary>
        /// 取得文件后缀
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns></returns>
        public static string GetFileExtends(string filename)
        {
            string ext = null;
            if (filename.IndexOf('.') > 0)
            {
                string[] fs = filename.Split('.');
                ext = fs[fs.Length - 1];
            }
            return ext;
        }
        #endregion

        #region 取得上传文件的大小
        /// <summary>
        /// 取得上传文件的大小
        /// </summary>
        /// <param name="filename">对象</param>
        /// <returns></returns>
        public static int GetFileSize(FileUpload myFileUpload)
        {
            int size = 0;
            size = myFileUpload.PostedFile.ContentLength;
            return size;
        }
        #endregion

        #region 检测文件是否合法
        /// <summary>
        /// 检测上传文件是否合法
        /// </summary>
        /// <param name="fileExtends">文件后缀名</param>
        /// <returns></returns>
        public static bool CheckFileExtends(string fileExtends, string UpFileType)
        {
            bool status = false;
            string[] fe = new string[] { "" };
            fileExtends = fileExtends.ToLower();
            if (UpFileType == "" || UpFileType == null || UpFileType == string.Empty)
            {
                fe = Enum.GetNames(typeof(FileType));
            }
            else
            {
                fe = FunObject.StringSplit(UpFileType, "|");
            }
            for (int i = 0; i < fe.Length; i++)
            {
                if (fe[i].ToLower() == fileExtends)
                {
                    status = true;
                    break;
                }
            }
            return status;
        }
        #endregion

        #region 保存文件
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fpath">全路径,Server.MapPath()</param>
        /// <param name="myFileUpload">上传控件</param>
        /// <returns></returns>
        public static string FileSave(string fpath, FileUpload myFileUpload, string UpFileType, int UpFileSize)
        {
            string DenyFileType = "html|htm|php|php2|php3|php4|php5|phtml|pwml|inc|asp|aspx|ascx|jsp|cfm|cfc|pl|bat|exe|com|dll|vbs|js|reg|cgi|htaccess|asis|sh|shtml|shtm|phtm";
            string s = "";
            string fileExtends = "";
            int filesize = 0;
            string fileName = myFileUpload.FileName;
            if (fileName != "")
            {

                if (!System.IO.Directory.Exists(fpath))
                {
                    System.IO.Directory.CreateDirectory(fpath);
                }
                //取得文件后缀
                fileExtends = GetFileExtends(fileName);
                filesize = GetFileSize(myFileUpload);

                if (CheckFileExtends(fileExtends, DenyFileType))
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('对不起，你上传的文件类型不正确！');history.back();</script>");

                    System.Web.HttpContext.Current.Response.End();
                }

                if (!CheckFileExtends(fileExtends, UpFileType))
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('对不起，你上传的文件类型不正确，格式为：" + UpFileType + "');history.back();</script>");

                    System.Web.HttpContext.Current.Response.End();
                }
                if (filesize > UpFileSize * 1024)
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('对不起，你上传的文件超过了限制，限制大小为：" + UpFileSize + "KB.');history.back();</script>");
                    System.Web.HttpContext.Current.Response.End();
                }
                Random rd = new Random();
                s = RandomObject.DateRndName(rd) + "." + fileExtends;
                string file = fpath + "\\" + s;
                try
                {
                    myFileUpload.SaveAs(file);
                }
                catch (Exception ee)
                {
                    throw new Exception(ee.ToString());
                }
            }
            return s;
        }
        #endregion

        #region 缩略图
        //产生图片缩略图 fixType{0：不固定 1：固定宽度  2：固定高度}
        //当fixType!=0时，若固定高度，则width表示最大宽度值，若固定宽度，则height表示最大高度值，0表示不指定
        /// <summary>
        /// 产生图片缩略图
        /// </summary>
        /// <param name="fileName1">服务器的原图片物理地址</param>
        /// <param name="fileName2">要生成的服务器缩略图物理地址</param>
        /// <param name="width">当fixType!=0时，若固定高度，则width表示最大宽度值</param>
        /// <param name="height">当fixType!=0时，若固定宽度，则height表示最大高度值</param>
        /// <param name="fixType">fixType{0：不固定 1：固定宽度  2：固定高度}</param>
        public static void GetThumbnail(string fileName1, string fileName2, int width, int height, int fixType)
        {
            Hashtable htmimes = new Hashtable();
            htmimes[".gif"] = "image/gif";
            htmimes[".jpeg"] = "image/jpeg";
            htmimes[".jpg"] = "image/jpeg";
            htmimes[".png"] = "image/png";
            htmimes[".tif"] = "image/tiff";
            htmimes[".tiff"] = "image/tiff";
            htmimes[".bmp"] = "image/bmp";

            System.Drawing.Image image = System.Drawing.Image.FromFile(fileName1);

            int owidth = image.Size.Width;
            int oheight = image.Size.Height;

            switch (fixType)
            {
                case 0:
                    owidth = width;
                    oheight = height;
                    break;
                case 1:
                    if (owidth <= width)
                    {
                        //当缩略后的高度超过了最大高度时
                        if (height > 0 && oheight > height)
                        {
                            owidth = Convert.ToInt32((float)owidth * ((float)height / (float)oheight));
                            oheight = height;
                        }
                    }
                    else
                    {
                        oheight = Convert.ToInt32((float)oheight * ((float)width / (float)owidth));
                        owidth = width;
                        //当缩略后的高度超过了最大高度时
                        if (height > 0 && oheight > height)
                        {
                            owidth = Convert.ToInt32((float)owidth * ((float)height / (float)oheight));
                            oheight = height;
                        }
                    }
                    break;
                case 2:
                    if (oheight < height)
                    {
                        //当缩略后的宽度超过了最大宽度时
                        if (width > 0 && owidth > width)
                        {
                            oheight = Convert.ToInt32((float)oheight * ((float)width / (float)owidth));
                            owidth = width;
                        }
                    }
                    else
                    {

                        owidth = Convert.ToInt32((float)owidth * ((float)height / (float)oheight));
                        oheight = height;
                        //当缩略后的宽度超过了最大宽度时
                        if (width > 0 && owidth > width)
                        {
                            oheight = Convert.ToInt32((float)oheight * ((float)width / (float)owidth));
                            owidth = width;
                        }
                    }
                    break;
                default:
                    owidth = width;
                    oheight = height;
                    break;
            }

            string sExt = fileName1.Substring(fileName1.LastIndexOf(".")).ToLower();

            System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

            System.Drawing.Image myThumbnail = image.GetThumbnailImage(owidth, oheight, myCallback, IntPtr.Zero);

            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(fileName2))) System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fileName2));

            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

            ImageCodecInfo myImageCodecInfo = GetEncoderInfo((string)htmimes[sExt]);

            myThumbnail.Save(fileName2, myImageCodecInfo, myEncoderParameters);

            image.Dispose();
        }

        private static bool ThumbnailCallback()
        {
            return false;
        }

        /// <summary>
        /// 获取图像编码解码器的所有相关信息
        /// </summary>
        /// <param name="mimeType">包含编码解码器的多用途网际邮件扩充协议 (MIME) 类型的字符串</param>
        /// <returns>返回图像编码解码器的所有相关信息</returns>
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        /// <summary>
        /// 生成高质量的缩略图方法（等比例缩小）
        /// </summary>
        /// <param name="fileName">原图地址</param>
        /// <param name="newFile">新图保存的地址</param>
        /// <param name="maxHeight">新图的最大宽</param>
        /// <param name="maxWidth">新图的最大高</param>
        public static void GetThumbnail(string fileName, string newFile, int maxHeight, int maxWidth)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(fileName);
            System.Drawing.Imaging.ImageFormat
            thisFormat = img.RawFormat;
            Size newSize = getNewSize(maxWidth, maxHeight, img.Width, img.Height);
            Bitmap outBmp = new Bitmap(newSize.Width, newSize.Height);
            Graphics g = Graphics.FromImage(outBmp);
            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;//设置高质量插值法
            g.DrawImage(img, new Rectangle(0, 0, newSize.Width, newSize.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
            g.Dispose();

            // 以下代码为保存图片时,设置压缩质量
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象.
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = null;
            for (int x = 0; x < arrayICI.Length; x++)
            {
                if (arrayICI[x].FormatDescription.Equals("JPEG"))
                {
                    jpegICI = arrayICI[x];
                    //设置JPEG编码
                    break;
                }
            }
            if (jpegICI != null)
            {
                outBmp.Save(newFile, jpegICI, encoderParams);
            }
            else
            {
                outBmp.Save(newFile,
                thisFormat);
            }
            img.Dispose();
            outBmp.Dispose();
        }

        //自适应图片大小
        private static System.Drawing.Size getNewSize(int maxWidth, int maxHeight, int width, int height)
        {
            double w = 0.0;
            double h = 0.0;
            double sw = Convert.ToDouble(width);
            double sh = Convert.ToDouble(height);
            double mw = Convert.ToDouble(maxWidth);
            double mh = Convert.ToDouble(maxHeight);

            if (sw < mw && sh < mh)
            {
                w = sw;
                h = sh;
            }
            else if ((sw / sh) > (mw / mh))
            {
                w = maxWidth;
                h = (w * sh) / sw;
            }
            else
            {
                h = maxHeight;
                w = (h * sw) / sh;
            }
            return new Size(Convert.ToInt32(w), Convert.ToInt32(h));
        }

        #endregion


        /// <summary>
        /// 会产生graphics异常的PixelFormat
        /// </summary>
        private static PixelFormat[] indexedPixelFormats = { PixelFormat.Undefined, PixelFormat.DontCare,
PixelFormat.Format16bppArgb1555, PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed,
PixelFormat.Format8bppIndexed
    };

        /// <summary>
        /// 判断图片的PixelFormat 是否在 引发异常的 PixelFormat 之中
        /// </summary>
        /// <param name="imgPixelFormat">原图片的PixelFormat</param>
        /// <returns></returns>
        private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
        {
            foreach (PixelFormat pf in indexedPixelFormats)
            {
                if (pf.Equals(imgPixelFormat)) return true;
            }

            return false;
        }


        public static void CreageImageWater(System.Drawing.Image img, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency)
        {
            //获得水印图像
            //System.Drawing.Image markImg = System.Drawing.Image.FromFile(watermarkFilename);

            FileStream fs = new FileStream(watermarkFilename, FileMode.Open);
            System.Drawing.Image markImg = System.Drawing.Image.FromStream(fs);
            fs.Close();

            //创建颜色矩阵
            float[][] ptsArray ={ 
           new float[] {1, 0, 0, 0, 0},
           new float[] {0, 1, 0, 0, 0},
           new float[] {0, 0, 1, 0, 0},
           new float[] {0, 0, 0, 1, 0}, //注意：此处为0.0f为完全透明，1.0f为完全不透明
           new float[] {0, 0, 0, 0, 1}};
            ColorMatrix colorMatrix = new ColorMatrix(ptsArray);
            //新建一个Image属性
            ImageAttributes imageAttributes = new ImageAttributes();
            //将颜色矩阵添加到属性
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default,
             ColorAdjustType.Default);
            //生成位图作图区
            Bitmap newBitmap = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
            //设置分辨率
            newBitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);
            //创建Graphics
            Graphics g = Graphics.FromImage(newBitmap);
            //消除锯齿
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //拷贝原图到作图区
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);

            int xpos = 0;
            int ypos = 0;

            System.Drawing.Image watermark = new System.Drawing.Bitmap(watermarkFilename);
            switch (watermarkStatus)
            {
                case 1:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 2:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 3:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 4:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 5:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 6:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 7:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
                case 8:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
                case 9:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
            }


            //如果原图过小
            if (markImg.Width > img.Width || markImg.Height > img.Height)
            {
                System.Drawing.Image.GetThumbnailImageAbort callb = null;
                //对水印图片生成缩略图,缩小到原图得1/4
                System.Drawing.Image new_img = markImg.GetThumbnailImage(img.Width / 4, markImg.Height * img.Width / markImg.Width, callb, new System.IntPtr());
                //添加水印
                g.DrawImage(new_img, new Rectangle(xpos, ypos, new_img.Width, new_img.Height), 0, 0, new_img.Width, new_img.Height, GraphicsUnit.Pixel, imageAttributes);
                //释放缩略图
                new_img.Dispose();
                //释放Graphics
                //将生成得图片读入MemoryStream
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                newBitmap.Save(ms, ImageFormat.Jpeg);
                //返回新的Image对象
                img = System.Drawing.Image.FromStream(ms);
                img.Save(filename);

            }
            //原图足够大
            else
            {
                //添加水印
                g.DrawImage(markImg, new Rectangle(xpos, ypos, markImg.Width, markImg.Height), 0, 0, markImg.Width, markImg.Height, GraphicsUnit.Pixel, imageAttributes);
                //释放Graphics
                //将生成得图片读入MemoryStream
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                newBitmap.Save(ms, ImageFormat.Jpeg);
                //返回新的Image对象
                img = System.Drawing.Image.FromStream(ms);

                img.Save(filename);
            }
            g.Dispose();
            img.Dispose();
            newBitmap.Dispose();
            markImg.Dispose();
            watermark.Dispose();
            imageAttributes.Dispose();
        }



        /// <summary>
        /// 加图片的图片水印
        /// </summary>
        /// <param name="img">System.Drawing.Image img = System.Drawing.Image.FromStream(HttpContext.Current.Request.Files[0].InputStream);</param>
        /// <param name="filename">要生成的服务器水印图片物理地址</param>
        /// <param name="watermarkFilename">水印文件名在服务器上的物理地址</param>
        /// <param name="watermarkStatus">图片水印位置</param>
        public static void SaveImageSignPic(System.Drawing.Image img, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency)
        {
            int width = img.Width;

            int hight = img.Height * width / img.Width;
            System.Drawing.Bitmap bitmap = new Bitmap(width, hight);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //g.Clear(System.Drawing.Color.Transparent);
            g.DrawImage(img, 0, 0, width, hight);

            //水印图片
            Bitmap copyImage = (Bitmap)Bitmap.FromFile(watermarkFilename);
            //copyImage.MakeTransparent(Color.White);
            if (copyImage.Width > width || copyImage.Height > hight)
            {
                return;
            }
            g.DrawImage(copyImage, width - copyImage.Width, hight - copyImage.Height, copyImage.Width, copyImage.Height);
            try
            {
                bitmap.Save(filename);  //这个文件夹有权限
            }
            catch (Exception e)
            {
                throw e;
            }

            bitmap.Dispose();
            img.Dispose();
            g.Dispose();
            GC.Collect();
            return;


            //Graphics g = null;
            if (IsPixelFormatIndexed(img.PixelFormat))
            {
                Bitmap bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
                g = Graphics.FromImage(bmp);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.Clear(System.Drawing.Color.Transparent);
                g.DrawImage(img, 0, 0, img.Width, img.Height);
                //return;
            }
            else
            {
                g = Graphics.FromImage(img);
                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
            }

            System.Drawing.Image watermark = new System.Drawing.Bitmap(watermarkFilename);

            if (watermark.Height >= img.Height || watermark.Width >= img.Width)
            {
                return;
            }

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();

            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            //imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float transparency = 0.5F;
            if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
            {
                transparency = (watermarkTransparency / 10.0F);
            }

            float[][] colorMatrixElements = {
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
											};

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            int xpos = 0;
            int ypos = 0;

            switch (watermarkStatus)
            {
                case 1:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 2:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 3:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 4:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 5:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 6:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                    break;
                case 7:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
                case 8:
                    xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
                case 9:
                    xpos = (int)((img.Width * (float).99) - (watermark.Width));
                    ypos = (int)((img.Height * (float).99) - watermark.Height);
                    break;
            }

            //g.DrawImage(watermark, new System.Drawing.Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
            g.DrawImage(watermark, new System.Drawing.Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, System.Drawing.GraphicsUnit.Pixel, imageAttributes);

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType.IndexOf("jpeg") > -1)
                {
                    ici = codec;
                }
            }
            EncoderParameters encoderParams = new EncoderParameters();
            long[] qualityParam = new long[1];
            if (quality < 0 || quality > 100)
            {
                quality = 80;
            }
            qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
            encoderParams.Param[0] = encoderParam;

            //if (ici != null)
            //{
            //    img.Save(filename, ici, encoderParams);
            //}
            //else
            {
                img.Save(filename);
            }

            g.Dispose();
            img.Dispose();
            watermark.Dispose();
            imageAttributes.Dispose();
            GC.Collect();
        }

        /// <summary>
        /// 增加图片的文字水印
        /// </summary>
        /// <param name="img">System.Drawing.Image img = System.Drawing.Image.FromStream(HttpContext.Current.Request.Files[0].InputStream);</param>
        /// <param name="filename">要生成的服务器水印图片物理地址</param>
        /// <param name="watermarkText">水印文字</param>
        /// <param name="watermarkStatus">图片水印位置</param>
        public static void SaveImageSignText(System.Drawing.Image img, string filename, string watermarkText, int watermarkStatus, int quality, string fontname, int fontsize)
        {
            //System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            //	.FromFile(filename);
            Graphics g = Graphics.FromImage(img);
            Font drawFont = new Font(fontname, fontsize, FontStyle.Regular, GraphicsUnit.Pixel);
            SizeF crSize;
            crSize = g.MeasureString(watermarkText, drawFont);

            float xpos = 0;
            float ypos = 0;

            switch (watermarkStatus)
            {
                case 1:
                    xpos = (float)img.Width * (float).01;
                    ypos = (float)img.Height * (float).01;
                    break;
                case 2:
                    xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                    ypos = (float)img.Height * (float).01;
                    break;
                case 3:
                    xpos = ((float)img.Width * (float).99) - crSize.Width;
                    ypos = (float)img.Height * (float).01;
                    break;
                case 4:
                    xpos = (float)img.Width * (float).01;
                    ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 5:
                    xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                    ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 6:
                    xpos = ((float)img.Width * (float).99) - crSize.Width;
                    ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 7:
                    xpos = (float)img.Width * (float).01;
                    ypos = ((float)img.Height * (float).99) - crSize.Height;
                    break;
                case 8:
                    xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                    ypos = ((float)img.Height * (float).99) - crSize.Height;
                    break;
                case 9:
                    xpos = ((float)img.Width * (float).99) - crSize.Width;
                    ypos = ((float)img.Height * (float).99) - crSize.Height;
                    break;
            }

            //			System.Drawing.StringFormat StrFormat = new System.Drawing.StringFormat();
            //			StrFormat.Alignment = System.Drawing.StringAlignment.Center;
            //
            //			g.DrawString(watermarkText, drawFont, new System.Drawing.SolidBrush(System.Drawing.Color.White), xpos + 1, ypos + 1, StrFormat);
            //			g.DrawString(watermarkText, drawFont, new System.Drawing.SolidBrush(System.Drawing.Color.Black), xpos, ypos, StrFormat);
            //g.DrawString(watermarkText, drawFont, new SolidBrush(Color.White), xpos + 1, ypos + 1);
            g.DrawString(watermarkText, drawFont, new SolidBrush(Color.Black), xpos, ypos);

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType.IndexOf("jpeg") > -1)
                {
                    ici = codec;
                }
            }
            EncoderParameters encoderParams = new EncoderParameters();
            long[] qualityParam = new long[1];
            if (quality < 0 || quality > 100)
            {
                quality = 80;
            }
            qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
            encoderParams.Param[0] = encoderParam;

            if (ici != null)
            {
                //System.Web.HttpContext.Current.Response.Write(filename);
                //System.Web.HttpContext.Current.Response.End();
                img.Save(filename, ici, encoderParams);
            }
            else
            {
                img.Save(filename);
            }
            g.Dispose();
            //bmp.Dispose();
            img.Dispose();
        }

    }
}