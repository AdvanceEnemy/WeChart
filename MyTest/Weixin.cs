using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;

namespace MyTest
{
    public class Weixin:IHttpHandler
    {
        #region

        private string[] OpenParam = {};
        private bool CheckSignature(string signature,string timestamp,string nonce,string token)
        {
            string[] arrtemp = {token,timestamp,nonce};

            Array.Sort(arrtemp);

            string tempStrShell = string.Join("",arrtemp);
            tempStrShell = FormsAuthentication.HashPasswordForStoringInConfigFile(tempStrShell,"SHA1").ToLower();

            return tempStrShell == signature;
        }

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            string postString=string.Empty;
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                using (Stream stream = HttpContext.Current.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                }

                if (!string.IsNullOrEmpty(postString))
                {
                    Execute(postString);
                }
            }
            else
            {
                Auth(); //微信接入的测试
            }
        }

        private void Auth()
        {
            string token = "weixin";//你申请的时候填写的Token
            string echoString = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];
        }

        private void Execute(string postString)
        {
            //context.Response.Write("Hello");
        }
        #endregion


    }
}