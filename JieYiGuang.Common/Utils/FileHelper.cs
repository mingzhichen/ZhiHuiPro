using System;
using System.IO;
using System.Web;
using Microsoft.Win32;
using System.Collections.Generic;

namespace JieYiGuang.Common
{
    public partial class FileHelper : System.Web.UI.Page
    {
       public FileHelper()
        {
            DelFile = 0;
            FileCount = 0;
        }

        #region 写文件
        /****************************************
         * 函数名称：WriteFile
         * 功能说明：写文件,会覆盖掉以前的内容
         * 参    数：Path:文件路径,Strings:文本内容
         * 调用示列：
         *           string Path = Server.MapPath("Default2.aspx");       
         *           string Strings = "这是我写的内容啊";
         *           JieYiGuang.Common.Control.FileHelper.WriteFile(Path,Strings);
        *****************************************/
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="Strings">文件内容</param>
        public static void WriteFile(string Path, string Strings)
        {
            if (!System.IO.File.Exists(Path))
            {
                System.IO.FileStream f = System.IO.File.Create(Path);
                f.Close();
            }
            //System.IO.StreamWriter f2 = new System.IO.StreamWriter(Path, false, System.Text.Encoding.GetEncoding("gb2312"));
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(Path, false);
            f2.Write(Strings);
            f2.Close();
            f2.Dispose();
        }
        #endregion

        #region 读文件
        /****************************************
         * 函数名称：ReadFile
         * 功能说明：读取文本内容
         * 参    数：Path:文件路径
         * 调用示列：
         *           string Path = Server.MapPath("Default2.aspx");       
         *           string s = JieYiGuang.Common.Control.FileHelper.ReadFile(Path);
        *****************************************/
        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns></returns>
        public static string ReadFile(string Path)
        {
            string s = "";
            if (!System.IO.File.Exists(Path))
                s = "不存在相应的目录";
            else
            {
                StreamReader f2 = new StreamReader(Path, System.Text.Encoding.GetEncoding("gb2312"));
                s = f2.ReadToEnd();
                f2.Close();
                f2.Dispose();
            }

            return s;
        }
        #endregion

        #region 追加文件
        /****************************************
         * 函数名称：FileAdd
         * 功能说明：追加文件内容
         * 参    数：Path:文件路径,strings:内容
         * 调用示列：
         *           string Path = Server.MapPath("Default2.aspx");     
         *           string Strings = "新追加内容";
         *           JieYiGuang.Common.Control.FileHelper.FileAdd(Path, Strings);
        *****************************************/
        /// <summary>
        /// 追加文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="strings">内容</param>
        public static void FileAdd(string Path, string strings)
        {
            StreamWriter sw = File.AppendText(Path);
            sw.Write(strings);
            sw.Flush();
            sw.Close();
        }
        #endregion

        #region 拷贝文件
        /****************************************
         * 函数名称：FileCoppy
         * 功能说明：拷贝文件
         * 参    数：OrignFile:原始文件,NewFile:新文件路径
         * 调用示列：
         *           string OrignFile = Server.MapPath("Default2.aspx");     
         *           string NewFile = Server.MapPath("Default3.aspx");
         *           JieYiGuang.Common.Control.FileHelper.FileCoppy(OrignFile, NewFile);
        *****************************************/
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="OrignFile">原始文件</param>
        /// <param name="NewFile">新文件路径</param>
        public static void FileCoppy(string OrignFile, string NewFile)
        {
            File.Copy(OrignFile, NewFile, true);
        }

        #endregion

        #region 删除文件
        /****************************************
         * 函数名称：FileDel
         * 功能说明：删除文件01
         * 参    数：Path:文件路径
         * 调用示列：
         *           string Path = Server.MapPath("Default3.aspx");    
         *           JieYiGuang.Common.Control.FileHelper.FileDel(Path);
        *****************************************/
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Path">路径</param>
        public static bool FileDel(string Path)
        {
            try
            {
                if (System.IO.File.Exists(Path))
                {
                    System.IO.File.Delete(Path);
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /****************************************
         * 函数名称：FileDel
         * 功能说明：删除文件02
         * 参    数：FilePath:文件路径,PathType:文件路径类型
         * 调用示列：
         *           string Path = Server.MapPath("Default3.aspx");    
         *           JieYiGuang.Common.Control.FileHelper.FileDel(Path);
        *****************************************/
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FilePath">删除的文件路径</param>
        /// <param name="PathType">删除文件路径类型</param>
        /// <returns>成功/失败</returns>
        public static bool DeleteFile(string FilePath, DeleteFilePathType PathType)
        {
            bool rBool = false;
            switch (PathType)
            {
                case DeleteFilePathType.DummyPath:
                    FilePath = HttpContext.Current.Server.MapPath(FilePath);
                    break;
                case DeleteFilePathType.NowDirectoryPath:
                    FilePath = HttpContext.Current.Server.MapPath(FilePath);
                    break;
                case DeleteFilePathType.PhysicsPath:
                    break;
            }
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                rBool = true;
            }
            return rBool;
        }
        /// <summary>
        /// 删除文件路径类型
        /// </summary>
        public enum DeleteFilePathType
        {
            /// <summary>
            /// 物理路径
            /// </summary>
            PhysicsPath = 1,
            /// <summary>
            /// 虚拟路径
            /// </summary>
            DummyPath = 2,
            /// <summary>
            /// 当前目录
            /// </summary>
            NowDirectoryPath = 3
        }

        #endregion

        #region 移动文件
        /****************************************
         * 函数名称：FileMove
         * 功能说明：移动文件
         * 参    数：OrignFile:原始路径,NewFile:新文件路径
         * 调用示列：
         *            string OrignFile = Server.MapPath("../说明.txt");    
         *            string NewFile = Server.MapPath("../../说明.txt");
         *            JieYiGuang.Common.Control.FileHelper.FileMove(OrignFile, NewFile);
        *****************************************/
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="OrignFile">原始路径</param>
        /// <param name="NewFile">新路径</param>
        public static void FileMove(string OrignFile, string NewFile)
        {
            File.Move(OrignFile, NewFile);
        }
        #endregion

        #region 在当前目录下创建目录
        /****************************************
         * 函数名称：FolderCreate
         * 功能说明：在当前目录下创建目录
         * 参    数：OrignFolder:当前目录,NewFloder:新目录
         * 调用示列：
         *           string OrignFolder = Server.MapPath("test/");    
         *           string NewFloder = "new";
         *           JieYiGuang.Common.Control.FileHelper.FolderCreate(OrignFolder, NewFloder); 
        *****************************************/
        /// <summary>
        /// 在当前目录下创建目录
        /// </summary>
        /// <param name="OrignFolder">当前目录</param>
        /// <param name="NewFloder">新目录</param>
        public static void FolderCreate(string OrignFolder, string NewFloder)
        {
            Directory.SetCurrentDirectory(OrignFolder);
            Directory.CreateDirectory(NewFloder);
        }
        #endregion

        #region 递归删除文件夹目录及文件
        /****************************************
         * 函数名称：DeleteFolder
         * 功能说明：递归删除文件夹目录及文件
         * 参    数：dir:文件夹路径
         * 调用示列：
         *           string dir = Server.MapPath("test/");  
         *           JieYiGuang.Common.Control.FileHelper.DeleteFolder(dir);       
        *****************************************/
        /// <summary>
        /// 递归删除文件夹目录及文件
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static int DelFile = 0;
        public static int DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                    {
                        File.Delete(d); //直接删除其中的文件
                        DelFile++;
                    }
                    else
                        DeleteFolder(d); //递归删除子文件夹 
                }
                Directory.Delete(dir); //删除已空文件夹 
            }
            return DelFile;

        }

        #endregion

        #region 递归获得文件夹目录下文件个数
        /****************************************
         * 函数名称：GetPathFileCount
         * 功能说明：递归获得文件夹目录下文件个数
         * 参    数：path:文件夹路径
         * 调用示列：
         *           string dir = Server.MapPath("test/");  
         *           JieYiGuang.Common.Control.FileHelper.GetPathFileCount(dir);       
        *****************************************/
        /// <summary>
        /// 递归获得文件夹目录下文件个数
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static int FileCount = 0;
        public static int GetPathFileCount(string path)
        {
            if (Directory.Exists(path)) //如果存在这个文件夹
            {
                foreach (string d in Directory.GetFileSystemEntries(path))
                {
                    if (File.Exists(d))
                    {
                        FileCount =FileCount+1;
                    }
                    else
                        GetPathFileCount(d); //递归查询子文件夹 
                }
            }
            return FileCount;

        }

        #endregion

        #region 将指定文件夹下面的所有内容copy到目标文件夹下面 如果目标文件夹为只读属性就会报错。
        /****************************************
         * 函数名称：CopyDir
         * 功能说明：将指定文件夹下面的所有内容copy到目标文件夹下面 如果目标文件夹为只读属性就会报错。
         * 参    数：srcPath:原始路径,aimPath:目标文件夹
         * 调用示列：
         *           string srcPath = Server.MapPath("test/");  
         *           string aimPath = Server.MapPath("test1/");
         *           JieYiGuang.Common.Control.FileHelper.CopyDir(srcPath,aimPath);   
        *****************************************/
        /// <summary>
        /// 指定文件夹下面的所有内容copy到目标文件夹下面
        /// </summary>
        /// <param name="srcPath">原始路径</param>
        /// <param name="aimPath">目标文件夹</param>
        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(aimPath))
                    Directory.CreateDirectory(aimPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                //如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                //string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                //遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    //先当作目录处理如果存在这个目录就递归Copy该目录下面的文件

                    if (Directory.Exists(file))
                        CopyDir(file, aimPath + Path.GetFileName(file));
                    //否则直接Copy文件
                    else
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                }

            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }


        #endregion

        #region 服务器上的文件直接下载
        /****************************************
         * 函数名称：DownloadFile
         * 功能说明：服务器上的文件直接下载
         * 参    数：Path:文件路径
         * 调用示列：
         *           string Path = Server.MapPath("Default3.aspx");    
         *           JieYiGuang.Common.Control.FileHelper.DownloadFile(Path);
        *****************************************/
        /// <summary>
        /// 服务器上的文件直接下载
        /// </summary>
        /// <param name="Path">路径</param>
        public static void DownloadFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(file.Name));
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.WriteFile(file.FullName);
                System.Web.HttpContext.Current.Response.End();
            }
        }
        #endregion

        //其他

        #region [返回文件的大小]
        /// <summary>
        /// 返回文件的大小
        /// </summary>
        /// <param name="Size"></param>
        /// <returns></returns>
        public double GetFileSizeK(int Size)
        {
            return System.Math.Round(Convert.ToDouble(Size) / 1024, 2);
        }
        #endregion

        #region "根据文件扩展名获得文件的content-type"
        /// <summary>
        /// 根据文件扩展名获得文件的content-type
        /// </summary>
        /// <param name="fileextension">文件扩展名如.gif</param>
        /// <returns>文件对应的content-type 如:application/gif</returns>
        public static string GetFileMIME(string fileextension)
        {
            //set the default content-type
            const string DEFAULT_CONTENT_TYPE = "application/unknown";

            RegistryKey regkey, fileextkey;
            string filecontenttype;

            //the file extension to lookup


            try
            {
                //look in HKCR
                regkey = Registry.ClassesRoot;

                //look for extension
                fileextkey = regkey.OpenSubKey(fileextension);

                //retrieve Content Type value
                filecontenttype = fileextkey.GetValue("Content Type", DEFAULT_CONTENT_TYPE).ToString();

                //cleanup
                fileextkey = null;
                regkey = null;
            }
            catch
            {
                filecontenttype = DEFAULT_CONTENT_TYPE;
            }

            return filecontenttype;
        }
        #endregion

        #region 取得文件后缀名
        /****************************************
         * 函数名称：GetPostfixStr
         * 功能说明：取得文件后缀名
         * 参    数：filename:文件名称
         * 调用示列：
         *           string filename = "aaa.aspx";        
         *           string s = JieYiGuang.Common.Control.FileHelper.GetPostfixStr(filename);         
        *****************************************/
        /// <summary>
        /// 取后缀名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>.gif|.html格式</returns>
        public static string GetPostfixStr(string filename)
        {
            int start = filename.LastIndexOf(".");
            int length = filename.Length;
            string postfix = filename.Substring(start, length - start);
            return postfix;
        }
        #endregion

        #region "根据文件扩展名获取当前目录下的文件列表"
        /// <summary>
        /// 根据文件扩展名获取当前目录下的文件列表
        /// </summary>
        /// <param name="FileExt">文件扩展名</param>
        /// <returns>返回文件列表</returns>
        public static List<string> GetDirFileList(string FileExt)
        {
            List<string> FilesList = new List<string>();
            string[] Files = Directory.GetFiles(GetScriptPath, string.Format("*.{0}", FileExt));
            foreach (string var in Files)
            {
                FilesList.Add(System.IO.Path.GetFileName(var).ToLower());
            }
            return FilesList;
        }
        /// <summary>
        /// 获取当前访问文件物理目录
        /// </summary>
        /// <returns>路径</returns>
        public static string GetScriptPath
        {
            get
            {
                string Paths = HttpContext.Current.Request.ServerVariables["PATH_TRANSLATED"].ToString();
                return Paths.Remove(Paths.LastIndexOf("\\"));
            }
        }
        #endregion

        #region "在当前路径上创建日期格式目录(20060205)"
        /// <summary>
        /// 在当前路径上创建日期格式目录(20060205)
        /// </summary>
        /// <param name="sPath">返回目录名</param>
        /// <returns></returns>
        public static string CreateDir(string sPath)
        {
            string sTemp = System.DateTime.Today.Year.ToString() + System.DateTime.Today.Month.ToString("00") + System.DateTime.Today.Day.ToString("00");
            sPath += sTemp;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@sPath); //构造函数创建目录
            if (di.Exists == false)
            {
                di.Create();
            }

            return sTemp;
        }
        #endregion

        #region [生成日期目录]
        /// <summary>
        /// 生成日期目录
        /// </summary>
        /// <param name="module"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string GetDateOfFolder(bool IsDate, string folder)
        {
            string currentFolder = "";
            string defaultPath = "~/UploadFiles/";
            string LocalPath = HttpContext.Current.Server.MapPath(defaultPath);
            string year = DateTime.Today.Year.ToString();
            string month = DateTime.Today.Month.ToString();

            if (!string.IsNullOrEmpty(folder))
            {
                currentFolder = "/" + folder.Trim();
            }
            if (IsDate)
            {
                currentFolder = currentFolder + "/" + year + "/" + month;
            }
            if (!Directory.Exists(LocalPath + currentFolder))
            {
                Directory.CreateDirectory(LocalPath + currentFolder);
            }
            return (defaultPath + currentFolder + "/").ToString().Replace("////", "/").Replace("///", "/").Replace("//", "/").Replace("//", "/");
        }
        #endregion


    }
}
