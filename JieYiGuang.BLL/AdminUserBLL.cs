using System;
using System.Data;
using System.Collections.Generic;
using JieYiGuang.Common;
using JieYiGuang.Dto;

namespace JieYiGuang.BLL
{
    /// <summary>
    /// 管理员信息表
    /// </summary>
    public partial class AdminUserBLL
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.AdminUserDAL dal;
        public AdminUserBLL()
        {
            dal = new DAL.AdminUserDAL(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法=============================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            return dal.Exists(user_name);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.T_AdminUser model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.T_AdminUser model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.T_AdminUser GetModel(int id)
        {
            return dal.GetModel(id);
        }
     
        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.T_AdminUser GetModel(string user_name, string password)
        {
            return dal.GetModel(user_name, password);
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.T_AdminUser GetModel(string user_name, string password, bool is_encrypt)
        {
            //检查一下是否需要加密
            if (is_encrypt)
            {
                //先取得该用户的随机密钥
                //string salt = dal.GetSalt(user_name);
                //if (string.IsNullOrEmpty(salt))
                //{
                //    return null;
                //}
                //把明文进行加密重新赋值
                password = DESEncrypt.Encrypt(password, "");
            }
            return dal.GetModel(user_name, password);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion


        //--------------------后台管理操作------------------------------------------------

        #region ManageListPage 【资讯后台管理查询】
        public List<Model.T_AdminUser> ManageListPage(QueryParams QueryParams, int PageSize, int PageIndex, out int rowCount)
        {
            int count = 0;
            List<Model.T_AdminUser> List = null;

            List = dal.ManageListPage(QueryParams, PageSize, PageIndex, out count);
            rowCount = count;

            return List;
        }
        #endregion
    }
}