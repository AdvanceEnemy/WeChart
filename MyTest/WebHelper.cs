using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace MyTest
{
    public class WebHelper
    {
        const string Url = @"http://www.zhihu.com/login";

        public static string SendRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            CookieContainer cc = new CookieContainer();
            
            //---simulate account:
            request.Method = "POST";
            //request.ContentType = "";
            request.CookieContainer = cc;
            string data = "_xsrf:2cbcc234831b56bb88cf80dfb532178d&" +
            "email:93612774@qq.com&" +
            "password:songsz1songsz1&" +
            "rememberme:y";
            data = HttpUtility.UrlEncode(data);
            byte[] postdata = Encoding.UTF8.GetBytes(data);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentLength = postdata.Length;
            request.AllowAutoRedirect = false;
            request.KeepAlive = true;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(postdata, 0, postdata.Length);
                stream.Close();
            }

            HttpWebResponse res = (HttpWebResponse)request.GetResponse();
            res.Cookies = cc.GetCookies(request.RequestUri);

            // return response;
            string htmlData = string.Empty;
            Stream datastream = res.GetResponseStream();
            if (datastream != null)
            {
                var reader = new StreamReader(datastream);
                htmlData = reader.ReadToEnd();
                reader.Close();
                datastream.Close();
            }
            res.Close();


            HttpWebRequest req2 = (HttpWebRequest)WebRequest.Create("http://www.zhihu.com/");
            req2.CookieContainer = new CookieContainer();
            req2.CookieContainer = cc;
            req2.Method = "GET";
            var res2 = (HttpWebResponse) req2.GetResponse();
            Stream datastream2 = res2.GetResponseStream();
            if (datastream2 != null)
            {
                var reader = new StreamReader(datastream2);
                htmlData = reader.ReadToEnd();
                reader.Close();
                datastream2.Close();
            }
            res.Close();


            return htmlData;

        }

        //private static HttpWebResponse GetResponse()
        //{
        //    var req = SendRequest();
        //    var response = (HttpWebResponse)req.GetResponse();
        //    //Console.WriteLine(response.StatusDescription);
        //    return response;
        //}
        //public static string  GetHtmlData()
        //{

        //    var response = GetResponse();
        //    string htmlData = string.Empty;
        //    Stream datastream = response.GetResponseStream();
        //    if (datastream != null)
        //    {
        //        var reader = new StreamReader(datastream);
        //        htmlData = reader.ReadToEnd();
        //        reader.Close();
        //        datastream.Close();
        //    }
        //    response.Close();
        //    return htmlData;
        //}


        public static string GetAccessToken()
        {
            string accessToken = string.Empty;
            string getUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
            getUrl = string.Format(getUrl, "wxbf19b3533a3d72cd", "d473ca6261bb8d1c62db26fe52d78003");
            Uri uri = new Uri(getUrl);
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(uri);
            webReq.Method = "POST";

            //获取返回信息
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string returnJason = streamReader.ReadToEnd();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> json = (Dictionary<string, object>)serializer.DeserializeObject(returnJason);
            object value;
            if (json.TryGetValue("access_token", out value))
            {
                accessToken = value.ToString();
            }
            return accessToken;
        }
    }
}