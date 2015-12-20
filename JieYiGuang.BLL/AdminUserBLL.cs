using System;
using System.Data;
using System.Collections.Generic;
using JieYiGuang.Common;
using JieYiGuang.Dto;

namespace JieYiGuang.BLL
{
    /// <summary>
    /// ����Ա��Ϣ��
    /// </summary>
    public partial class AdminUserBLL
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //���վ��������Ϣ
        private readonly DAL.AdminUserDAL dal;
        public AdminUserBLL()
        {
            dal = new DAL.AdminUserDAL(siteConfig.sysdatabaseprefix);
        }

        #region ��������=============================
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// ��ѯ�û����Ƿ����
        /// </summary>
        public bool Exists(string user_name)
        {
            return dal.Exists(user_name);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.T_AdminUser model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Model.T_AdminUser model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.T_AdminUser GetModel(int id)
        {
            return dal.GetModel(id);
        }
     
        /// <summary>
        /// �����û������뷵��һ��ʵ��
        /// </summary>
        public Model.T_AdminUser GetModel(string user_name, string password)
        {
            return dal.GetModel(user_name, password);
        }

        /// <summary>
        /// �����û������뷵��һ��ʵ��
        /// </summary>
        public Model.T_AdminUser GetModel(string user_name, string password, bool is_encrypt)
        {
            //���һ���Ƿ���Ҫ����
            if (is_encrypt)
            {
                //��ȡ�ø��û��������Կ
                //string salt = dal.GetSalt(user_name);
                //if (string.IsNullOrEmpty(salt))
                //{
                //    return null;
                //}
                //�����Ľ��м������¸�ֵ
                password = DESEncrypt.Encrypt(password, "");
            }
            return dal.GetModel(user_name, password);
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion


        //--------------------��̨�������------------------------------------------------

        #region ManageListPage ����Ѷ��̨�����ѯ��
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