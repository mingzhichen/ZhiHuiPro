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
        /// ���캯��
        /// </summary>
        public SmtpMail()
        {
            senter = new Senter();
        }

        /// <summary>
        /// ����һ���µķ����ʼ���Ϣ
        /// </summary>
        public void Add(MailMessage doc)
        {
            senter.Add(doc);
        }

        private sealed class Senter
        {
            /// <summary>
            /// ��󻺳��ĵ���
            /// </summary>
            public int maxBufferLength = 100;

            /// <summary>
            /// ִ���߳�
            /// </summary>
            public Thread thread;

            /// <summary>
            /// ����������
            /// </summary>
            public Queue<MailMessage> addQueue;

            /// <summary>
            /// �����캯��
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
            /// ��һ���ĵ����뵽����
            /// </summary>
            /// <param name="doc">�������ĵ�</param>
            public void Add(MailMessage doc)
            {
                this.addQueue.Enqueue(doc);
            }

            ~Senter()
            {

            }

             /// <summary>
            /// ����ִ����
            /// </summary>
            public void SenteHandler()
            {
               int count = 0;
               MailMessage doc;
                //����ѭ��
                while (true)
                {
                    //������������
                    while (addQueue.Count > 0 && count < maxBufferLength)
                    {
                        count++;

                        //�����ʼ�
                        doc = addQueue.Dequeue();
                        
                        //Sentmail.SentmailByJmail(doc.Subject, doc.Body, doc.FromName, doc.From, doc.RecipientEmail, doc.MailServer, doc.MailServerUserName, doc.MailServerPassWord);
                        Sentmail.SentmailByDotnet(doc.Subject, doc.Body, doc.FromName, doc.From, doc.RecipientEmail, doc.MailServer, doc.MailServerUserName, doc.MailServerPassWord,25,true);
                       
                        //WriteFile(@"E:\WebSite\BouSun_New\BouSun.Web\" + count.ToString() + ".txt", doc.Subject + "<br />" + doc.Body + "<br />" + doc.FromName + "<br />" + doc.From + "<br />" + doc.RecipientEmail + "<br />" + doc.MailServer + "<br />" + doc.MailServerUserName + "<br />" + doc.MailServerPassWord);
                    }

                    //��⴦������Ƿ�ﵽ��󻺳���
                    if (count >= maxBufferLength)
                    {
                        count = 0;
                    }
                    //���߳���ͣ2000����
                    Thread.Sleep(2000);
                }

            }

            /// <summary>
            /// д�ļ�
            /// </summary>
            /// <param name="Path">�ļ�·��</param>
            /// <param name="Strings">�ļ�����</param>
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
