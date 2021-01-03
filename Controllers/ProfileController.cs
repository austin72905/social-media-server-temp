using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaTemp.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            int memberID = Convert.ToInt32(Request.Query["memberid"]);
            string username = Request.Query["username"].ToString();

            var memdata = new MemberData();
            memdata = TempMemData.Memlist.Where(i => i.username == username).FirstOrDefault(); //預設值為null
            if (memdata == null)
            {
                return Json(new ProfileResp
                {
                    code=1,
                    msg="無此用戶"
                });
            }
            var respdata = new ProfileData()
            {
                username = memdata.username,
                nickname= memdata.nickname,
                gender = memdata.gender,
                job = memdata.job,
                state = memdata.state,
                introduce = memdata.introduce,
                interest = memdata.interest,
                preferType = memdata.preferType,

                memberID = memdata.memberID,
            };
            var resp = new ProfileResp()
            {
                code = 0,
                msg = "獲取成功",
                data = respdata,

            };
            return Json(resp);
        }
    }

    public class ProfileResp
    {
        public int code { get; set; }
        public ProfileData data { get; set; }
        public string msg { get; set; }
    }

    public class ProfileData
    {
        public string username { get; set; }

        public string nickname { get; set; }

        public string account { get; set; }
        public string gender { get; set; }
        public string job { get; set; }

        public string state { get; set; }

        public string introduce { get; set; }

        public string interest { get; set; }

        public string preferType { get; set; }

        public int memberID { get; set; }

        public bool isFriend { get; set; }
    }
}
