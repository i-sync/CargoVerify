using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using BLL.Service;
using System.Xml.Serialization;
using System.IO;
using System.Net;

namespace BLL
{
    public class Common
    {
        private static string serviceURL;//服务器URL地址
        private static Common instance;
        private static object obj = new object();
        private LBEBusinessWebService service;

        //储存用户信息/及会话ID
        private userInfo userInfo;
        private string sessionId;

        private Common()
        {
            //初始化WEB服务
            service = new LBEBusinessWebService();
            service.Url = serviceURL;
        }
        /// <summary>
        /// 设置服务器URL地址
        /// </summary>
        public static string ServiceURL
        {
            set { serviceURL = value; }
        }

        /// <summary>
        /// 获取sessionId
        /// </summary>
        public string SessionId
        {
            get { return sessionId; }
        }

        /// <summary>
        /// 管理员用户名
        /// </summary>
        public static string ADMINUID
        {
            set;
            get;
        }
        /// <summary>
        /// 管理员密码
        /// </summary>
        public static string ADMINPWD
        {
            get;
            set;
        }
        /// <summary>
        /// 方案名
        /// </summary>
        public static string SCHEME
        {
            get;
            set;
        }

        //获取实例 
        public static Common Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (obj)
                    {
                        if (instance == null)
                            instance = new Common();
                    }
                }
                return instance;
            }
        }

        public LBEBusinessWebService Service
        {
            get
            {
                return service;
            }
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        public userInfo UserInfo
        {
            get { return userInfo; }
        }


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool Login(string userId, string password, out string errMsg)
        {
            errMsg = string.Empty;

            //登录前初始化用户信息
            userInfo = new userInfo();

            bool flag = false;
            flag = adminLogin(out errMsg);
            if (!flag)
            {
                return flag;
            }
            //参数数组
            lbParameter[] parms = 
            {
                new lbParameter(){ name="userid",value=userId},
                new lbParameter(){name="password",value = password}
            };
            //判断用户名及密码是否正确 1 代表成功，2代表失败 
            bizProcessResult result = execBizProcess("tUser_M2", "", parms, null);
            //bizProcessResult result = Soap.Instance.execBizProcess("tUser_M2", "", parms, null, out errMsg);

            if (result.result == 1) //(result.result > 0)
            {
                return GetUserInfo(userId, out errMsg);
            }
            else
            {
                errMsg = result.message;
                return false;
            }
        }

        /// <summary>
        /// 首先登录管理账户，然后使用管理账户再去操作其它动作。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool adminLogin(out string errMsg)
        {
            errMsg = string.Empty;
            bool flag = false;
            //loginResult result = service.login(ADMINUID, ADMINPWD, SCHEME, null, null); //login(userId, password, out errMsg);
            loginResult result = Soap.Instance.login(ADMINUID, ADMINPWD, SCHEME, out errMsg);
            if (result.result > 0) //(result.result > 0)
            {
                sessionId = result.sessionId;//储存会话ID
                flag = true;
            }
            else
            {
                errMsg = "管理员登录失败：" + result.message;
            }
            return flag;
        }


        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool Logout(out string errMsg)
        {
            errMsg = string.Empty;
            lbeResult result = service.logout(sessionId);
            if (result.result > 0)
            {
                sessionId = string.Empty;
                return true;
            }
            else
            {
                errMsg = result.message;
                return false;
            }
        }

        /// <summary>
        /// 登录成功后，查询用户信息
        /// </summary>
        /// <returns></returns>
        public bool GetUserInfo(string userId, out string errMsg)
        {
            errMsg = string.Empty;
            queryOption queryOption = new queryOption { batchNo = 1, batchSize = 10 };
            queryResult queryResult = query("tUser", null, string.Format("UserID='{0}'", userId), queryOption);
            //queryResult queryResult = Soap.Instance.query("tUser", null, string.Format("UserID='{0}'", userId), queryOption, out errMsg);
            if (queryResult == null) return false;
            if (queryResult.result > 0)
            {
                //int i = queryResult.count;
                userInfo.id = Convert.ToInt32(queryResult.records[0].values[0]);
                userInfo.name = queryResult.records[0].values[3].ToString();
                userInfo.grade = Convert.ToInt32(queryResult.records[0].values[4]);
                userInfo.lastLogin = queryResult.records[0].values[5].ToString();
                userInfo.status = Convert.ToInt32(queryResult.records[0].values[9]);
                //userInfo.orgId = Convert.ToInt64(queryResult.records[0].values[14]);

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="objectName">查询的对象名</param>
        /// <param name="parms"> 传入的参数集合(当要查询的对象为 查询对象,带参数的视图 等要传入参数, 实体对象则传入空值) </param>
        /// <param name="condition">查询的附加条件(查询对象无效),如：“ID=1000” 等 </param>
        /// <param name="queryOption">查询选项</param>
        /// <returns></returns>
        public queryResult query(string objectName, lbParameter[] parms, string condition, queryOption queryOption)
        {
            return service.query(sessionId, objectName, parms, condition, queryOption);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="objectName">查询的对象名</param>
        /// <param name="id">要修改的记录ID</param>
        /// <param name="parms"> 传入的数据集合 </param>
        /// <returns></returns>
        public lbeResult update(string objectName, string id, lbParameter[] parms)
        {
            return service.update(sessionId, objectName, id, parms);
        }
        /// <summary>
        /// 执行对象流程
        /// </summary>
        /// <param name="bizProcessName">执行对象名</param>
        /// <param name="parms">参数</param>
        /// <param name="variables"></param>
        /// <returns></returns>
        public bizProcessResult execBizProcess(string bizProcessName, string id, lbParameter[] parms, lbParameter[] variables)
        {
            return service.execBizProcess(sessionId, bizProcessName, id, parms, variables);
        }
    }
}
