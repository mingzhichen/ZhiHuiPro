using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Text.RegularExpressions;

namespace JieYiGuang.Common.Control
{
    public class ImageHandle
    {
        /**/
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
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
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
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
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

        /**/
        /// <summary>
        /// 在图片上增加文字水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_sy">生成的带文字水印的图片路径</param>
        public static void AddWater(string Path, string Path_sy)
        {
            string addText = System.Configuration.ConfigurationManager.AppSettings["WaterText"];
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(image, 0, 0, image.Width, image.Height);




            //int[] sizes = new int[] {16,14,12,10,8,6,4};
            Font crFont = null;
            SizeF crSize = new SizeF();
            //for (int i=0 ;i<7; i++)
            //{
            crFont = new Font("arial", 10, FontStyle.Bold);
            crSize = g.MeasureString(addText, crFont);

            //if ((ushort)crSize.Width < (ushort)image.Width)
            //break;
            //}
            float xpos = 0;
            float ypos = 0;
            xpos = ((float)image.Width * (float).99) - crSize.Width;
            ypos = ((float)image.Height * (float).99) - crSize.Height;


            //System.Drawing.Font f = new System.Drawing.Font("Verdana", 40);
            System.Drawing.Brush b = new System.Drawing.SolidBrush(Color.FromArgb(200, 200, 200, 200));

            g.DrawString(addText, crFont, b, xpos, ypos);
            g.Dispose();

            image.Save(Path_sy);
            image.Dispose();
            //System.IO.File.Delete(Path);
        }

        /**/
        /// <summary>
        /// 在图片上生成图片水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_syp">生成的带图片水印的图片路径</param>
        public static void AddWaterPic(string Path, string Path_syp)
        {
            string Path_sypf = System.Configuration.ConfigurationManager.AppSettings["WaterPicPath"];
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(copyImage, new System.Drawing.Rectangle(image.Width - copyImage.Width, image.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();


            image.Save(Path_syp);
            image.Dispose();
            //System.IO.File.Delete(Path);
        }
        public static void CatchImage(string str)
        {
            //自动保存远程图片
            WebClient client = new WebClient();
            //备用Reg:<img.*?src=([\"\'])(http:\/\/.+\.(jpg|gif|bmp|bnp))\1.*?>
            Regex reg = new Regex("IMG[^>]*?src\\s*=\\s*(?:\"(?<1>[^\"]*)\"|'(?<1>[^\']*)')", RegexOptions.IgnoreCase);
            MatchCollection m = reg.Matches(str);

            foreach (Match math in m)
            {
                string imgUrl = math.Groups[1].Value;

                //在原图片名称前加YYMMDD重名名并上传

                Regex regName = new Regex(@"\w+.(?:jpg|gif|bmp|png)", RegexOptions.IgnoreCase);

                string strNewImgName = DateTime.Now.ToShortDateString().Replace("-", "") + regName.Match(imgUrl).ToString();

                try
                {
                    //保存图片
                    client.DownloadFile(imgUrl, "~/images/" + strNewImgName);

                }
                catch
                {
                }
                finally
                {

                }
                client.Dispose();
            }
        }
    }
}
