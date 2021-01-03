using SocialMediaTemp.Chathub;
using System.Collections.Generic;

namespace SocialMediaTemp
{
    public class TempMemData
    {
        private static List<MemberData> _memlist { get; set; }

        public static List<MemberData> Memlist
        {
            get
            {
                if (_memlist == null)
                {
                    _memlist = GetMemlist();
                }

                return _memlist;
            }
        }

        public static List<MemberData> GetMemlist()
        {
            var memlist = new List<MemberData>()
            {
                new MemberData
                {
                    memberID=1,
                    username="austin",
                    password="123",
                    nickname= "台中小太陽",
                    gender="男",
                    job="工程師",
                    state="累累得",
                    introduce="我是大屌怪",
                    interest = "睡覺",
                    preferType="乃大的",

                },
                new MemberData
                {
                    memberID=2,
                    username="mandy",
                    password="123",
                    nickname= "內湖王阿姨",
                    gender="女",
                    job="年輕阿姨",
                    state="加好友就送I12 PRO",
                    introduce="東京有五棟房子",
                    interest = "聊天",
                    preferType="小鮮肉",
                },
                new MemberData
                {
                    memberID=3,
                    username="julia",
                    password="123",
                    nickname= "台北潘阿姨",
                    gender="男",
                    job="年輕阿姨",
                    state="現在加好友，晚上帶你去牽車 BY渴望小鮮肉的大獅子",
                    introduce="東區有5間店面",
                    interest = "睡覺",
                    preferType="乃大的",
                },
                new MemberData
                {
                    memberID=4,
                    username="nunu",
                    password="123",
                    nickname= "台中張阿姨",
                    gender="女",
                    job="市長",
                    state="加好友就送PS5",
                    introduce="台北有五棟房子",
                    interest = "睡覺",
                    preferType="小鮮肉",
                },
                new MemberData
                {
                    memberID=5,
                    username="jojo",
                    password="123",
                    nickname= "東京承太郎",
                    gender="男",
                    job="打手",
                    state="女人就是撈刀",
                    introduce="歐啦歐啦歐啦",
                    interest = "睡覺",
                    preferType="不說話的女人",
                },
                new MemberData
                {
                    memberID=6,
                    username="coco",
                    password="123",
                    nickname= "台北摳阿姨",
                    gender="女",
                    job="台女",
                    state="想找大棒棒",
                    introduce="我是coco 喜歡自摳",
                    interest = "睡覺、吃飯、打咚咚",
                    preferType="氣質",
                },
            };

            return memlist;
        }


        //訊息list
        private static Dictionary<string, Dictionary<string, List<ChatMsgData>>> _msgList { get; set; }

        public static Dictionary<string, Dictionary<string, List<ChatMsgData>>> MsgList
        {
            get
            {
                if(_msgList == null)
                {
                    _msgList = new Dictionary<string, Dictionary<string, List<ChatMsgData>>>();
                }
                return _msgList;
            }
        }

        public static Dictionary<string, List<ChatMsgData>> SaveMsgData(string id, ChatRespData userData,string input,bool unread=false)
        {
            
            var msgDetail = new ChatMsgData
            {
                memberid=userData.memberid,
                username =userData.username,
                gender=userData.gender,
                text = input,

            };
            //接收者在填ture
            if (unread)
            {
                msgDetail.unread = true;
            }
            var list = new List<ChatMsgData>();
            list.Add(msgDetail);
            var dic = new Dictionary<string, List<ChatMsgData>>();
            dic.Add(id, list);
            return dic;
        }

        public static ChatMsgData SaveMsgData(ChatRespData userData, string input, bool unread = false)
        {
            var msgDetail = new ChatMsgData
            {
                memberid = userData.memberid,
                username = userData.username,
                gender = userData.gender,
                text = input,

            };

            //接收者在填ture
            if (unread)
            {
                msgDetail.unread = true;
            }

            return msgDetail;
        }

    }



    public class MemberData
    {
        public string username { get; set; }

        public string password { get; set; }
        public string gender { get; set; }
        public int memberID { get; set; }

        public string nickname { get; set; }

        public string job { get; set; }

        public string state { get; set; }

        public string introduce { get; set; }

        public string interest { get; set; }

        public string preferType { get; set; }
    }

}
