using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaTemp.Controllers
{
    public class FriendController : Controller
    {
        public IActionResult Index()
        {
            List<FriendData> memlist = new List<FriendData>();



            foreach (var item in TempMemData.Memlist)
            {
                memlist.Add(
                    new FriendData()
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
            var resp = new FriendResp()
            {
                code = 0,
                data = memlist,

            };
            return Json(resp);
        }


        //加好友
        public IActionResult Add([FromBody] FriendReq req)
        {
            List<FriendData> memlist = new List<FriendData>();

            foreach (var item in TempMemData.Memlist)
            {
                memlist.Add(
                    new FriendData()
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

            //var respdata = new FriendData()
            //{
            //    username = "austin",
            //    nickname = "台北張阿姨",
            //    gender = "女",
            //    memberID = 1,
            //    job = "阿姨",
            //    state = "加好友就送PS5",
            //    introduce = "台北有五棟房子"
            //};

            //var respdata2 = new FriendData()
            //{
            //    username = "julia",
            //    nickname = "台北潘阿姨",
            //    gender = "女",
            //    memberID = 3,
            //    job = "年輕阿姨",
            //    state = "現在加好友，晚上帶你去牽車 BY渴望小鮮肉的大獅子",
            //    introduce = "東區有5間店面"
            //};

            //memlist.Add(respdata);
            //memlist.Add(respdata2);

            var resp = new FriendResp()
            {
                code = 0,
                msg = "新增成功",
                data = memlist,

            };
            return Json(resp);
        }

        //刪除好友
        public IActionResult Delete([FromBody] FriendReq req)
        {
            List<FriendData> memlist = new List<FriendData>();

            foreach (var item in TempMemData.Memlist)
            {
                memlist.Add(
                    new FriendData()
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

            //var respdata = new FriendData()
            //{
            //    username = "austin",
            //    nickname = "台北張阿姨",
            //    gender = "女",
            //    memberID = 1,
            //    job = "阿姨",
            //    state = "加好友就送PS5",
            //    introduce = "台北有五棟房子"
            //};

            //var respdata2 = new FriendData()
            //{
            //    username = "julia",
            //    nickname = "台北潘阿姨",
            //    gender = "女",
            //    memberID = 3,
            //    job = "年輕阿姨",
            //    state = "現在加好友，晚上帶你去牽車 BY渴望小鮮肉的大獅子",
            //    introduce = "東區有5間店面"
            //};

            //memlist.Add(respdata);
            //memlist.Add(respdata2);

            var resp = new FriendResp()
            {
                code = 0,
                msg = "刪除成功",
                data = memlist,

            };
            return Json(resp);
        }

    }


    public class FriendReq
    {
        public int memberid { get; set; }

        public int friendid { get; set; }
    }




    public class FriendResp
    {
        public int code { get; set; }
        public List<FriendData> data { get; set; }
        public string msg { get; set; }
    }

    public class FriendData
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
