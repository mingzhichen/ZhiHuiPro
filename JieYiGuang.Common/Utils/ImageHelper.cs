using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.IO;
using ImageResizer;

namespace JieYiGuang.Common
{
    /// <summary>
    /// 缩略图类
    /// </summary
    public static class ImageUtils
    {
        #region 缩略图
        /// <summary>
        /// 缩略图，按高度和宽度来缩略
        /// </summary>
        /// <param name="image"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Image Scale(Image image, Size size)
        {
            return image.GetThumbnailImage(size.Width, size.Height, null, new IntPtr());
        }
        /// <summary>
        /// 缩略图，按倍数来缩略
        /// </summary>
        /// <param name="image">原图</param>
        /// <param name="multiple">放大或缩小的倍数，负数表示缩小，正数表示放大</param>
        /// <returns></returns>
        public static Image Scale(Image image, Int32 multiple)
        {
            Int32 newWidth;
            Int32 newHeight;
            Int32 absMultiple = Math.Abs(multiple);
            if (multiple == 0)
            {
                return image.Clone() as Image;
            }
            if (multiple < 0)
            {
                newWidth = image.Width / absMultiple;
                newHeight = image.Height / absMultiple;
            }
            else
            {
                newWidth = image.Width * absMultiple;
                newHeight = image.Height * absMultiple;
            }
            return image.GetThumbnailImage(newWidth, newHeight, null, new IntPtr());
        }
        /// <summary>
        /// 固定宽度缩略        
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static Image ScaleFixWidth(Image image, Int32 width)
        {
            Int32 newWidth = width;
            Int32 newHeight;
            Double tempMultiple = (Double)newWidth / (Double)image.Width;
            newHeight = (Int32)(((Double)image.Height) * tempMultiple);
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics newGp = Graphics.FromImage(newImage))
            {
                newGp.CompositingQuality = CompositingQuality.HighQuality;
                //设置高质量插值法
                newGp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //设置高质量,低速度呈现平滑程度
                newGp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //清空画布并以透明背景色填充
                newGp.Clear(Color.Transparent);
                newGp.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight));
            }
            return newImage;
        }
        /// <summary>
        /// 固定高度缩略        
        /// </summary>
        /// <param name="image"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image ScaleFixHeight(Image image, Int32 height)
        {
            Int32 newWidth;
            Int32 newHeight = height;

            Double tempMultiple = (Double)newHeight / (Double)image.Height;
            newWidth = (Int32)(((Double)image.Width) * tempMultiple);
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics newGp = Graphics.FromImage(newImage))
            {
                newGp.CompositingQuality = CompositingQuality.HighQuality;
                //设置高质量插值法
                newGp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //设置高质量,低速度呈现平滑程度
                newGp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //清空画布并以透明背景色填充
                newGp.Clear(Color.Transparent);
                newGp.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight));
            }
            return newImage;
        }
        /// <summary>
        /// 裁减缩略，根据固定的高度和宽度        
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        /// <returns></returns>
        public static Image ScaleCut(Image image, Int32 width, Int32 height)
        {
            int x = 0;
            int y = 0;
            int ow = image.Width;
            int oh = image.Height;
            if (width >= ow && height >= oh)
            {
                return image;
            }
            //如果结果要比原来的宽
            if (width > ow)
            {
                width = ow;
            }
            if (height > oh)
            {
                height = oh;
            }

            if ((double)image.Width / (double)image.Height > (double)width / (double)height)
            {
                oh = image.Height;
                ow = image.Height * width / height;
                y = 0;
                x = (image.Width - ow) / 2;
            }
            else
            {
                ow = image.Width;
                oh = image.Width * height / width;
                x = 0;
                y = (image.Height - oh) / 2;
            }
            Image newImage = new Bitmap(width, height);
            using (Graphics newGp = Graphics.FromImage(newImage))
            {
                newGp.CompositingQuality = CompositingQuality.HighQuality;
                //设置高质量插值法
                newGp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //设置高质量,低速度呈现平滑程度
                newGp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //清空画布并以透明背景色填充
                newGp.Clear(Color.Transparent);
                newGp.DrawImage(image, new Rectangle(0, 0, width, height),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);
            }
            return newImage;
        }

        /// <summary>
        /// 设定宽高
        /// </summary>
        /// <param name="newWidth">缩略图宽</param>
        /// <param name="newHeight">缩略图高</param>
        /// <param name="oldWidth">原图宽</param>
        /// <param name="oldHeight">原图高</param>
        /// <returns></returns>
        private static Size SetNewSize(int newWidth, int newHeight, int oldWidth, int oldHeight)
        {
            double _width = 0.0;
            double _height = 0.0;
            double _newWidth = 0.0;
            double _newHeight = 0.0;
            double _oldWidth = 0.0;
            double _oldHeight = 0.0;

            _newWidth = Convert.ToInt32(newWidth);
            _newHeight = Convert.ToInt32(newHeight);
            _oldWidth = Convert.ToInt32(oldWidth);
            _oldHeight = Convert.ToInt32(oldHeight);

            if (_oldWidth < _newWidth && _oldHeight < _newHeight)
            {
                _width = _oldWidth;
                _height = _oldHeight;
            }
            else if ((_oldWidth / _oldHeight) > (_newWidth / _newHeight))
            {
                _width = _newWidth;
                _height = (_width * _oldHeight) / _oldWidth;
            }
            else
            {
                _height = _newHeight;
                _width = (_height * _oldWidth) / _oldHeight;
            }
            return new Size(Convert.ToInt32(_width), Convert.ToInt32(_height));
        }

        #endregion

        #region 获取图片大中小三种规格的路径 GetImage
        /// <summary>
        /// 获取图片大中小三种规格的路径
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="ImagePath"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string GetImage(String ImagePath, ImageTypeEnum Type)
        {
            if (string.IsNullOrWhiteSpace(ImagePath))
            {
                return "";
            }
            else
            {
                if (ImagePath.StartsWith("http://"))
                {
                    return ImagePath;
                }
                return "http://img.shejiquan.me" + ImagePath;
            }
#if DEBUG
            return "http://img.shejiquan.me" + ImagePath;
#endif
            if (string.IsNullOrEmpty(ImagePath))
            {
                return "";
            }
            if (ImagePath.StartsWith("http://"))
            {
                return ImagePath;
            }
            var server = System.Web.HttpContext.Current.Server;
            if (string.IsNullOrWhiteSpace(ImagePath))
            {                                                                      
                //传入图片路径为空
                ImagePath = "/Themes/Images/nopic.gif";
            }
            else
            {
                //后缀名的位置
                int dotIndex = ImagePath.LastIndexOf(".");
                System.Text.StringBuilder temp = new System.Text.StringBuilder(ImagePath);
                //截取图片的参数
                var versions = "maxwidth=1200&maxheight=1200&format=jpg";
                //图片增加后缀
                switch (Type)
                {
                    //大图
                    case ImageTypeEnum.BIG:
                        temp.Insert(dotIndex, "_large");
                        break;
                    //中图
                    case ImageTypeEnum.MID:
                        temp.Insert(dotIndex, "_medium");
                        versions = "maxwidth=350&maxheight=350&format=jpg";
                        break;
                    //小图
                    case ImageTypeEnum.SMALL:
                        temp.Insert(dotIndex, "_small");
                        versions = "maxwidth=200&maxheight=200&format=jpg";
                        break;
                    //默认大图
                    default:
                        temp.Insert(dotIndex, "_large");
                        break;
                }
                // 判断截图是否存在
                string ServerPath = server.MapPath(temp.ToString());
                if (!File.Exists(ServerPath))
                {
                    // 判断原图是否存在
                    string SrcServerPath = server.MapPath(ImagePath);
                    if (File.Exists(SrcServerPath))
                    {
                        // 进行截图
                        try
                        {
                            ImageBuilder.Current.Build(new ImageJob(
                                    File.OpenRead(SrcServerPath),
                                    ServerPath.Substring(0, ServerPath.LastIndexOf(".")),//不带后缀名的图片名称
                                    new Instructions(versions),
                                    false,//是否保留原图
                                    true));//是否增加后缀
                            ImagePath = temp.ToString();
                        }
                        catch
                        {
                            // 截图异常
                            ImagePath = "/Themes/Images/nopic.gif";
                        }
                    }
                    else
                    {
                        // 原图不存在
                        ImagePath = "/Themes/Images/nopic.gif";
                    }
                }
                else
                {
                    ImagePath = temp.ToString();
                }
            }
            return "http://m.weishop.me" + ImagePath;
        }
        #endregion
        /// <summary>
        /// 生成缩略图        
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);
            ImageFormat imgFormat = originalImage.RawFormat;
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            Size newSize = new Size();//SetNewSize(width, height, originalImage.Width, originalImage.Height);
            switch (mode.ToUpper())
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "CUT"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                case "CUTHW"://指定高，宽按比例(宽或高不够时补空白)
                    newSize = SetNewSize(width, height, originalImage.Width, originalImage.Height);
                    break;
                default:
                    break;
            }
            ////使用MagickNet对图片进行处理，首先按指定大小比例对图片进行剪切，然后进行缩放，采用CUT方式
            //MagickNet.Image img = new MagickNet.Image(originalImagePath);
            //img.Crop(new System.Drawing.Rectangle(x, y, ow, oh));
            //img.Resize(new System.Drawing.Size(towidth, toheight));
            //img.Write(thumbnailPath);
            //MagickNet.Magick.Term();

            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            //清空画布并以白色背景色填充
            g.Clear(Color.White);
            //在指定位置画图
            if (mode.ToUpper().Equals("CUTHW"))
            {
                g.DrawImage(originalImage, new Rectangle((width - newSize.Width) / 2, (height - newSize.Height) / 2, newSize.Width, newSize.Height), 2, 2, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel);
                g.DrawRectangle(new Pen(Color.FromArgb(222, 222, 222)), new Rectangle(0, 0, width - 1, height - 1));
            }
            else
            {
                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                    new Rectangle(x, y, ow, oh),
                    GraphicsUnit.Pixel);
            }
            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, GetFormat(thumbnailPath));
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        /// <summary>
        /// 打水印，在某一点        
        /// </summary>
        /// <param name="image"></param>
        /// <param name="waterImagePath"></param>
        /// <param name="p"></param>
        public static void Makewater(Image image, String waterImagePath, Point p)
        {
            JieYiGuang.Common.ImageUtils.Makewater(image, waterImagePath, p, ImagePosition.TopLeft);
        }
        public static void Makewater(Image image, String waterImagePath, Point p, ImagePosition imagePosition)
        {
            using (Image warterImage = Image.FromFile(waterImagePath))
            {
                using (Graphics newGp = Graphics.FromImage(image))
                {
                    newGp.CompositingQuality = CompositingQuality.HighQuality;
                    //设置高质量插值法
                    newGp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //设置高质量,低速度呈现平滑程度
                    newGp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    switch (imagePosition)
                    {
                        case ImagePosition.BottomLeft:
                            p.Y = image.Height - warterImage.Height - p.Y;
                            break;
                        case ImagePosition.TopRigth:
                            p.X = image.Width - warterImage.Width - p.X;
                            break;
                        case ImagePosition.BottomRight:
                            p.Y = image.Height - warterImage.Height - p.Y;
                            p.X = image.Width - warterImage.Width - p.X;
                            break;
                    }
                    newGp.DrawImage(warterImage, new Rectangle(p, new Size(warterImage.Width, warterImage.Height)));
                }
            }
        }
        public static void Makewater(Image image, String waterStr, Font font, Brush brush, Point p)
        {
            JieYiGuang.Common.ImageUtils.Makewater(image, waterStr, font, brush, p, ImagePosition.TopLeft);

        }
        public static void Makewater(Image image, String waterStr, Font font, Brush brush, Point p, ImagePosition imagePosition)
        {
            using (Graphics newGp = Graphics.FromImage(image))
            {
                Int32 stringWidth;
                Int32 stringHeight;
                stringHeight = (int)font.Size;
                stringWidth = (int)(((float)StringDeal.GetBitLength(waterStr) / (float)2) * (font.Size + 1));
                newGp.CompositingQuality = CompositingQuality.HighQuality;
                //设置高质量插值法
                newGp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //设置高质量,低速度呈现平滑程度
                newGp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //文字抗锯齿
                newGp.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                switch (imagePosition)
                {
                    case ImagePosition.BottomLeft:
                        p.Y = image.Height - stringHeight - p.Y;
                        break;
                    case ImagePosition.TopRigth:
                        p.X = image.Width - stringWidth - p.X;
                        break;
                    case ImagePosition.BottomRight:
                        p.Y = image.Height - stringHeight - p.Y;
                        p.X = image.Width - stringWidth - p.X;
                        break;
                }
                newGp.DrawString(waterStr, font, brush, p);
            }
        }
        /// <summary>
        /// 高质量保存               
        /// </summary>
        /// <param name="image"></param>
        /// <param name="path"></param>
        public static void SaveQuality(Image image, String path)
        {
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = ImageCodecInfo.GetImageEncoders()[0];
            myEncoder = Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L); // 0-100
            myEncoderParameters.Param[0] = myEncoderParameter;
            try
            {
                image.Save(path, myImageCodecInfo, myEncoderParameters);
            }
            finally
            {
                myEncoderParameter.Dispose();
                myEncoderParameters.Dispose();
            }
        }

        ///<summary> 
        /// 按比例缩放图片 
        /// </summary> 
        /// <param name="imgUrl">图片的路径</param> 
        /// <param name="imgHeight">图片的高度</param> 
        /// <param name="imgWidth">图片的宽度</param> 
        /// <returns></returns> 
        public static string GetImageSize(string imgUrl, int imgHeight, int imgWidth)
        {
            string fileName = System.Web.HttpContext.Current.Server.MapPath(imgUrl);
            string strResult = string.Empty;
            if (System.IO.File.Exists(fileName) && imgHeight != 0 && imgWidth != 0)
            {
                decimal desWidth; decimal desHeight;                                            //目标宽高 
                System.Drawing.Image objImage = System.Drawing.Image.FromFile(fileName);
                decimal radioAct = (decimal)objImage.Width / (decimal)objImage.Height;        //原始图片的宽高比 
                decimal radioLoc = (decimal)imgWidth / (decimal)imgHeight;                    //图片位的宽高比 
                if (radioAct > radioLoc)                                                        //原始图片比图片位宽 
                {
                    decimal dcmZoom = (decimal)imgWidth / (decimal)objImage.Width;
                    desHeight = objImage.Height * dcmZoom;
                    desWidth = imgWidth;
                }
                else
                {
                    decimal dcmZoom = (decimal)imgHeight / (decimal)objImage.Height;
                    desWidth = objImage.Width * dcmZoom;
                    desHeight = imgHeight;
                }
                objImage.Dispose();                //释放资源 
                strResult = "width=\"" + Convert.ToString((int)desWidth) + "\" height=\""
                   + Convert.ToString((int)desHeight) + "\" ";
            }
            return strResult;
        }

        /// <summary>
        /// 按宽和高生成截取
        /// </summary>
        /// <param name="originalImagePath">原始图片(物理路径)</param>
        /// <param name="thumbnailPath">生成路径(物理路径)</param>
        /// <param name="width">截取宽度</param>
        /// <param name="height">截取高度</param>
        public static void GetCutImage(string originalImagePath, string thumbnailPath, int width, int height)
        {
            if (File.Exists(thumbnailPath)) return;
            //MakeThumbnail(originalImagePath, thumbnailPath, width, height, "CUT");
            ImageCutHelper.CutForCustom(originalImagePath,thumbnailPath,width,height,100);
        }

        #region 新增图片裁剪方法
        /// <summary>
        /// 裁剪图片并保存
        /// </summary>
        /// <param name="fileName">源图路径（绝对路径）</param>
        /// <param name="newFileName">缩略图路径（绝对路径）</param>
        /// <param name="maxWidth">缩略图宽度</param>
        /// <param name="maxHeight">缩略图高度</param>
        /// <param name="cropWidth">裁剪宽度</param>
        /// <param name="cropHeight">裁剪高度</param>
        /// <param name="X">X轴</param>
        /// <param name="Y">Y轴</param>
        public static bool MakeThumbnailImage(string fileName, string newFileName, int maxWidth, int maxHeight, int cropWidth, int cropHeight, int X, int Y)
        {
            byte[] imageBytes = File.ReadAllBytes(fileName);
            Image originalImage = Image.FromStream(new System.IO.MemoryStream(imageBytes));
            Bitmap b = new Bitmap(cropWidth, cropHeight);
            try
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    //设置高质量插值法
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //设置高质量,低速度呈现平滑程度
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    //清空画布并以透明背景色填充
                    g.Clear(Color.Transparent);
                    //在指定位置并且按指定大小绘制原图片的指定部分
                    g.DrawImage(originalImage, new Rectangle(0, 0, cropWidth, cropWidth), X, Y, cropWidth, cropHeight, GraphicsUnit.Pixel);
                    Image displayImage = new Bitmap(b, maxWidth, maxHeight);
                    SaveImage(displayImage, newFileName, GetCodecInfo("image/" + GetFormat(newFileName).ToString().ToLower()));
                    return true;
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                b.Dispose();
            }
        }
        #endregion

        #region Helper
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image">Image 对象</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="ici">指定格式的编解码参数</param>
        private static void SaveImage(Image image, string savePath, ImageCodecInfo ici)
        {
            //设置 原图片 对象的 EncoderParameters 对象
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, ((long)100));
            image.Save(savePath, ici, parameters);
            parameters.Dispose();
        }

        /// <summary>
        /// 获取图像编码解码器的所有相关信息
        /// </summary>
        /// <param name="mimeType">包含编码解码器的多用途网际邮件扩充协议 (MIME) 类型的字符串</param>
        /// <returns>返回图像编码解码器的所有相关信息</returns>
        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType)
                    return ici;
            }
            return null;
        }

        /// <summary>
        /// 得到图片格式
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <returns></returns>
        private static ImageFormat GetFormat(string name)
        {
            string ext = name.Substring(name.LastIndexOf(".") + 1);
            switch (ext.ToLower())
            {
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "bmp":
                    return ImageFormat.Bmp;
                case "png":
                    return ImageFormat.Png;
                case "gif":
                    return ImageFormat.Gif;
                default:
                    return ImageFormat.Jpeg;
            }
        }

        #endregion

        #region 其它

        /// <summary>
        /// 判断文件类型是否为WEB格式图片
        /// (注：JPG,GIF,BMP,PNG)
        /// </summary>
        /// <param name="contentType">HttpPostedFile.ContentType</param>
        /// <returns></returns>
        public static bool IsImage(string contentType)
        {
            if (contentType == "image/pjpeg" || contentType == "image/jpeg" || contentType == "image/gif" || contentType == "image/bmp" || contentType == "image/png")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

        #endregion

        
        /// <summary>
        /// 获取图片大中小三种规格的路径
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="ImagePath"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string GetCutImg(String imgpath, int width, int height)
        {
            if (string.IsNullOrEmpty(imgpath) || imgpath.IndexOf(".") < 1)
            {
                return ("http://img.shejiquan.me/Content/images/untitled.png");
            }

            if (!imgpath.ToLower().StartsWith("http"))
            {
                var ext = Path.GetExtension(imgpath);
                imgpath = imgpath.Substring(0, imgpath.LastIndexOf(".")) + "_" + width + "_" + height + ext;
                imgpath = "http://img.shejiquan.me" + imgpath;
            }
            return imgpath;
        }
        public static string BtxCMS_GetImage(string imgpath)
        {
            if (string.IsNullOrEmpty(imgpath) || imgpath.IndexOf(".") < 1)
            {
                return ("http://img.shejiquan.me/Content/images/untitled.png");
            }

            if (!imgpath.ToLower().StartsWith("http"))
            {
                imgpath = "http://img.shejiquan.me" + imgpath;
            }
            return imgpath;
        }

    }
    //当字符串中有中文时，一个中文的长度表示为2
    //如 StringDeal.GetBitLength("123")没有中文返回的长度是3
    //如 StringDeal.GetBigLength("123四")有中文返回的长度是5，如果直接用"123四".Length返回的是4
    public static class StringDeal
    {
        public static int GetBitLength(string waterStr)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+");
            int length = waterStr.Length;

            for (int i = 0; i < waterStr.Length; i++)
            {
                if (regex.IsMatch(waterStr.Substring(i, 1)))
                {
                    length++;
                }
            }

            return length;
        }
    }
    public enum StringPosition
    {
        TopLeft,
        BottomLeft
    }
    public enum ImagePosition
    {
        TopLeft,
        BottomLeft,
        BottomRight,
        TopRigth
    }

    #region 图片规格后缀枚举
    public enum ImageTypeEnum
    {
        BIG,
        MID,
        SMALL
    }
    #endregion
}
