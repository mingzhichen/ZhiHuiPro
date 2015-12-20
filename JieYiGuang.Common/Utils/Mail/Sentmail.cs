using System;
using System.Collections.Generic;
using System.Text;

namespace JieYiGuang.Common.Utils.Mail
{
    public class Sentmail
    {
        /// <summary>
        /// 发送邮件
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

            ////Silent属性：如果设置为true,JMail不会抛出例外错误. JMail. Send( () 会根据操作结果返回true或false
            //Jmail.Silent = true;
            ////Jmail创建的日志，前提loging属性设置为true
            //Jmail.Logging = true;
            ////字符集，缺省为"US-ASCII"
            //Jmail.Charset = "GB2312";
            ////信件的contentype. 缺省是"text/plain"） : 字符串如果你以HTML格式发送邮件, 改为"text/html"即可。
            //Jmail.ContentType = "text/html";
            ////添加收件人
            //Jmail.AddRecipient(ToEmail, "", "");
            ////发件人名称
            //Jmail.FromName = FromName;
            ////发件人邮件地址
            //Jmail.From = FromEmail;
            ////发件人邮件用户名
            //Jmail.MailServerUserName = mailserverusername;
            ////发件人邮件密码
            //Jmail.MailServerPassWord = mailserverpassword;
            ////设置邮件标题
            //Jmail.Subject = Subject;

            //Jmail.Body = body + "<div style=\"width:90%;\"><br /><br />&nbsp;&nbsp;&nbsp;&nbsp;" + FromName + "<br />" + t.ToString() + "<br /><br /><hr /><span style=\"color:red;\">重要提示</span>：请不要回复本邮件！</div>";

            ////Jmail发送的方法
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
            //myEmail.BodyFormat = this.Html?MailFormat.Html:MailFormat.Text; //邮件形式，.Text、.Html 


            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = stmpserver;
            smtp.Port = MailDomainPort;
            smtp.Credentials = new System.Net.NetworkCredential(mailserverusername, mailserverpassword);
            //smtp.UseDefaultCredentials = true;
            //smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            //当不是25端口(gmail:587)
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
