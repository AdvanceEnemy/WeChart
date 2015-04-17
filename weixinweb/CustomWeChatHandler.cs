using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using weixinweb.Common;
using weixinweb.Factory;
using weixinweb.MessageType;
using weixinweb.Menu;

namespace weixinweb
{
    class CustomWeChatHandler
    {
        public RequstMessage RequstMsg { get; set; }

        public ResponseMessage ResponseMsg { get; set; }

        public CustomWeChatHandler(Stream inputStream)
        {
            using (XmlReader xmlReader = XmlReader.Create(inputStream))
            {
                XDocument doc = XDocument.Load(xmlReader);
                RequstMsg = RequstFactory.CreateRequest( doc);
            }
        }

        #region MyRegion

         

        #endregion
        internal void Execute()
        {
            MenuInfo.CreateMenu();
            if (RequstMsg == null)
                return;
            try
            {
            switch (RequstMsg.MsgType.ToUpper())
            {
                case "TEXT":
                   ResponseMsg= OnExecuteText(RequstMsg as TextMessage);
                    break;
                case "IMAGE":
                    ResponseMsg=OnExecuteImage(RequstMsg as ImageMessage);
                    break;
                case "EVENT":
                    ResponseMsg = OnExecuteEvent(RequstMsg as EventMessage);
                    break;
                default:
                    throw new Exception("没有找到相应的MsgType类型！");
            }
            }
            catch (Exception)
            {
                
                throw new Exception("wrong response instance");
            }

        }


        private ResponseMessage OnExecuteImage(ImageMessage imageMessage)
        {
            return GetTextResponseMsg(imageMessage.CreateTime.ToString(CultureInfo.InvariantCulture));
            //
        }

        private ResponseMessage OnExecuteEvent(EventMessage eventMessage)
        {
            return GetTextResponseMsg(eventMessage.CreateTime.ToString(CultureInfo.InvariantCulture));
            //
        }
        private ResponseMessage OnExecuteText(TextMessage textMessage)
        {
            return GetTextResponseMsg(textMessage.Content);
        }

        private TextResponse GetTextResponseMsg(string content)
        {
            var response = ResponseFactory.GetResponseMsg<TextResponse>(RequstMsg);
            response.Content = content;
            return response;
        }

        public string ProcessResult()
        {
            XDocument xml = ResponseMsg.ToXml();
            //return ToXml(ResponseMsg);
            return xml.ToString();
        }

        public string ToXml(object input)
        {
            var xs = new XmlSerializer(input.GetType());
            using (var memoryStream = new MemoryStream())
            using (var xmlTextWriter = new XmlTextWriter(memoryStream, new UTF8Encoding()))
            {
                xs.Serialize(xmlTextWriter, input);
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
    }


}
