using System;
using System.Net.Mail;

namespace JieYiGuang.Common.Helper
{
    public abstract class MailHelper
    {
        public string _From = "s1@devotop.com";//Ĭ�Ϸ����ʼ�
        public string EmailHost = "smtp.mxhichina.com";//SMTP������
        public string EmailUserName = "s1@devotop.com";//SMTP������
        public string EmailPassWord = "S1devotop";//SMTP������
        public bool EmailEnablessl = false;//SMTP������

        public int EmailPort = 25;


        /// <summary>
        /// �ʼ�����
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// �ʼ�����
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// �ռ���ַ
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// ������,�����ʼ���ַ
        /// </summary>
        public string From
        {
            get { return _From; }
            set { _From = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public abstract void Send();
        /// <summary>
        /// Ⱥ���ʼ�
        /// </summary>
        public abstract void GroupSend(string[] groupTo);
        /// <summary>
        /// ����һ���ʼ�ʵ��
        /// </summary>
        /// <returns></returns>
        public static MailHelper CreateInstance()
        {
            return new SmtpMail();
        }
    }
    /*
    /// <summary>
    /// ��JMail�����ʼ�
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
            //Silent���ԣ��������Ϊtrue,JMail�����׳��������. JMail. Send( () ����ݲ����������true��false
            Jmail.Silent = false;
            //Jmail��������־��ǰ��loging��������Ϊtrue
            Jmail.Logging = true;
            //�ַ�����ȱʡΪ"US-ASCII"
            Jmail.Charset = System.Text.Encoding.Default.BodyName;// "GB2312";
            //�ż���contentype. ȱʡ��"text/plain"�� : �ַ����������HTML��ʽ�����ʼ�, ��Ϊ"text/html"���ɡ�
            Jmail.ContentType = "text/html";
            //����ռ���
            Jmail.AddRecipient(ToEmail, "", "");
            Jmail.From = FromEmail;
            ///Jmail.
            //�������ʼ��û���
            Jmail.MailServerUserName = EmailUserName;
            //�������ʼ�����
            Jmail.MailServerPassWord = EmailPassWord;
            //�����ʼ�����
            Jmail.Subject = Subject;
            //�ʼ���Ӹ���,(�฽���Ļ��������ټ�һ��Jmail.AddAttachment( "c:\\test.jpg",true,null);)�Ϳ��Ը㶨�ˡ���ע�ݣ����˸��������������Jmail.ContentType= "text/html";ɾ������������ʼ���������롣
            //Jmail.AddAttachment("c:\\test.jpg", true, null);
            //�ʼ�����
            Jmail.Body = body;// +t.ToString();
            //Jmail���͵ķ���
            Jmail.Send(EmailHost, false);
            Jmail.Close();
        }
        /// <summary>
        /// Ⱥ��
        /// </summary>
        /// <param name="groupTo"></param>
        public override void GroupSend(string[] groupTo)
        {

        }
    }
    */
    /// <summary>
    /// SMTP�ʼ�
    /// </summary>
    public class SmtpMail : MailHelper
    {
        private SmtpClient _smtpClient;
        public SmtpMail()
        {
            _smtpClient = new SmtpClient();
            _smtpClient.Timeout = 100000;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//ָ�������ʼ����ͷ�ʽ
            _smtpClient.Host = EmailHost;//ָ��SMTP������
            _smtpClient.Port = EmailPort;
            _smtpClient.Credentials = new System.Net.NetworkCredential(EmailUserName, EmailPassWord);//�û���������
            _smtpClient.EnableSsl = EmailEnablessl;
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        public override void Send()
        {
            MailMessage _mailMessage = PreSend();
            _mailMessage.To.Add(new MailAddress(To));
            SendOut(_mailMessage);
        }
        /// <summary>
        /// Ⱥ��
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
        /// ׼��MailMessage��
        /// </summary>
        /// <returns></returns>
        private MailMessage PreSend()
        {
            MailMessage _mailMessage = new MailMessage();
            _mailMessage.From = new MailAddress(this.From);
            _mailMessage.Subject = this.Subject;
            _mailMessage.Body = this.Body;//����
            _mailMessage.BodyEncoding = System.Text.Encoding.UTF8;//���ı���
            _mailMessage.IsBodyHtml = true;//����ΪHTML��ʽ
            _mailMessage.Priority = MailPriority.High;//���ȼ�
            return _mailMessage;
        }
        /// <summary>
        /// ����
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
                //From Ϊ �����ã��� Visual Basic ��Ϊ Nothing����- �� -
                //To Ϊ �����ã��� Visual Basic ��Ϊ Nothing����- �� -
                // message Ϊ �����ã��� Visual Basic ��Ϊ Nothing����
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
                //To��CC �� BCC ��û���ռ��ˡ�
            }
            catch (ObjectDisposedException e)
            {
                throw e;
                // �˶����ѱ��ͷš�
            }
            catch (InvalidOperationException e)
            {
                throw e;
                //�� SmtpClient ��һ�� SendAsync �������ڽ��С�- �� - 
                //Host Ϊ �����ã��� Visual Basic ��Ϊ Nothing����- �� -
                //Host �ǿ��ַ��� ("")������ Port ���㡣
            }
            catch (SmtpFailedRecipientsException e)
            {
                throw e;
                //message �޷����ݸ� To��CC �� BCC �е�һ�������ռ��ˡ� 
            }
            catch (SmtpException e)
            {
                throw e;
                //���ӵ� SMTP ������ʧ�ܡ�- �� -�����֤ʧ�ܡ�- �� -������ʱ��
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Title">����</param>
        /// <param name="Content">����</param>
        /// <param name="Emails">�ռ���</param>
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
