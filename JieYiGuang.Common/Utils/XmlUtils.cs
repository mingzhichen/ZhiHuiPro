using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;

namespace JieYiGuang.Common.Helper
{
    public class XmlUtils : XmlDocument
    {
        protected string StrXmlFile; //文件绝对路径
        protected XmlDocument ObjXmlDoc = new XmlDocument();


        //把接收到的XML转为字典
        public static Dictionary<string, string> GetXmlModel(string xmlStr)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            Dictionary<string, string> mo = new Dictionary<string, string>();
            var data = doc.DocumentElement.ChildNodes;
            for (int i = 0; i < data.Count; i++)
            {
                mo.Add(data.Item(i).LocalName, data.Item(i).InnerText);
            }
            return mo;
        }



        ////从字典中读取指定的值
        public static string ReadModel(string key, Dictionary<string, string> model)
        {
            string str = "";
            model.TryGetValue(key, out str);
            if (str== null)
                str = "";
            return str;
        }



        /// <summary>
        /// 通过反射给接收消息model赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T GetModel<T>(T model, Dictionary<string, string> xmlModel) where T : class
        {
            var m = model.GetType();
            foreach (PropertyInfo p in m.GetProperties())
            {
                string name = p.Name;
                if (xmlModel.Keys.Contains(name))
                {
                    string value=xmlModel.Where(x => x.Key == name).FirstOrDefault().Value;
                    p.SetValue(model,
                    string.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, p.PropertyType), null); 
                }
            }
            return model;
        }

        /// <summary>
        /// 对象实例转成xml
        /// </summary>
        /// <param name="model">对象实例</param>
        /// <returns></returns>
        public static string EntityToXml<T>(T model)
        {
            IList<T> items = new List<T>();
            items.Add(model);
            return EntityToXml<T>(items);
        }

        /// <summary>
        /// 对象实例集转成xml
        /// </summary>
        /// <param name="items">对象实例集</param>
        /// <returns></returns>
        public static string EntityToXml<T>(IList<T> items)
        {
            //创建XmlDocument文档
            XmlDocument doc = new XmlDocument();
            //创建根元素
            XmlElement root = doc.CreateElement(typeof(T).Name + "s");
            //添加根元素的子元素集
            foreach (T item in items)
            {
                EntityToXml<T>(doc, root, item);
            }
            //向XmlDocument文档添加根元素
            doc.AppendChild(root);

            return doc.InnerXml;
        }
        private static void EntityToXml<T>(XmlDocument doc, XmlElement root, T item)
        {
            //创建元素
            XmlElement xmlItem = doc.CreateElement(typeof(T).Name);
            //对象的属性集

            System.Reflection.PropertyInfo[] propertyInfo =
            typeof(T).GetProperties(System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance);



            foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
            {
                if (pinfo != null)
                {
                    //对象属性名称
                    string name = pinfo.Name;
                    //对象属性值
                    string value = String.Empty;

                    if (pinfo.GetValue(item, null) != null)
                        value = pinfo.GetValue(item, null).ToString();//获取对象属性值
                    //设置元素的属性值
                    xmlItem.SetAttribute(name, value);
                }
            }
            //向根添加子元素
            root.AppendChild(xmlItem);
        }

        #region 功能：装载xml文件　方法：XmlControl(string XmlFile)
        /// <summary>
        /// 装载xml文件 例：
        /// </summary>
        /// <param name="XmlFile">xml文件 面对路径</param>
        public void XmlHelper(string XmlFile)
        {

            if (System.IO.File.Exists(XmlFile))
            {
                ObjXmlDoc.Load(XmlFile);
            }
            else
            {
                throw new Exception("文件: " + XmlFile + " 不存在!");
            }
            StrXmlFile = XmlFile;
        }

        #endregion

        #region 获取所有指定名称的节点(XmlNodeList)
        /// <summary>
        /// 获取所有指定名称的节点(XmlNodeList)
        /// </summary>
        /// <param name="XmlPathNode">节点名称</param>
        /// <returns></returns>
        public XmlNodeList GetXmlNodes(string XmlPathNode)
        {
            XmlNodeList strReturn = null;
            try
            {
                strReturn = ObjXmlDoc.SelectNodes(XmlPathNode);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return strReturn;
        }
        #endregion

        /// <summary>
        /// 在指定的Xml元素下,添加子Xml元素，同时带属性
        /// </summary>
        /// <param name="xmlElement"></param>
        /// <param name="xValue"></param>
        /// <param name="attrName"></param>
        /// <param name="attrValue"></param>
        /// <param name="IsCDataSection"></param>
        /// <returns></returns>
        public bool AddChildWhitAttributes(ref XmlElement xmlElement, string xValue, string attrName, string attrValue)
        {
            return AddChildWhitAttributes(ref xmlElement, xValue, attrName, attrValue, false);
        }

        /// <summary>
        /// 在指定的Xml元素下,添加子Xml元素，同时带属性
        /// </summary>
        /// <param name="xmlElement"></param>
        /// <param name="xValue"></param>
        /// <param name="attrName"></param>
        /// <param name="attrValue"></param>
        /// <param name="IsCDataSection"></param>
        /// <returns></returns>
        public bool AddChildWhitAttributes(ref XmlElement xmlElement, string xValue, string attrName, string attrValue, bool IsCDataSection)
        {
            if ((xmlElement != null) && (xmlElement.OwnerDocument != null))
            {
                if (IsCDataSection)
                {
                    XmlCDataSection tempdata = xmlElement.OwnerDocument.CreateCDataSection(attrName);
                    tempdata.InnerText = xValue;
                    xmlElement.AppendChild(tempdata);

                    XmlAttribute xa = xmlElement.OwnerDocument.CreateAttribute(attrName);
                    xa.Value = attrValue;
                    xmlElement.Attributes.Append(xa);
                }
                else
                {
                    XmlAttribute xa = xmlElement.OwnerDocument.CreateAttribute(attrName);
                    xa.Value = attrValue;
                    xmlElement.Attributes.Append(xa);
                    xmlElement.InnerText = xValue;
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 在指定的Xml元素下,添加子Xml元素
        /// </summary>
        /// <param name="xmlElement">被追加子元素的Xml元素</param>
        /// <param name="childElementName">要添加的Xml元素名称</param>
        /// <param name="childElementValue">要添加的Xml元素值</param>
        /// <returns></returns>
        public bool AppendChildElementByNameValue(ref XmlElement xmlElement, string childElementName, object childElementValue)
        {
            return AppendChildElementByNameValue(ref xmlElement, childElementName, childElementValue, false);
        }


        /// <summary>
        /// 在指定的Xml元素下,添加子Xml元素
        /// </summary>
        /// <param name="xmlElement">被追加子元素的Xml元素</param>
        /// <param name="childElementName">要添加的Xml元素名称</param>
        /// <param name="childElementValue">要添加的Xml元素值</param>
        /// <param name="IsCDataSection">是否是CDataSection类型的子元素</param>
        /// <returns></returns>
        public bool AppendChildElementByNameValue(ref XmlElement xmlElement, string childElementName, object childElementValue, bool IsCDataSection)
        {
            if ((xmlElement != null) && (xmlElement.OwnerDocument != null))
            {
                //是否是CData类型Xml元素
                if (IsCDataSection)
                {
                    XmlCDataSection tempdata = xmlElement.OwnerDocument.CreateCDataSection(childElementName);
                    tempdata.InnerText = FiltrateControlCharacter(childElementValue.ToString());
                    XmlElement childXmlElement = xmlElement.OwnerDocument.CreateElement(childElementName);
                    childXmlElement.AppendChild(tempdata);
                    xmlElement.AppendChild(childXmlElement);
                }
                else
                {
                    XmlElement childXmlElement = xmlElement.OwnerDocument.CreateElement(childElementName);
                    childXmlElement.InnerText = FiltrateControlCharacter(childElementValue.ToString());
                    xmlElement.AppendChild(childXmlElement);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 在指定的Xml结点下,添加子Xml元素
        /// </summary>
        /// <param name="xmlElement">被追加子元素的Xml节点</param>
        /// <param name="childElementName">要添加的Xml元素名称</param>
        /// <param name="childElementValue">要添加的Xml元素值</param>
        /// <returns></returns>
        public bool AppendChildElementByNameValue(ref XmlNode xmlNode, string childElementName, object childElementValue)
        {
            return AppendChildElementByNameValue(ref xmlNode, childElementName, childElementValue, false);
        }


        /// <summary>
        /// 在指定的Xml结点下,添加子Xml元素
        /// </summary>
        /// <param name="xmlElement">被追加子元素的Xml节点</param>
        /// <param name="childElementName">要添加的Xml元素名称</param>
        /// <param name="childElementValue">要添加的Xml元素值</param>
        /// <param name="IsCDataSection">是否是CDataSection类型的子元素</param>
        /// <returns></returns>
        public bool AppendChildElementByNameValue(ref XmlNode xmlNode, string childElementName, object childElementValue, bool IsCDataSection)
        {
            if ((xmlNode != null) && (xmlNode.OwnerDocument != null))
            {
                //是否是CData类型Xml结点
                if (IsCDataSection)
                {
                    XmlCDataSection tempdata = xmlNode.OwnerDocument.CreateCDataSection(childElementName);
                    tempdata.InnerText = FiltrateControlCharacter(childElementValue.ToString());
                    XmlElement childXmlElement = xmlNode.OwnerDocument.CreateElement(childElementName);
                    childXmlElement.AppendChild(tempdata);
                    xmlNode.AppendChild(childXmlElement);
                }
                else
                {
                    XmlElement childXmlElement = xmlNode.OwnerDocument.CreateElement(childElementName);
                    childXmlElement.InnerText = FiltrateControlCharacter(childElementValue.ToString());
                    xmlNode.AppendChild(childXmlElement);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 通过数据行向当前XML元素下追加子元素
        /// </summary>
        /// <param name="xmlElement">被追加子元素的Xml元素</param>
        /// <param name="dcc">当前数据表中的列集合</param>
        /// <param name="dr">当前行数据</param>
        /// <returns></returns>
        public bool AppendChildElementByDataRow(ref XmlElement xmlElement, DataColumnCollection dcc, DataRow dr)
        {
            return AppendChildElementByDataRow(ref xmlElement, dcc, dr, null);
        }

        /// <summary>
        /// 通过数据行向当前XML元素下追加子元素
        /// </summary>
        /// <param name="xmlElement">被追加子元素的Xml元素</param>
        /// <param name="dcc">当前数据表中的列集合</param>
        /// <param name="dr">当前行数据</param>
        /// <param name="removecols">不会被追加的列名</param>
        /// <returns></returns>
        public bool AppendChildElementByDataRow(ref XmlElement xmlElement, DataColumnCollection dcc, DataRow dr, string removecols)
        {
            if ((xmlElement != null) && (xmlElement.OwnerDocument != null))
            {
                foreach (DataColumn dc in dcc)
                {
                    if ((removecols == null) ||
                        (removecols == "") ||
                        (("," + removecols + ",").ToLower().IndexOf("," + dc.Caption.ToLower() + ",") < 0))
                    {
                        XmlElement tempElement = xmlElement.OwnerDocument.CreateElement(dc.Caption);
                        tempElement.InnerText = FiltrateControlCharacter(dr[dc.Caption].ToString().Trim());
                        xmlElement.AppendChild(tempElement);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 实始化节点, 当节点存在则清除当前路径下的所有子结点, 如不存在则直接创建该结点
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <returns></returns>
        public XmlNode InitializeNode(string xmlpath)
        {
            XmlNode xmlNode = this.SelectSingleNode(xmlpath);
            if (xmlNode != null)
            {
                xmlNode.RemoveAll();
            }
            else
            {
                xmlNode = CreateNode(xmlpath);
            }
            return xmlNode;
        }


        /// <summary>
        /// 删除指定路径下面的所有子结点和自身
        /// </summary>
        /// <param name="xmlpath">指定路径</param>
        public void RemoveNodeAndChildNode(string xmlpath)
        {
            XmlNodeList xmlNodeList = this.SelectNodes(xmlpath);
            if (xmlNodeList.Count > 0)
            {
                foreach (XmlNode xn in xmlNodeList)
                {
                    xn.RemoveAll();
                    xn.ParentNode.RemoveChild(xn);
                }
            }
        }

        /// <summary>
        /// 创建指定路径下的节点
        /// </summary>
        /// <param name="xmlpath">节点路径</param>
        /// <returns></returns>
        public XmlNode CreateNode(string xmlpath)
        {

            string[] xpathArray = xmlpath.Split('/');
            string root = "";
            XmlNode parentNode = this;
            //建立相关节点
            for (int i = 1; i < xpathArray.Length; i++)
            {
                XmlNode node = this.SelectSingleNode(root + "/" + xpathArray[i]);
                // 如果当前路径不存在则建立,否则设置当前路径到它的子路径上
                if (node == null)
                {
                    XmlElement newElement = this.CreateElement(xpathArray[i]);
                    parentNode.AppendChild(newElement);
                }
                //设置低一级的路径
                root = root + "/" + xpathArray[i];
                parentNode = this.SelectSingleNode(root);
            }
            return parentNode;
        }

        /// <summary>
        /// 得到指定路径的节点值
        /// </summary>
        /// <param name="xmlnode">要查找节点</param>
        /// <param name="path">指定路径</param>
        /// <returns></returns>
        public string GetSingleNodeValue(XmlNode xmlnode, string path)
        {
            if (xmlnode == null)
            {
                return null;
            }

            if (xmlnode.SelectSingleNode(path) != null)
            {
                if (xmlnode.SelectSingleNode(path).LastChild != null)
                {
                    return xmlnode.SelectSingleNode(path).LastChild.Value;
                }
                else
                {
                    return "";
                }
            }
            return null;
        }

        /// <summary>
        /// 查找指定路径是否存在
        /// </summary>
        /// <param name="xmlnode">要查找节点</param>
        /// <param name="path">指定路径</param>
        /// <returns></returns>
        public bool IsNodeExit(XmlNode xmlnode, string path)
        {
            bool isfind = true;
            if (xmlnode == null)
            {
                isfind = false;
            }
            else
            {
                if (xmlnode.SelectSingleNode(path) != null)
                {
                    isfind = true;
                }
                else
                {
                    isfind = false;
                }
            }
            return isfind;
        }

        /// <summary>
        /// 过滤控制字符,包括0x00 - 0x08,0x0b - 0x0c,0x0e - 0x1f
        /// </summary>
        /// <param name="content">要过滤的内容</param>
        /// <returns>过滤后的内容</returns>
        private string FiltrateControlCharacter(string content)
        {
            return Regex.Replace(content, "[\x00-\x08|\x0b-\x0c|\x0e-\x1f]", "");
        }

    }



}

//前台调用方法
//StringBuilder StrMenu = new StringBuilder();
//string XmlFile=System.Web.HttpContext.Current.Server.MapPath("~/Config/AdminMenu/ManageMenu.config");
//XmlHelper XmlTool = new XmlHelper(XmlFile);
//XmlNodeList XmlMenu = XmlTool.GetXmlNodes("/AdminMenu/mainmenu[@manageid='1'][@show='1']");
//foreach (XmlNode xn in XmlMenu)
//{
//    StrMenu.Append(string.Format("\n\t\t\t<li id=\"man_nav_{0}\"><a>{1}</a><span class=\"lt\"></span><span class=\"rt\"></span></li>", xn["menuid"].InnerText, xn["menutitle"].InnerText));                
//}
//ViewData["StrMenu"] = StrMenu.ToString();