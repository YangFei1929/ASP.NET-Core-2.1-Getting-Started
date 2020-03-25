using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProjDemo.Services
{
    public class Welcome:IWelcome
    {
        public string GetWelcomeMsg()
            => "你好，.Net Core 2.1";
    }
}
