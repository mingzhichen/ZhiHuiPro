using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Xml;
using JieYiGuang.Common.Config;

namespace JieYiGuang.Common.Helper
{
    /// <summary>
    /// IP��ַ�ۺϲ�ѯ������
    /// </summary>
    public class IPHelper : System.Web.UI.Page
    {
        #region ˽�г�Ա
        private string dataPath;
        private string ip;
        private string country;
        private string local;

        private long firstStartIp = 0;
        private long lastStartIp = 0;
        private FileStream objfs = null;
        private long startIp = 0;
        private long endIp = 0;
        private int countryFlag = 0;
        private long endIpOff = 0;
        private string errMsg = null;

        /// <summary>
        /// IP���ݿ�ʵ�ʵ�ַ
        /// </summary>
        private string IpDataPath = ConfigHelper.GetConfigString("IpDBpath");

        #endregion
        
        #region ��������
        /// <summary>
        /// IP��·��
        /// </summary>
        public string DataPath
        {
            set { dataPath = value; }
        }
        /// <summary>
        /// IP��ַ
        /// </summary>
        public string IP
        {
            set { ip = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Country
        {
            get { return country; }
        }
        /// <summary>
        /// �����ַ
        /// </summary>
        public string Local
        {
            get { return local; }
        }
        /// <summary>
        /// ���ش�����Ϣ
        /// </summary>
        public string ErrMsg
        {
            get { return errMsg; }
        }
        #endregion

        #region ��ѯIP�����ַ
       
        /// <summary>
        /// ��ѯIP�����ַ
        /// </summary>
        /// <param name="objString">IP��ַ</param>
        /// <returns></returns>
        public string GetIPDate(string objString)
        {
            try
            {
                this.DataPath = Server.MapPath(IpDataPath);
                this.IP = objString;
                string Address = this.IPLocation().ToString();
                if (Address.IndexOf("CZ88.NET") != -1)
                {
                    return Address.Replace("CZ88.NET", "").Trim();
                }
                else
                {
                    return Address.Trim();
                }

            }
            catch
            {   
                return this.ErrMsg;
            }            
        }
        #endregion

        #region ����ƥ������
        private int QQwry()
        {
            string pattern = @"(((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))\.){3}((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))";
            Regex objRe = new Regex(pattern);
            Match objMa = objRe.Match(ip);
            if (!objMa.Success)
            {
                this.errMsg = "IP��ʽ����";
                return 4;
            }

            long ip_Int = this.IpToInt(ip);
            int nRet = 0;
            if (ip_Int >= IpToInt("127.0.0.0") && ip_Int <= IpToInt("127.255.255.255"))
            {
                this.country = "�����ڲ����ص�ַ";
                this.local = "";
                nRet = 1;
            }
            else if ((ip_Int >= IpToInt("0.0.0.0") && ip_Int <= IpToInt("2.255.255.255")) || (ip_Int >= IpToInt("64.0.0.0") && ip_Int <= IpToInt("126.255.255.255")) || (ip_Int >= IpToInt("58.0.0.0") && ip_Int <= IpToInt("60.255.255.255")))
            {
                this.country = "���籣����ַ";
                this.local = "";
                nRet = 1;
            }
            objfs = new FileStream(this.dataPath, FileMode.Open, FileAccess.Read);
            try
            {
                //objfs.Seek(0,SeekOrigin.Begin);
                objfs.Position = 0;
                byte[] buff = new Byte[8];
                objfs.Read(buff, 0, 8);
                firstStartIp = buff[0] + buff[1] * 256 + buff[2] * 256 * 256 + buff[3] * 256 * 256 * 256;
                lastStartIp = buff[4] * 1 + buff[5] * 256 + buff[6] * 256 * 256 + buff[7] * 256 * 256 * 256;
                long recordCount = Convert.ToInt64((lastStartIp - firstStartIp) / 7.0);
                if (recordCount <= 1)
                {
                    country = "FileDataError";
                    objfs.Close();
                    return 2;
                }
                long rangE = recordCount;
                long rangB = 0;
                long recNO = 0;
                while (rangB < rangE - 1)
                {
                    recNO = (rangE + rangB) / 2;
                    this.GetStartIp(recNO);
                    if (ip_Int == this.startIp)
                    {
                        rangB = recNO;
                        break;
                    }
                    if (ip_Int > this.startIp)
                        rangB = recNO;
                    else
                        rangE = recNO;
                }
                this.GetStartIp(rangB);
                this.GetEndIp();
                if (this.startIp <= ip_Int && this.endIp >= ip_Int)
                {
                    this.GetCountry();
                    this.local = this.local.Replace("������һ��Ҫ���̨�壡������", "");
                }
                else
                {
                    nRet = 3;
                    this.country = "δ֪";
                    this.local = "";
                }
                objfs.Close();
                return nRet;
            }
            catch
            {
                return 1;
            }

        }
        #endregion

        #region IP��ַת����Int����
        private long IpToInt(string ip)
        {
            char[] dot = new char[] { '.' };
            string[] ipArr = ip.Split(dot);
            if (ipArr.Length == 3)
                ip = ip + ".0";
            ipArr = ip.Split(dot);

            long ip_Int = 0;
            long p1 = long.Parse(ipArr[0]) * 256 * 256 * 256;
            long p2 = long.Parse(ipArr[1]) * 256 * 256;
            long p3 = long.Parse(ipArr[2]) * 256;
            long p4 = long.Parse(ipArr[3]);
            ip_Int = p1 + p2 + p3 + p4;
            return ip_Int;
        }
        #endregion

        #region intת����IP
        private string IntToIP(long ip_Int)
        {
            long seg1 = (ip_Int & 0xff000000) >> 24;
            if (seg1 < 0)
                seg1 += 0x100;
            long seg2 = (ip_Int & 0x00ff0000) >> 16;
            if (seg2 < 0)
                seg2 += 0x100;
            long seg3 = (ip_Int & 0x0000ff00) >> 8;
            if (seg3 < 0)
                seg3 += 0x100;
            long seg4 = (ip_Int & 0x000000ff);
            if (seg4 < 0)
                seg4 += 0x100;
            string ip = seg1.ToString() + "." + seg2.ToString() + "." + seg3.ToString() + "." + seg4.ToString();

            return ip;
        }
        #endregion

        #region ��ȡ��ʼIP��Χ
        private long GetStartIp(long recNO)
        {
            long offSet = firstStartIp + recNO * 7;
            //objfs.Seek(offSet,SeekOrigin.Begin);
            objfs.Position = offSet;
            byte[] buff = new Byte[7];
            objfs.Read(buff, 0, 7);

            endIpOff = Convert.ToInt64(buff[4].ToString()) + Convert.ToInt64(buff[5].ToString()) * 256 + Convert.ToInt64(buff[6].ToString()) * 256 * 256;
            startIp = Convert.ToInt64(buff[0].ToString()) + Convert.ToInt64(buff[1].ToString()) * 256 + Convert.ToInt64(buff[2].ToString()) * 256 * 256 + Convert.ToInt64(buff[3].ToString()) * 256 * 256 * 256;
            return startIp;
        }
        #endregion

        #region ��ȡ����IP
        private long GetEndIp()
        {
            //objfs.Seek(endIpOff,SeekOrigin.Begin);
            objfs.Position = endIpOff;
            byte[] buff = new Byte[5];
            objfs.Read(buff, 0, 5);
            this.endIp = Convert.ToInt64(buff[0].ToString()) + Convert.ToInt64(buff[1].ToString()) * 256 + Convert.ToInt64(buff[2].ToString()) * 256 * 256 + Convert.ToInt64(buff[3].ToString()) * 256 * 256 * 256;
            this.countryFlag = buff[4];
            return this.endIp;
        }
        #endregion

        #region ��ȡ����/����ƫ����
        private string GetCountry()
        {
            switch (this.countryFlag)
            {
                case 1:
                case 2:
                    this.country = GetFlagStr(this.endIpOff + 4);
                    this.local = (1 == this.countryFlag) ? " " : this.GetFlagStr(this.endIpOff + 8);
                    break;
                default:
                    this.country = this.GetFlagStr(this.endIpOff + 4);
                    this.local = this.GetFlagStr(objfs.Position);
                    break;
            }
            return " ";
        }
        #endregion

        #region ��ȡ����/�����ַ���
        private string GetFlagStr(long offSet)
        {
            int flag = 0;
            byte[] buff = new Byte[3];
            while (1 == 1)
            {
                //objfs.Seek(offSet,SeekOrigin.Begin);
                objfs.Position = offSet;
                flag = objfs.ReadByte();
                if (flag == 1 || flag == 2)
                {
                    objfs.Read(buff, 0, 3);
                    if (flag == 2)
                    {
                        this.countryFlag = 2;
                        this.endIpOff = offSet - 4;
                    }
                    offSet = Convert.ToInt64(buff[0].ToString()) + Convert.ToInt64(buff[1].ToString()) * 256 + Convert.ToInt64(buff[2].ToString()) * 256 * 256;
                }
                else
                {
                    break;
                }
            }
            if (offSet < 12)
                return " ";
            objfs.Position = offSet;
            return GetStr();
        }
        #endregion

        #region GetStr
        private string GetStr()
        {
            byte lowC = 0;
            byte upC = 0;
            string str = "";
            byte[] buff = new byte[2];
            while (1 == 1)
            {
                lowC = (Byte)objfs.ReadByte();
                if (lowC == 0)
                    break;
                if (lowC > 127)
                {
                    upC = (byte)objfs.ReadByte();
                    buff[0] = lowC;
                    buff[1] = upC;
                    System.Text.Encoding enc = System.Text.Encoding.GetEncoding("GB2312");
                    str += enc.GetString(buff);
                }
                else
                {
                    str += (char)lowC;
                }
            }
            return str;
        }
        #endregion

        #region ��ȡIP��ַ
        /// <summary>
        /// ��ȡIP��ַ
        /// </summary>
        /// <returns></returns>
        public string IPLocation()
        {
            this.QQwry();
            return this.country + this.local;
        }
        /// <summary>
        /// ��ȡIP��ַ
        /// </summary>
        /// <param name="dataPath">���ݿ��ַ</param>
        /// <param name="ip">IP</param>
        /// <returns></returns>
        public string IPLocation(string dataPath, string ip)
        {
            this.dataPath = dataPath;
            this.ip = ip;
            this.QQwry();
            return this.country + this.local;
        }
        #endregion

        #region ��ѯ������ӦIP��ַ


        /// <summary>
        /// ������IPת��
        /// </summary>
        /// <param name="strDomain">����</param>
        /// <returns></returns>
        public string DomainToIp(string strDomain)
        {
            try
            {
                IPHostEntry Host = new IPHostEntry();
                Host = Dns.GetHostEntry(strDomain);
                IPEndPoint IP = new IPEndPoint(Host.AddressList[0], 0);
                return IP.Address.ToString();
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    return "<font color=\"red\">����" + ex.Message + "</font>";
                }
            }
            return "��Ч��ַ";
        }

   
        /// <summary>
        /// ��ѯ������ӦIP��ַ
        /// </summary>
        /// <param name="objString">������ַ</param>
        /// <returns></returns>
        public string GetDomain(string objString)
        {
            try
            {
                IPHostEntry Host = new IPHostEntry();
                Host = Dns.GetHostEntry(objString);
                IPEndPoint IP = new IPEndPoint(Host.AddressList[0], 0);
                return "��Ӧ������IP��" + IP.Address.ToString();
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    return "<font color=\"red\">����" + ex.Message + "</font>";
                }
            }
            return null;
        }
        #endregion

        #region �Ƿ����ӵ�whoise������
        /// <summary>
        /// �Ƿ����ӵ�whoise������
        /// </summary>
        /// <param name="strDomain">���ص�����</param>
        /// <param name="strServer">��������ַ</param>
        /// <param name="strResponse">��������Ӧ��</param>
        /// <returns></returns>
         protected bool IsWhosisSuccess(string strDomain, string strServer, out string strResponse)
         {
            strResponse = "none";
            bool blSuccess = false;
            TcpClient tcpc = new TcpClient();
            try
            {
                tcpc.Connect(strServer, 43);
            }
            catch (SocketException ex)
            {
                if (ex != null)
                {
                    strResponse = "���Ӳ����� Whois server,���Ժ����ԡ�";
                    return false;
                }
            }

            strDomain += "\r\n";
            Byte[] arrDomain = Encoding.UTF8.GetBytes(strDomain.ToCharArray());
            try
            {
                Stream s = tcpc.GetStream();
                s.Write(arrDomain, 0, strDomain.Length);

                StreamReader sr = new StreamReader(tcpc.GetStream(), Encoding.UTF8);
                StringBuilder strBuilder = new StringBuilder();
                string strLine = null;

                while (null != (strLine = sr.ReadLine()))
                {
                    strBuilder.Append(strLine + "\n");
                }
                tcpc.Close();

                blSuccess = true;


                if (strBuilder.ToString().IndexOf("http://www.dns.com.cn") != -1)
                {
                    strResponse = strBuilder.ToString().Replace("http://www.dns.com.cn", "http://Www.copyMM.Com");
                }
                else
                {
                    strResponse = strBuilder.ToString();
                }

            }
            catch (Exception e)
            {
                strResponse = e.ToString();
            }

            return blSuccess;
        }

        /// <summary>
        /// ��ȡWhoise���
        /// </summary>
        /// <param name="TxtWhois"></param>
        /// <returns></returns>
        public string GetWhosieResult(string TxtWhois)
        {
            try
            {
                string strServer;
                string strDomain = TxtWhois.Trim();
                string strServerCom = "whois.internic.net"; //whoise��������ַ
                string strServerCN = "whois.cnnic.net.cn";//whoise��������ַ
                string strResponse;
                string[] arrDomain = strDomain.Split('.');
                if (arrDomain.Length == 2 && arrDomain[1].ToUpper() == "CN" || arrDomain.Length == 3 && arrDomain[2].ToUpper() == "CN")
                {
                    strServer = strServerCN;
                }
                else
                {
                    strServer = strServerCom;
                }

                bool blSuccess = this.IsWhosisSuccess(strDomain, strServer, out strResponse);

                if (blSuccess)
                {
                    return strResponse;
                }
                else
                {
                    return "����ʧ��....��ȷ�������Ƿ�ע��";
                }
            }
            catch
            {

                return "����ʧ��....";

            }
        }
        #endregion

        #region ������ѯ����


        /// <summary>
        /// �ж������Ƿ�ע��
        /// ֧����������
        /// </summary>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public bool IsRegDomain(string domainName)
        {
            bool flag = false;

            string dm = System.Web.HttpUtility.UrlEncode(domainName, System.Text.Encoding.GetEncoding("GB2312"));
            try
            {
                //�жϷ����ǳ��࣬���Զ���ļ��ٴ����ַ����ȵȣ������õķ���Ч�ʲ��Ǻܸ�
                WebClient wc = new WebClient();
                string xmlstr = wc.DownloadString("http://panda.www.net.cn/cgi-bin/check.cgi?area_domain="+dm);  
                StringReader sr = new StringReader(xmlstr);
                XmlTextReader xr = new XmlTextReader(sr);
                while (xr.Read())
                {
                    if (xr.IsStartElement("original"))
                    {
                        xr.Read();
                        if (xr.Value.Substring(0, 3) == "210")
                        {
                            flag = true;
                            break;
                        }
                        else
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                return flag;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Alex��ѯ
        /// <summary>
        /// ����Alex��ѯ��ַ
        /// </summary>
        /// <param name="strUrl">��ѯ��URL</param>
        /// <returns></returns>
        public string GetAlex(string strUrl)
        {
            return "<script type='text/javascript' language='javascript' src='http://xslt.alexa.com/site_stats/js/t/c?url=" + strUrl + "'></script>";
        }
        #endregion

    }
}
