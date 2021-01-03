using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialMediaTemp.Controllers
{
    public class RegisterController: Controller
    {
        
        public IActionResult Index()
        {
            var respdata = new Data()
            {
                username = "austin",
                gender = "男",
                memberID = 1,
                isRegist = true
            };
            var resp = new Resp()
            {
                code =0,
                data= respdata,

            };
            var str = JsonSerializer.Serialize(resp);
            return Json(resp);
        }
    }

    public class Resp
    {
        public int code { get; set; }
        public Data data { get; set; }
        public string msg  { get; set; }
    }

    public class Data
    {
        public string username { get; set; }
        public string gender { get; set; }
        public int memberID { get; set; }
        public bool isRegist { get; set; }
    }
}
