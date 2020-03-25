using CoreProjDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProjDemo.Services
{
   public interface ITestService
    {
        Task<List<TestTable>> GetAllAsync();
        Task<bool> AddAsync(TestTable testTable);
    }
}
