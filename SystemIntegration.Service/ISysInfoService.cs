﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Service.Dtos;
using SystemIntegration.Models;
using SystemIntegration.Service.ViewModels;

namespace SystemIntegration.Service
{
    public interface ISysInfoService
    {
        /// <summary>
        /// 返回用户绑定的系统信息
        /// </summary>
        /// <param name="userNum"></param>
        /// <returns></returns>
        List<VSysInfoList> GetUserSysInfoList(string userNum,string type);

        /// <summary>
        /// 返回所以系统的信息，同时显示用户已经绑定系统的状态
        /// </summary>
        /// <param name="userNum"></param>
        /// <returns></returns>
        List<VSysInfoList> GetUserAllSysInfoList(string userNum,string type);

        /// <summary>
        /// 获取全部系统的分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        VPageBootstrapTable<SysInfo> GetSysInfoPage(VSysInfoListCondition input);

        /// <summary>
        /// 获取全部、用户绑定系统数量
        /// </summary>
        /// <param name="userNum"></param>
        /// <returns></returns>
        VSysCount GetSysCount(string userNum);

        /// <summary>
        /// 添加用户绑定的系统
        /// </summary>
        /// <param name="sysInfo"></param>
        /// <returns></returns>
        string AddUserSys(string userNum, string loginName, string loginPwd,int sysID);

        /// <summary>
        /// 判断该系统的用户名、密码是否正确
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <param name="loginType">用户的类型：管理员或者普通用户。如果是内部判断权限则为空</param>
        /// <param name="loginUrl">要验证用户名密码的系统地址</param>
        /// <returns></returns>
        VLoginCheckPwd CheckSysLogin(string loginName, string loginPwd, int sysID);

        /// <summary>
        /// 绑定用户系统
        /// </summary>
        /// <param name="userNum"></param>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <param name="sysID"></param>
        /// <returns></returns>
        string BindUserSys(string userNum,string userIP,string userName, string loginName, string loginPwd, int sysID);

        /// <summary>
        /// 获取单个系统信息
        /// </summary>
        /// <param name="sysID"></param>
        /// <returns></returns>
        SysInfo GetSysInfo(int sysID);

        /// <summary>
        /// 添加一个系统信息
        /// </summary>
        /// <param name="sysInfo"></param>
        /// <returns></returns>
        string InsertSysInfo(VSysInfo sysInfo);

        /// <summary>
        /// 更新一个系统的信息
        /// </summary>
        /// <param name="sysInfo"></param>
        /// <returns></returns>
        string UpdateSysInfo(VSysInfo sysInfo);

        /// <summary>
        /// 返回dbError，数据库错误。返回userOrPwdError，用户名密码错误。正确返回访问地址。
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="userNum"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        string RedirectToSys(int sysID, string userNum,string userName, string ip);

        /// <summary>
        /// 用户系统解除绑定,成功返回“ok”,失败返回“error”
        /// </summary>
        /// <returns></returns>
        string RemoveUserSys(int sysID, string userNum,string userName, string ip);
    }
}
