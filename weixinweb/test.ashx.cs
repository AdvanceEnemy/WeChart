using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml;


namespace weixinweb
{
    /// <summary>
    /// interfaceTest 的摘要说明
    /// </summary>
    public class InterfaceTest : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                ExecutePost();
            }
            else
            {
                //微信接入的测试
                Auth();
            }
        }
        #region
        private void Auth()
        {
            const string token = "wxskyseaweixin"; //你申请的时候填写的Token
            string echoString = HttpContext.Current.Request.QueryString["echostr"];
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];

            if (CheckSignature(signature, timestamp, nonce, token))
            {
                if (!string.IsNullOrEmpty(echoString))
                {
                    HttpContext.Current.Response.Write(echoString);
                }
            }
        }

        private bool CheckSignature(string signature, string timestamp, string nonce, string token)
        {
            string[] arrtemp = { token, timestamp, nonce };
            Array.Sort(arrtemp);
            string tempStrShell = string.Join("", arrtemp);
            tempStrShell = FormsAuthentication.HashPasswordForStoringInConfigFile(tempStrShell, "SHA1").ToLower();
            return tempStrShell == signature;
        }
        private static void ExecutePost()
        {
          #region test
//            string postString;
//            using (Stream stream = HttpContext.Current.Request.InputStream)
//            {
//                Byte[] postBytes = new Byte[stream.Length];
//                stream.Read(postBytes, 0, (Int32)stream.Length);
//                postString = Encoding.UTF8.GetString(postBytes);
//            }
//            //使用XMLDocument加载信息结构
//            XmlDocument xmlDoc = new XmlDocument();
//            xmlDoc.LoadXml(postString);

//            //把传过来的XML数据各个字段区分出来，并且填到fields这个字典变量中去
//            var selectSingleNode = xmlDoc.SelectSingleNode("/xml");
//            if (selectSingleNode != null)
//            {
//                Dictionary<string, string> fields = selectSingleNode.ChildNodes.Cast<XmlNode>().ToDictionary(x => x.Name, x => x.InnerText);
//                //形成返回格式的XML文档
//                string returnXml = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{3}]]></Content></xml>", fields["FromUserName"], fields["ToUserName"], DateTime.Now.Subtract(new DateTime(1970, 1, 1, 8, 0, 0)).TotalSeconds, fields["Content"]);
//                //HttpContext.Current.Response.ContentType = "text/xml";
//                HttpContext.Current.Response.Write(returnXml = @"<xml>
//  <Content>都很好</Content>
//  <ToUserName><![CDATA[ouwATs99mmE2nRTyogNFtDet_ZKk]]></ToUserName>
//  <FromUserName><![CDATA[gh_333df3d90472]]></FromUserName>
//  <CreateTime>1429175671</CreateTime>
//  <MsgType><![CDATA[text]]></MsgType>
//</xml>");
//            }
#endregion
            try
            {
                var handler = new CustomWeChatHandler(HttpContext.Current.Request.InputStream);

                handler.Execute();
                //string v = "<?xml version=\"1.0\"?><ToUserName>ouwATs99mmE2nRTyogNFtDet_ZKk</ToUserName><FromUserName>gh_333df3d90472</FromUserName><CreateTime>2015-03-26T18:00:37.4964169+08:00</CreateTime><MsgType>text</MsgType><Content>扭扭捏捏</Content>>";
                HttpContext.Current.Response.Write(@handler.ProcessResult());
//                HttpContext.Current.Response.Write( @"<xml>
//                  <Content>都很好</Content>
//                  <ToUserName><![CDATA[ouwATs99mmE2nRTyogNFtDet_ZKk]]></ToUserName>
//                  <FromUserName><![CDATA[gh_333df3d90472]]></FromUserName>
//                  <CreateTime>1429175671</CreateTime>
//                  <MsgType><![CDATA[text]]></MsgType>
//                </xml>");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

    }
}