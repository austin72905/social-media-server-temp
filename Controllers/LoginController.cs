using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialMediaTemp.Controllers
{
    public class LoginController : Controller
    {
        
        
        public IActionResult Index([FromBody] LoginReq req)
        {
            var respdata = new LoginData();
            //var memdata = new MemberData();
            var resp = new LoginResp();
            var memdata = TempMemData.Memlist.Where(i => i.username == req.username).FirstOrDefault();

            if(memdata == null)
            {
                resp.code = 1;
                resp.msg = "用戶不存在";
            }
            else
            {
                respdata.username = memdata.username;
                respdata.gender = memdata.gender;
                respdata.memberID = memdata.memberID;

                //response 
                resp.code = 0;
                resp.data = respdata;
            }

            //var respdata = new LoginData()
            //{
            //    username = "austin",
            //    gender = "男",
            //    memberID = 1,
            //};
            //var resp = new LoginResp()
            //{
            //    code = 0,
            //    data = respdata,

            //};
            return Json(resp);
        }


    }

    public class LoginReq
    {
        public string username { get; set; }

        public string password { get; set; }
    }

    public class LoginResp
    {
        public int code { get; set; }
        public LoginData data { get; set; }
        public string msg { get; set; }
    }

    public class LoginData
    {
        public string username { get; set; }
        public string gender { get; set; }
        public int memberID { get; set; }
        public bool isRegist { get; set; }
    }
}


