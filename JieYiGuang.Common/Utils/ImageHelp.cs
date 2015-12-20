using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace JieYiGuang.Common.Helper
{
    public static class GenerateThumbnail
    {
        /**/
        /// <summary>
        ///  生成缩略图 静态方法   
        /// </summary>
        /// <param name="pathImageFrom"> 源图的路径(含文件名及扩展名) </param>
        /// <param name="pathImageTo"> 生成的缩略图所保存的路径(含文件名及扩展名)
        ///                            注意：扩展名一定要与生成的缩略图格式相对应 </param>
        /// <param name="width"> 欲生成的缩略图 "画布" 的宽度(像素值) </param>
        /// <param name="height"> 欲生成的缩略图 "画布" 的高度(像素值) </param>
        public static string GenThumbnail(string pathImageFrom, string pathImageTo, int width, int height)
        {
            Image imageFrom = null;

            try
            {
                Stream s= File.Open(pathImageFrom, FileMode.Open);
                imageFrom = Image.FromStream(s);
                s.Close();
            }
            catch
            {
                //throw;
            }

            if (imageFrom == null)
            {
                return "抱歉，您上传的图片不符合要求，请尝试上传其他图片";
            }

            // 源图宽度及高度
            int imageFromWidth = imageFrom.Width;
            int imageFromHeight = imageFrom.Height;

            // 生成的缩略图实际宽度及高度
            int bitmapWidth = width;
            int bitmapHeight = height;

            // 生成的缩略图在上述"画布"上的位置
            int X = 0;
            int Y = 0;

            // 根据源图及欲生成的缩略图尺寸,计算缩略图的实际尺寸及其在"画布"上的位置
//             if (bitmapHeight * imageFromWidth > bitmapWidth * imageFromHeight)
//             {
//                 bitmapHeight = imageFromHeight * width / imageFromWidth;
//                 Y = (height - bitmapHeight) / 2;
//             }
//             else
//             {
//                 bitmapWidth = imageFromWidth * height / imageFromHeight;
//                 X = (width - bitmapWidth) / 2;
//             }

            // 创建画布
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);

            // 用白色清空
            g.Clear(Color.White);

            // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // 指定高质量、低速度呈现。
            g.SmoothingMode = SmoothingMode.HighQuality;

            // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。
            g.DrawImage(imageFrom, new Rectangle(X, Y, bitmapWidth, bitmapHeight), new Rectangle(0, 0, imageFromWidth, imageFromHeight), GraphicsUnit.Pixel);

            try
            {
                //经测试 .jpg 格式缩略图大小与质量等最优
                if (!Directory.Exists(Path.GetDirectoryName(pathImageTo)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(pathImageTo));
                }
                bmp.Save(pathImageTo, ImageFormat.Jpeg);
            }
            catch
            {
                return "抱歉，上传异常，请重新尝试上传其它图片";
            }
            finally
            {
                //显示释放资源
                imageFrom.Dispose();
                bmp.Dispose();
                g.Dispose();
                GC.Collect();
            }
            return "";
        }

        public static void GenThumDir(string dir, int width, int height)
        {
            var files = Directory.GetFiles(dir);
            var fileArray = files.Where(v => Path.GetExtension(v).EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                Path.GetExtension(v).EndsWith("jpeg", StringComparison.OrdinalIgnoreCase) ||
                Path.GetExtension(v).EndsWith("png", StringComparison.OrdinalIgnoreCase) ||
                Path.GetExtension(v).EndsWith("bmp", StringComparison.OrdinalIgnoreCase) ||
                Path.GetExtension(v).EndsWith("gif", StringComparison.OrdinalIgnoreCase)).OrderBy(v => Path.GetFileNameWithoutExtension(v));
            fileArray.ToList().ForEach(v =>
            {
                GenThumbnail(v, Path.Combine(Path.Combine(Path.GetDirectoryName(v), "缩略图"), Path.GetFileName(v)), width, height);

            });

        }

    }

}