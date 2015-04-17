using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using weixinweb.MessageType;

namespace weixinweb.MessageType
{
    public class TextResponse : ResponseMessage
    {
        public string Content { get; set; }
    }
}
