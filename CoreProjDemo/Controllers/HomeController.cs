using CoreProjDemo.Models;
using CoreProjDemo.Services;
using CoreProjDemo.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProjDemo.Controllers
{
    public class HomeController:Controller
    {
        private  readonly ITestService _testService;
        //private readonly IOptions<ConnectionOptions> _options;
        //public HomeController(ITestService testService,IOptions<ConnectionOptions> options)//这样ConnectionOptions类就被注入了Controller
        //{
        //    _testService = testService;
        //    _options = options;
        //}
        public HomeController(ITestService testService)
        {
            _testService = testService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "班级列表";
            return View(await _testService.GetAllAsync());
        }
        public IActionResult Add()
        {
            ViewBag.Title = "添加人员";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(TestTable model)
        {
            if(ModelState.IsValid)
            {
                _testService.AddAsync(model);
            }
            return RedirectToAction("Index");
        }

        public  IActionResult Edit(int Id)
        {
            return RedirectToAction("Index");
        }
    }
}
