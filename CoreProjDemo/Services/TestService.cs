using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreProjDemo.Models;

namespace CoreProjDemo.Services
{
    public class TestService : ITestService
    {
        public async Task<bool> AddAsync(TestTable testTable)
        {
            return await Add(testTable);
        }
        private async Task<bool> Add(TestTable testTable)
        {
            return true;
        }
        public async Task<List<TestTable>> GetAllAsync()
        {
            var list= new List<TestTable>();
            for (int i = 0; i < 6; i++)
            {
                list.Add(new TestTable { Id = i + 1, Age = 20 + i, Name = $"老赵{i + 1}", Sex = (i % 2 == 0 ? true : false),Remarks=$"这是老赵{i + 1}" });
            }
            return list;
        }
    }
}
