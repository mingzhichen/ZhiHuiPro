using System;
using System.Collections.Generic;
using System.Text;

namespace JieYiGuang.Common.Utils.Mail
{
    public class Sentmail
    {
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="Subject"></param>
        /// <param name="body"></param>
        /// <param name="FromEmail"></param>
        /// <param name="ToEmail"></param>
        /// <param name="stmpserver"></param>
        /// <param name="mailserverusername"></param>
        /// <param name="mailserverpassword"></param>
        public static void SentmailByJmail(string Subject, string body, string FromName, string FromEmail, string ToEmail, string stmpserver, string mailserverusername, string mailserverpassword)
        {
            //DateTime t = DateTime.Now;
            //jmail.Message Jmail = new jmail.Message();

            ////Silent���ԣ��������Ϊtrue,JMail�����׳��������. JMail. Send( () ����ݲ����������true��false
            //Jmail.Silent = true;
            ////Jmail��������־��ǰ��loging��������Ϊtrue
            //Jmail.Logging = true;
            ////�ַ�����ȱʡΪ"US-ASCII"
            //Jmail.Charset = "GB2312";
            ////�ż���contentype. ȱʡ��"text/plain"�� : �ַ����������HTML��ʽ�����ʼ�, ��Ϊ"text/html"���ɡ�
            //Jmail.ContentType = "text/html";
            ////����ռ���
            //Jmail.AddRecipient(ToEmail, "", "");
            ////����������
            //Jmail.FromName = FromName;
            ////�������ʼ���ַ
            //Jmail.From = FromEmail;
            ////�������ʼ��û���
            //Jmail.MailServerUserName = mailserverusername;
            ////�������ʼ�����
            //Jmail.MailServerPassWord = mailserverpassword;
            ////�����ʼ�����
            //Jmail.Subject = Subject;

            //Jmail.Body = body + "<div style=\"width:90%;\"><br /><br />&nbsp;&nbsp;&nbsp;&nbsp;" + FromName + "<br />" + t.ToString() + "<br /><br /><hr /><span style=\"color:red;\">��Ҫ��ʾ</span>���벻Ҫ�ظ����ʼ���</div>";

            ////Jmail���͵ķ���
            //Jmail.Send(stmpserver, false);
            //Jmail.Close();
        }

        public static string SentmailByDotnet(string Subject, string body, string FromName, string FromEmail, string ToEmail, string stmpserver, string mailserverusername, string mailserverpassword, int MailDomainPort, bool ismrport)
        {
            System.Net.Mail.MailMessage myEmail = new System.Net.Mail.MailMessage();
            Encoding eEncod = Encoding.GetEncoding("utf-8");

            myEmail.From = new System.Net.Mail.MailAddress(FromEmail, FromName, eEncod);

            myEmail.To.Add(ToEmail);

            myEmail.Subject = Subject;
            myEmail.SubjectEncoding = Encoding.GetEncoding("utf-8");
            myEmail.IsBodyHtml = true;
            myEmail.Body = body;
            myEmail.BodyEncoding = Encoding.GetEncoding("utf-8");
            myEmail.Priority = System.Net.Mail.MailPriority.High;
            //myEmail.BodyFormat = this.Html?MailFormat.Html:MailFormat.Text; //�ʼ���ʽ��.Text��.Html 


            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = stmpserver;
            smtp.Port = MailDomainPort;
            smtp.Credentials = new System.Net.NetworkCredential(mailserverusername, mailserverpassword);
            //smtp.UseDefaultCredentials = true;
            //smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            //������25�˿�(gmail:587)
            //if (!ismrport)
            //{
            //    smtp.EnableSsl = true;
            //}


            try
            {
                smtp.Send(myEmail);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //string result = "Subject:" + Subject + " body:" + body + " FromName:" + FromName + " FromEmail:" + FromEmail + " ToEmail:" + ToEmail + " stmpserver:" + stmpserver + " mailserverusername:" + mailserverusername + " mailserverpassword:" + mailserverpassword + " MailDomainPort:" + MailDomainPort + " ismrport:" + ismrport;
                throw ex;
                return "0";
            }
            return "1";
        }

    }
}
