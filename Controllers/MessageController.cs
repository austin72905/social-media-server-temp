using Microsoft.AspNetCore.Mvc;
using SocialMediaTemp.Chathub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaTemp.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            string memberid =Request.Query["memberid"].ToString();
            string recieveid = Request.Query["recieveid"].ToString();
            var resp = new MsgResp 
            { 
                code=0,
                msg="獲取訊息成功"
            };
            //先找到memberid 對應的資料
            if (!TempMemData.MsgList.ContainsKey(memberid))
            {
                var list = new List<ChatMsgData>();
                resp.data = list;
                return Json(resp);
            }

            if (!TempMemData.MsgList[memberid].ContainsKey(recieveid))
            {
                var list = new List<ChatMsgData>();
                resp.data = list;
                return Json(resp);
            }

            var resultlist = TempMemData.MsgList[memberid][recieveid].ToList();
            resp.data = resultlist;
            return Json(resp);
        }

        public IActionResult LastMsgs()
        {
            string memberid = Request.Query["memberid"].ToString();
            //string recieveid = Request.Query["recieveid"].ToString();

            var resp = new MsgListResp
            {
                code = 0,
                msg = "獲取訊息成功",
                
            };

            var list = new List<ChatMsgListData>();
            //先找到memberid 對應的資料
            //沒找到就返回空值
            if (!TempMemData.MsgList.ContainsKey(memberid))
            {
                
                resp.data = list;
                return Json(resp);
            }
            //取得memberid 裡面所有訊息的最後一個值
            foreach (var item in TempMemData.MsgList[memberid]) 
            {
                //獲取用戶資料(取得是跟哪個用戶聊天)
                var userdata = new MemberData();
                userdata = TempMemData.Memlist.Where(i => i.memberID == Convert.ToInt32(item.Key)).FirstOrDefault();

                int countUnread = 0;
                //計算未讀
                foreach (var chatMsgData in item.Value)
                {
                    if (chatMsgData.unread == true)
                    {
                        countUnread += 1;
                    }
                }

                //item.Value => List   .Last() => 取最後一個元素
                list.Add(new ChatMsgListData 
                { 
                    memberid=item.Value.Last().memberid,
                    gender = userdata.gender,
                    username = item.Value.Last().username,
                    text = item.Value.Last().text,
                    chatname = userdata.nickname,
                    chatid =item.Key,
                    unreadcount = countUnread


                });
                
                

            }

            //把最後訊息的列表給他
            resp.data = list;
            return Json(resp);
        }


        public IActionResult UnreadMsg()
        {
            string memberid = Request.Query["memberid"].ToString();
            int countUnread = 0;
            var resp = new MsgCountResp
            {
                code = 0,
                msg = "獲取訊息成功",

            };
            var countUnreadDic = new Dictionary<string, int>();
            //先找到memberid 對應的資料
            //沒找到就返回空值
            if (!TempMemData.MsgList.ContainsKey(memberid))
            {

                resp.data = countUnreadDic;
                return Json(resp);
            }

            //取得memberid 裡面所有訊息的最後一個值
            foreach (var item in TempMemData.MsgList[memberid])
            {
                countUnreadDic.Add(item.Key, 0);
                //計算未讀
                foreach (var chatMsgData in item.Value)
                {
                    int count = 0;
                    if (chatMsgData.unread == true)
                    {
                        count += 1;
                    }
                    countUnreadDic[item.Key]= countUnreadDic[item.Key]+count;
                }

            }
            resp.data = countUnreadDic;
            return Json(resp);


        }

    }

    public class MsgResp
    {
        public int code { get; set; }
        public List<ChatMsgData> data { get; set; }
        public string msg { get; set; }
    }

    public class MsgListResp
    {
        public int code { get; set; }
        public List<ChatMsgListData> data { get; set; }
        public string msg { get; set; }

    }

    public class MsgCountResp
    {
        public int code { get; set; }
        public Dictionary<string,int> data { get; set; }
        public string msg { get; set; }
    }


}
