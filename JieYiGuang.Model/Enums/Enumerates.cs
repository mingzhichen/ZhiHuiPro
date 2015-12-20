using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JieYiGuang.Model.Enums
{
    /// <summary>
    /// 信息状态
    /// </summary>
    public enum EnumStatus
    {
        /// <summary>
        /// 删除
        /// </summary>
        Delete = -1,
        /// <summary>
        /// 正常
        /// </summary>
        Started = 0,
        /// <summary>
        /// 锁定
        /// </summary>
        Locking = 1,
        /// <summary>
        /// 审核
        /// </summary>
        Checking = 2,
        /// <summary>
        /// 过期
        /// </summary>
        Overdued = 3,
        /// <summary>
        /// 停用
        /// </summary>
        Stopped = 4

    }

    public enum EnumLoginType
    {
        /// <summary>
        /// 用户名
        /// </summary>
        UserName,
        /// <summary>
        /// 邮箱登入
        /// </summary>
        UserEmail,
        /// <summary>
        /// 手机登入
        /// </summary>
        UserMobile,
        /// <summary>
        /// 微信登入
        /// </summary>
        ApiWeiXin,

    }
    /// <summary>
    /// 用户登录状态
    /// </summary>
    public enum EnumLoginState
    {
        /// <summary>
        /// 超时或未登录
        /// </summary>
        Err_TimeOut,
        /// <summary>
        /// 用户被锁定
        /// </summary>
        Err_Locked,
        /// <summary>
        /// 用户编号不存在
        /// </summary>
        Err_UserInexistent,
        /// <summary>
        /// 用户IP被限制
        /// </summary>
        Err_IPLimited,
        /// <summary>
        /// 用户权限不足
        /// </summary>
        Err_NoAuthority,
        /// <summary>
        /// 发生数据库异常
        /// </summary>
        Err_DbException,
        /// <summary>
        /// 登录状态正常
        /// </summary>
        Succeed,
        /// <summary>
        /// 登录状态失败
        /// </summary>
        Fail,
        /// <summary>
        /// 帐号未(通过电子邮件)激活
        /// </summary>
        Err_UnActivation,
        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        Err_NameOrPwdError,
        /// <summary>
        /// 用户名错误
        /// </summary>
        Err_NameError,
        /// <summary>
        /// 密码错误
        /// </summary>
        Err_PwdError,
        /// <summary>
        /// 非管理员
        /// </summary>
        Err_NotAdmin,
        /// <summary>
        /// 连续登录错误锁定
        /// </summary>
        Err_DurativeLogError,
        /// <summary>
        /// 会员组超期
        /// </summary>
        Err_GroupExpire
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum EnumActionType
    {
        /// <summary>
        /// 登录
        /// </summary>
        LOGIN,
        /// <summary>
        /// 退出登录
        /// </summary>
        LOGOFF,

        /// <summary>
        /// 新增
        /// </summary>
        INSERT,
        /// <summary>
        /// 删除
        /// </summary>
        DELETE,
        /// <summary>
        /// 更新
        /// </summary>
        UPDATE
    }

    public enum EnumUserType
    {
        /// <summary>
        /// 设计师
        /// </summary>
        Designer,
        /// <summary>
        /// 企业用户
        /// </summary>
        Enterprise,
        /// <summary>
        /// 一般用户
        /// </summary>
        User

    }
}
