using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Models;
using SystemIntegration.Repository;

namespace SystemIntegration.Service
{
    public class UserInfoService : IUserInfoService
    {
        private IGenericRepository<UserInfo> userInfoRepo;
        public UserInfoService(IGenericRepository<UserInfo> repo)
        {
            userInfoRepo = repo;
        }

        public UserInfo GetUserInfoByNum(string userNum)
        {
            return userInfoRepo.GetByID(userNum);
        }

        public List<UserInfo> GetUserInfoList()
        {
            return userInfoRepo.GetList().ToList();
        }

        public bool Update(UserInfo user)
        {
            return userInfoRepo.Update(user);
        }
    }
}
