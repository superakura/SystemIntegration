using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Repository;
using SystemIntegration.Models;

namespace SystemIntegration.Service
{
    public interface IUserInfoService
    {
        UserInfo GetUserInfoByNum(string userNum);
        List<UserInfo> GetUserInfoList();

        bool Update(UserInfo user);
    }
}
