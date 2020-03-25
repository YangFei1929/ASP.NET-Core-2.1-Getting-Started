using CoreProjDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProjDemo.ViewComponents
{
    public class PersonListViewComponent:ViewComponent
    {
        private readonly ITestService _testService;
        public PersonListViewComponent(ITestService testService)
        {
            _testService = testService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int bigerNum)
        {
            var list = await _testService.GetAllAsync();
            var res = list.Where(u => u.Id > bigerNum).ToList();
            return View(res.Count);
        }
    }
}
