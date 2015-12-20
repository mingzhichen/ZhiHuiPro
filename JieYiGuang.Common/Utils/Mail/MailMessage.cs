using System;
using System.Collections.Generic;
using System.Text;

namespace JieYiGuang.Common.Utils.Mail
{
    public class MailMessage
    {
        private string _subject;
        private string _body;
        private string _from;
        private string _fromName;
        private string _recipientEmail;
        private string _mailserver;
        private string _username;
        private string _password;


        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject
        {
            get
            {
                return this._subject;
            }
            set
            {
                this._subject = value;
            }
        }

        /// <summary>
        /// 邮件正文
        /// </summary>
        public string Body
        {
            get
            {
                return this._body;
            }
            set
            {
                this._body = value;
            }
        }


        /// <summary>
        /// 发件人地址
        /// </summary>
        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                this._from = value;
            }
        }


        /// <summary>
        /// 发件人姓名
        /// </summary>
        public string FromName
        {
            get
            {
                return this._fromName;
            }
            set
            {
                this._fromName = value;
            }
        }


        /// <summary>
        /// 收件人Email
        /// </summary>
        public string RecipientEmail
        {
            get
            {
                return this._recipientEmail;
            }
            set
            {
                this._recipientEmail = value;
            }
        }

        /// <summary>
        /// 邮箱域
        /// </summary>
        public string MailServer
        {
            get
            {
                return this._mailserver;
            }
            set
            {
                this._mailserver = value;
            }
        }


        /// <summary>
        /// SMTP认证时使用的用户名
        /// </summary>
        public string MailServerUserName
        {
            set
            {
                if (value.Trim() != "")
                {
                    this._username = value.Trim();
                }
                else
                {
                    this._username = "";
                }
            }
            get
            {
                return _username;
            }
        }

        /// <summary>
        /// SMTP认证时使用的密码
        /// </summary>
        public string MailServerPassWord
        {
            set
            {
                this._password = value;
            }
            get
            {
                return _password;
            }
        }
    }

}
