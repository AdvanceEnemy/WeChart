using System;
using System.Xml.Linq;

namespace weixinweb.MessageType
{
    public class RequstMessage
    {
   
        public RequstMessage(XElement xml)
        {
            ToUserName = xml.Element("ToUserName").Value;
            FromUserName = xml.Element("FromUserName").Value;
            CreateTime = Convert.ToInt64(xml.Element("CreateTime").Value);
            MsgId = xml.Element("MsgId").Value;
            MsgType = xml.Element("MsgType").Value;
        }

        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public long CreateTime { get; set; }
        public string MsgId { get; set; }
        public string MsgType { get; set; }
    }
}