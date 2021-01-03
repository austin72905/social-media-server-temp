using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaTemp.Chathub
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, List<string>> _onnectList;
        public static Dictionary<string, List<string>> ConnectList
        {
            get
            {
                if (_onnectList == null)
                {
                    var newdic = new Dictionary<string, List<string>>();
                    _onnectList = newdic;
                }
                return _onnectList;
            }
            set
            {
                if (_onnectList == null)
                {
                    var newdic = new Dictionary<string, List<string>>();
                    _onnectList = newdic;
                }
            }
        }

        public string MyK { get; set; }
        public async Task SendAllMsg(string user, string input)
        {

            var content = $"{user} 說了 {input}";
            var connectID = Context.ConnectionId;

            //var groupname = Context.User.Identity.Name;
            await Clients.All.SendAsync("RecieveMsg", content + "我的ID是: " + connectID);
        }



        public async Task SendGroupMsg(string user, string input)
        {
            var content = $"{user} 說了 {input}";
            await Clients.Group("").SendAsync("", content);
        }

        //感覺是要用這個
        public async Task SendOneMsg(string user, string input)
        {
            var content = $"{user} 說了 {input}";

            await Clients.Client(Context.ConnectionId).SendAsync("SendOneMsg", content);
        }

        public async Task SendBothMsg(string userid, string recieveid, string input)
        {
            
            
            //獲取用戶資料
            var userdata = new MemberData();
            userdata = TempMemData.Memlist.Where(i => i.memberID == Convert.ToInt32(userid)).FirstOrDefault();

            var recieverdata = new MemberData();
            recieverdata = TempMemData.Memlist.Where(i => i.memberID == Convert.ToInt32(recieveid)).FirstOrDefault();

            var chatuserdata = new ChatRespData
            {
                memberid = userdata.memberID,
                username =userdata.nickname,
                gender=userdata.gender
            };


            //這個傳去前端好像也沒用到...
            var chatrecieverdata = new ChatRespData
            {
                memberid = recieverdata.memberID,
                username = recieverdata.username,
                gender = recieverdata.gender
            };


            //要傳到message組件的訊息
            //也要分傳給接收者還是自己的
            //自己接收的性別 要是接收 者的 不然大頭貼會嘿嘿嘿
            var chatLastMsgData = new ChatMsgListData
            {
                memberid = userdata.memberID,
                gender = recieverdata.gender,
                username = userdata.nickname,
                text = input,
                //是對哪個用戶的msg
                chatname = recieverdata.nickname,
                //傳這個是要讓點訊息時可以到該聊天室
                chatid = recieverdata.memberID.ToString(),
            };

            var chatLastMsgDataRec = new ChatMsgListData
            {
                memberid = userdata.memberID,
                gender = userdata.gender,
                username = userdata.nickname,
                text = input,
                //是對哪個用戶的msg
                chatname = userdata.nickname,
                //傳這個是要讓點訊息時可以到該聊天室
                chatid = userdata.memberID.ToString(),
                unreadcount =1
            };

            //將對話紀錄存下來
            //第一次用戶紀錄對話
            if (!TempMemData.MsgList.ContainsKey(userid))
            {
                //將對話存在 recieve id 的紀錄
                var MsgData = TempMemData.SaveMsgData(recieveid, chatuserdata, input);
                TempMemData.MsgList.Add(userid, MsgData);
            }
            else
            {
                var msgDetail = TempMemData.SaveMsgData(chatuserdata, input);
                //第一次與某用戶對話
                if (!TempMemData.MsgList[userid].ContainsKey(recieveid)) 
                {
                    
                    TempMemData.MsgList[userid].Add(recieveid, new List<ChatMsgData> { msgDetail });
                }
                else
                {
                    //之前有對話過
                    //取 List<ChatMsgData> 加入新對話
                    TempMemData.MsgList[userid][recieveid].Add(msgDetail);
                }
            }

            //接收者的資料
            if (!TempMemData.MsgList.ContainsKey(recieveid))
            {
                //應該是發送者的資料
                var MsgData = TempMemData.SaveMsgData(userid, chatuserdata, input,true);
                TempMemData.MsgList.Add(recieveid, MsgData);
            }
            else
            {
                var msgDetail = TempMemData.SaveMsgData(chatuserdata, input, true);
                
                //如果兩個用戶間之前沒對話過
                if (!TempMemData.MsgList[recieveid].ContainsKey(userid))
                {

                    TempMemData.MsgList[recieveid].Add(userid, new List<ChatMsgData> { msgDetail });
                }
                else
                {
                    //之前有對話過
                    //取 List<ChatMsgData> 加入新對話
                    TempMemData.MsgList[recieveid][userid].Add(msgDetail);
                }

            }
            

            //member 的 connectionid list
            var useridList = ConnectList[userid];
            //reciever 的 connectionid list
            var recieveidList = new List<string>();
            if (ConnectList.ContainsKey(recieveid))
            {
                //reciever 的 connectionid list

                recieveidList = ConnectList[recieveid];
            }

            var resultList = useridList.Concat(recieveidList).ToList();
            await Clients.Clients(resultList).SendAsync("RecieveBothMsg", chatuserdata, chatrecieverdata, input);

            //傳訊息到message 組件 (最後訊息)
            await Clients.Clients(useridList).SendAsync("SendLastMsg", chatLastMsgData);
            await Clients.Clients(recieveidList).SendAsync("SendLastMsg", chatLastMsgDataRec);

            //修改未讀總數
            //改變footer 未讀總數
            //讓接收者知道是誰傳的
            await Clients.Clients(recieveidList).SendAsync("ChangeTotal", userid, 1);

        }

        public async Task AddConnectList(string userid, string recieveid)
        {

            var recieveidList = new List<string>();
            if (!ConnectList.ContainsKey(userid))
            {
                ConnectList.Add(userid, new List<string>() { Context.ConnectionId });
            }
            else
            {
                ConnectList[userid].Add(Context.ConnectionId);
            }

            //如果他還沒上線過，就傳空的connectionID
            if (ConnectList.ContainsKey(recieveid))
            {
                //reciever 的 connectionid list

                recieveidList = ConnectList[recieveid];
            }

            //member 的 connectionid list
            var useridList = ConnectList[userid];

            var resultList = useridList.Concat(recieveidList).ToList();

            await Clients.Clients(resultList).SendAsync("IntoChat", "已進入聊天室" + "自己ID是: " + Context.ConnectionId);


        }

        //讀訊息
        public async Task ReadMsg(string userid, string recieveid)
        {
            //把數據庫的屬性改成unread=false
            if (TempMemData.MsgList.ContainsKey(userid))
            {
                if (TempMemData.MsgList[userid].ContainsKey(recieveid))
                {
                    foreach(var item in TempMemData.MsgList[userid][recieveid])
                    {
                        item.unread = false;
                    }
                }
            }
            //member 的 connectionid list
            var useridList = ConnectList[userid];
            //改變footer 未讀總數
            await Clients.Clients(useridList).SendAsync("ChangeTotal", 0,0);
        }

        //建立連線時

        public override Task OnConnectedAsync()
        {

            return base.OnConnectedAsync();
        }

        //解除連線時
        public override Task OnDisconnectedAsync(Exception exception)
        {
            foreach (var connectList in ConnectList)
            {
                //斷線把連線ID刪掉
                connectList.Value.Remove(Context.ConnectionId);
            }
            return base.OnDisconnectedAsync(exception);
        }

    }

    public class ChatRespData
    {
        public string username { get; set; }

        public int memberid { get; set; }

        public string gender { get; set; }
    }

    public class ChatMsgData
    {
        public string username { get; set; }
        //哪個ID 的用戶傳的
        public int memberid { get; set; }

        public string gender { get; set; }

        public string text { get; set; }

        public bool unread { get; set; }
    }

    public class ChatMsgListData
    {
        public string username { get; set; }
        //哪個ID 的用戶傳的
        public int memberid { get; set; }

        public string gender { get; set; }

        public string text { get; set; }

        public string chatname { get; set; }

        public string chatid { get; set; }

        public int unreadcount { get; set; }
    }


}
