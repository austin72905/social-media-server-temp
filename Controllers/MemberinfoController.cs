using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaTemp.Controllers
{
    public class MemberinfoController : Controller
    {
        public IActionResult Index()
        {
            List<MemberinfoData> memlist = new List<MemberinfoData>();

            foreach(var item in TempMemData.Memlist)
            {
                memlist.Add(
                    new MemberinfoData()
                    {
                        username = item.username,
                        nickname = item.nickname,
                        gender = item.gender,
                        memberID = item.memberID,
                        job = item.job,
                        state = item.state,
                        introduce = item.introduce
                    });
            }

            

            var resp = new MemberinfoResp()
            {
                code = 0,
                data = memlist,

            };
            return Json(resp);
        }
    }

    public class MemberinfoResp
    {
        public int code { get; set; }
        public List<MemberinfoData> data { get; set; }
        public string msg { get; set; }
    }

    public class MemberinfoData
    {
        public string username { get; set; }

        public string nickname { get; set; }
        public int memberID { get; set; }
        public string gender { get; set; }
        public string job { get; set; }
        public string state { get; set; }
        public string introduce { get; set; }

        public List<string> intersert { get; set; }

        public List<string> preferType { get; set; }
        public bool isRegist { get; set; }
    }
}
