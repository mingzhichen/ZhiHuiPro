using System;
using System.Net.Mail;

namespace JieYiGuang.Common.Helper
{
    public abstract class MailHelper
    {
        public string _From = "s1@devotop.com";//默认发送邮件
        public string EmailHost = "smtp.mxhichina.com";//SMTP服务器
        public string EmailUserName = "s1@devotop.com";//SMTP服务器
        public string EmailPassWord = "S1devotop";//SMTP服务器
        public bool EmailEnablessl = false;//SMTP服务器

        public int EmailPort = 25;


        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 收件地址
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// 发件人,发送邮件地址
        /// </summary>
        public string From
        {
            get { return _From; }
            set { _From = value; }
        }
        /// <summary>
        /// 发送
        /// </summary>
        public abstract void Send();
        /// <summary>
        /// 群发邮件
        /// </summary>
        public abstract void GroupSend(string[] groupTo);
        /// <summary>
        /// 创建一个邮件实体
        /// </summary>
        /// <returns></returns>
        public static MailHelper CreateInstance()
        {
            return new SmtpMail();
        }
    }
    /*
    /// <summary>
    /// 用JMail发送邮件
    /// </summary>
    public class JMail : MailHelper
    {
        public JMail()
        {
            //jmail.Message = new jmail.Message();
        }
        public override void Send()
        {
            jmail.Message Jmail = new jmail.Message();
            DateTime t = DateTime.Now;
            String Subject = _Subject;
            String body = _Body;
            String FromEmail = _From;
            String ToEmail = _To;
            //Silent属性：如果设置为true,JMail不会抛出例外错误. JMail. Send( () 会根据操作结果返回true或false
            Jmail.Silent = false;
            //Jmail创建的日志，前提loging属性设置为true
            Jmail.Logging = true;
            //字符集，缺省为"US-ASCII"
            Jmail.Charset = System.Text.Encoding.Default.BodyName;// "GB2312";
            //信件的contentype. 缺省是"text/plain"） : 字符串如果你以HTML格式发送邮件, 改为"text/html"即可。
            Jmail.ContentType = "text/html";
            //添加收件人
            Jmail.AddRecipient(ToEmail, "", "");
            Jmail.From = FromEmail;
            ///Jmail.
            //发件人邮件用户名
            Jmail.MailServerUserName = EmailUserName;
            //发件人邮件密码
            Jmail.MailServerPassWord = EmailPassWord;
            //设置邮件标题
            Jmail.Subject = Subject;
            //邮件添加附件,(多附件的话，可以再加一条Jmail.AddAttachment( "c:\\test.jpg",true,null);)就可以搞定了。［注］：加了附件，讲把上面的Jmail.ContentType= "text/html";删掉。否则会在邮件里出现乱码。
            //Jmail.AddAttachment("c:\\test.jpg", true, null);
            //邮件内容
            Jmail.Body = body;// +t.ToString();
            //Jmail发送的方法
            Jmail.Send(EmailHost, false);
            Jmail.Close();
        }
        /// <summary>
        /// 群发
        /// </summary>
        /// <param name="groupTo"></param>
        public override void GroupSend(string[] groupTo)
        {

        }
    }
    */
    /// <summary>
    /// SMTP邮件
    /// </summary>
    public class SmtpMail : MailHelper
    {
        private SmtpClient _smtpClient;
        public SmtpMail()
        {
            _smtpClient = new SmtpClient();
            _smtpClient.Timeout = 100000;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            _smtpClient.Host = EmailHost;//指定SMTP服务器
            _smtpClient.Port = EmailPort;
            _smtpClient.Credentials = new System.Net.NetworkCredential(EmailUserName, EmailPassWord);//用户名和密码
            _smtpClient.EnableSsl = EmailEnablessl;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        public override void Send()
        {
            MailMessage _mailMessage = PreSend();
            _mailMessage.To.Add(new MailAddress(To));
            SendOut(_mailMessage);
        }
        /// <summary>
        /// 群发
        /// </summary>
        /// <param name="groupTo"></param>
        public override void GroupSend(string[] groupTo)
        {
            MailMessage _mailMessage = PreSend();
            foreach (string to in groupTo)
            {
                _mailMessage.To.Add(new MailAddress(to));
            }
            SendOut(_mailMessage);
        }
        /// <summary>
        /// 准备MailMessage类
        /// </summary>
        /// <returns></returns>
        private MailMessage PreSend()
        {
            MailMessage _mailMessage = new MailMessage();
            _mailMessage.From = new MailAddress(this.From);
            _mailMessage.Subject = this.Subject;
            _mailMessage.Body = this.Body;//内容
            _mailMessage.BodyEncoding = System.Text.Encoding.UTF8;//正文编码
            _mailMessage.IsBodyHtml = true;//设置为HTML格式
            _mailMessage.Priority = MailPriority.High;//优先级
            return _mailMessage;
        }
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="mailMessage"></param>
        private void SendOut(MailMessage mailMessage)
        {
            try
            {

                _smtpClient.Send(mailMessage);
            }
            catch (ArgumentNullException e)
            {
                throw e;
                //From 为 空引用（在 Visual Basic 中为 Nothing）。- 或 -
                //To 为 空引用（在 Visual Basic 中为 Nothing）。- 或 -
                // message 为 空引用（在 Visual Basic 中为 Nothing）。
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
                //To、CC 和 BCC 中没有收件人。
            }
            catch (ObjectDisposedException e)
            {
                throw e;
                // 此对象已被释放。
            }
            catch (InvalidOperationException e)
            {
                throw e;
                //此 SmtpClient 有一个 SendAsync 调用正在进行。- 或 - 
                //Host 为 空引用（在 Visual Basic 中为 Nothing）。- 或 -
                //Host 是空字符串 ("")。或者 Port 是零。
            }
            catch (SmtpFailedRecipientsException e)
            {
                throw e;
                //message 无法传递给 To、CC 或 BCC 中的一个或多个收件人。 
            }
            catch (SmtpException e)
            {
                throw e;
                //连接到 SMTP 服务器失败。- 或 -身份验证失败。- 或 -操作超时。
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="Title">主题</param>
        /// <param name="Content">内容</param>
        /// <param name="Emails">收件人</param>
        public void SendEmail(string Title, string Content, string[] Emails)
        {
            this.Subject = Title;
            this.Body = Content;
            GroupSend(Emails);
            //foreach (string str in Emails)
            //{
            //    this.Subject = Title;
            //    this.Body = Content;
            //    this.To = str;
            //    try
            //    {
            //        this.Send();
            //    }
            //    catch
            //    {
            //        return;
            //    }
            //}
        }

    }



}
