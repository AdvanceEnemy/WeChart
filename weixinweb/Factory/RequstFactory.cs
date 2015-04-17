using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using weixinweb.MessageType;

namespace weixinweb.Factory
{
    public class RequstFactory
    {
        public static RequstMessage CreateRequest(XDocument doc)
        {
            XElement root = doc.Root;
            switch (root.Element("MsgType").Value.ToUpper())
            {
                case "TEXT":
                   return new TextMessage(root);
                    break;
                case "IMAGE":
                   return new ImageMessage(root);
                    break;
                case "EVENT":
                    return new EventMessage(root);
                default:
                    throw new Exception("没有找到相应的MsgType类型！");
            }
        }


    }

}