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
        /// ��������ͼ
        /// </summary>
        /// <param name="originalImagePath">Դͼ·��������·����</param>
        /// <param name="thumbnailPath">����ͼ·��������·����</param>
        /// <param name="width">����ͼ���</param>
        /// <param name="height">����ͼ�߶�</param>
        /// <param name="mode">��������ͼ�ķ�ʽ</param>    
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
                case "HW"://ָ���߿����ţ����ܱ��Σ�                
                    break;
                case "W"://ָ�����߰�����                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://ָ���ߣ�������
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://ָ���߿�ü��������Σ�                
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

            //�½�һ��bmpͼƬ
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //�½�һ������
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //���ø�������ֵ��
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //���ø�����,���ٶȳ���ƽ���̶�
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //��ջ�������͸������ɫ���
            g.Clear(System.Drawing.Color.Transparent);

            //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //��jpg��ʽ��������ͼ
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
        /// ��ͼƬ����������ˮӡ
        /// </summary>
        /// <param name="Path">ԭ������ͼƬ·��</param>
        /// <param name="Path_sy">���ɵĴ�����ˮӡ��ͼƬ·��</param>
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
        /// ��ͼƬ������ͼƬˮӡ
        /// </summary>
        /// <param name="Path">ԭ������ͼƬ·��</param>
        /// <param name="Path_syp">���ɵĴ�ͼƬˮӡ��ͼƬ·��</param>
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
            //�Զ�����Զ��ͼƬ
            WebClient client = new WebClient();
            //����Reg:<img.*?src=([\"\'])(http:\/\/.+\.(jpg|gif|bmp|bnp))\1.*?>
            Regex reg = new Regex("IMG[^>]*?src\\s*=\\s*(?:\"(?<1>[^\"]*)\"|'(?<1>[^\']*)')", RegexOptions.IgnoreCase);
            MatchCollection m = reg.Matches(str);

            foreach (Match math in m)
            {
                string imgUrl = math.Groups[1].Value;

                //��ԭͼƬ����ǰ��YYMMDD���������ϴ�

                Regex regName = new Regex(@"\w+.(?:jpg|gif|bmp|png)", RegexOptions.IgnoreCase);

                string strNewImgName = DateTime.Now.ToShortDateString().Replace("-", "") + regName.Match(imgUrl).ToString();

                try
                {
                    //����ͼƬ
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
