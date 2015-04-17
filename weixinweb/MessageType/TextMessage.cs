using System;
using System.Xml.Linq;

namespace weixinweb.MessageType
{
    public class TextMessage:RequstMessage
    {
        public TextMessage(XElement xml)
            : base(xml)
        {
            Content = xml.Element("Content").Value;
        }
        public string Content { get; set; }
    }
}