using System.Xml.Linq;

namespace weixinweb.MessageType
{
    class EventMessage:RequstMessage
    {
        public EventMessage(XElement xml)
            : base(xml)
        {
        }
    }
}

