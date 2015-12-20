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
        /// �ʼ�����
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
        /// �ʼ�����
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
        /// �����˵�ַ
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
        /// ����������
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
        /// �ռ���Email
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
        /// ������
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
        /// SMTP��֤ʱʹ�õ��û���
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
        /// SMTP��֤ʱʹ�õ�����
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
