using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIntegration.Service.Dtos;
using SystemIntegration.Repository;
using SystemIntegration.Models;

namespace SystemIntegration.Service
{
    public class SysInfoService : ISysInfoService
    {
        private IGenericRepository<SysInfo> repo;
        public SysInfoService(IGenericRepository<SysInfo> repository)
        {
            repo = repository;
        }
        public List<SysInfo> GetSysInfoList()
        {
            return repo.GetList().ToList();
        }
    }
}
