using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaTemp.Controllers
{
    public class PersonalController : Controller
    {

        public IActionResult Index()
        {
            int memberID = Convert.ToInt32(Request.Query["memberid"]);


            var memdata = new MemberData();
            memdata = TempMemData.Memlist.Where(i => i.memberID == memberID).FirstOrDefault();

            var respdata = new PersonalData() 
            { 
                username =memdata.username,
                nickname =memdata.nickname,
                gender = memdata.gender,
                job =memdata.job,
                state=memdata.state,
                introduce =memdata.introduce,
                interest =memdata.interest,
                preferType=memdata.preferType,
                memberID =memdata.memberID

            };
            var resp = new PersonalResp()
            {
                code = 0,
                msg="獲取成功",
                data = respdata,

            };
            return Json(resp);
        }



        public IActionResult SelectOption()
        {
            var respdata = new SOData()
            {
                interests = new List<string>() { "跳舞","睡覺","看書"},
                preferTypes = new List<string>() { "陽光", "知性", "文靜" },
            };
            var resp = new SOResp()
            {
                code = 0,
                msg = "獲取成功",
                data = respdata,

            };
            return Json(resp);
        }


    }

    public class PersonalResp
    {
        public int code { get; set; }
        public PersonalData data { get; set; }
        public string msg { get; set; }
    }

    public class PersonalData
    {
        public string username { get; set; }

        public string nickname { get; set; }

        public string gender { get; set; }
        public string job { get; set; }

        public string state { get; set; }

        public string introduce { get; set; }

        public string interest { get; set; }

        public string preferType { get; set; }

        public int memberID { get; set; }
        public bool isRegist { get; set; }
    }


    public class SOResp
    {
        public int code { get; set; }
        public SOData data { get; set; }
        public string msg { get; set; }
    }

    public class SOData
    {
        public List<string> interests { get; set; }

        public List<string> preferTypes { get; set; }

        public bool isRegist { get; set; }
    }

}
