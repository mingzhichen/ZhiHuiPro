using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace JieYiGuang.Common.Utils.Mail
{
    public class SmtpMail
    {

        private Senter senter;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SmtpMail()
        {
            senter = new Senter();
        }

        /// <summary>
        /// 增加一个新的发送邮件信息
        /// </summary>
        public void Add(MailMessage doc)
        {
            senter.Add(doc);
        }

        private sealed class Senter
        {
            /// <summary>
            /// 最大缓冲文档数
            /// </summary>
            public int maxBufferLength = 100;

            /// <summary>
            /// 执行线程
            /// </summary>
            public Thread thread;

            /// <summary>
            /// 添加任务队列
            /// </summary>
            public Queue<MailMessage> addQueue;

            /// <summary>
            /// 器构造函数
            /// </summary>
            public Senter()
            {
                addQueue = new Queue<MailMessage>();

                thread = new Thread(new ThreadStart(SenteHandler));
                thread.IsBackground = true;
                thread.Priority = ThreadPriority.BelowNormal;
                thread.Start();
            }

            /// <summary>
            /// 将一个文档加入到队列
            /// </summary>
            /// <param name="doc">待索引文档</param>
            public void Add(MailMessage doc)
            {
                this.addQueue.Enqueue(doc);
            }

            ~Senter()
            {

            }

             /// <summary>
            /// 任务执行器
            /// </summary>
            public void SenteHandler()
            {
               int count = 0;
               MailMessage doc;
                //处理循环
                while (true)
                {
                    //处理新增队列
                    while (addQueue.Count > 0 && count < maxBufferLength)
                    {
                        count++;

                        //发送邮件
                        doc = addQueue.Dequeue();
                        
                        //Sentmail.SentmailByJmail(doc.Subject, doc.Body, doc.FromName, doc.From, doc.RecipientEmail, doc.MailServer, doc.MailServerUserName, doc.MailServerPassWord);
                        Sentmail.SentmailByDotnet(doc.Subject, doc.Body, doc.FromName, doc.From, doc.RecipientEmail, doc.MailServer, doc.MailServerUserName, doc.MailServerPassWord,25,true);
                       
                        //WriteFile(@"E:\WebSite\BouSun_New\BouSun.Web\" + count.ToString() + ".txt", doc.Subject + "<br />" + doc.Body + "<br />" + doc.FromName + "<br />" + doc.From + "<br />" + doc.RecipientEmail + "<br />" + doc.MailServer + "<br />" + doc.MailServerUserName + "<br />" + doc.MailServerPassWord);
                    }

                    //检测处理次数是否达到最大缓冲数
                    if (count >= maxBufferLength)
                    {
                        count = 0;
                    }
                    //则线程暂停2000毫秒
                    Thread.Sleep(2000);
                }

            }

            /// <summary>
            /// 写文件
            /// </summary>
            /// <param name="Path">文件路径</param>
            /// <param name="Strings">文件内容</param>
            public void WriteFile(string Path, string Strings)
            {
                if (!System.IO.File.Exists(Path))
                {
                    System.IO.FileStream f = System.IO.File.Create(Path);
                    f.Close();
                }
                System.IO.StreamWriter f2 = new System.IO.StreamWriter(Path, false, System.Text.Encoding.GetEncoding("utf-8"));
                f2.Write(Strings);
                f2.Close();
                f2.Dispose();
            }

        }
    }
}
