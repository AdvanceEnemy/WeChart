using System.Xml.Linq;

namespace weixinweb.MessageType
{
    class ImageMessage:RequstMessage
    {
        public ImageMessage(XElement xml)
            : base(xml)
        {
        }
    }
}
