using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Xml.Serialization;

namespace weixinweb.MessageType
{
       [XmlRoot(ElementName = "xml")]
    public class ResponseMessage
    {
        [XmlCData]
        public string ToUserName { get; set; }

        [XmlCData]
        public string FromUserName { get; set; }

        public DateTime CreateTime { get; set; }

        [XmlCData]
        public string   MsgType { get; set; }
    }
}