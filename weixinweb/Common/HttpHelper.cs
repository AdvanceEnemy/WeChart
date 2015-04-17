using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace weixinweb.Common
{
    public enum HttpType
    {
        GET = 1,
        POST = 2,
    }

    public static class HttpHelper
    {
        public const int TIME_OUT = 10000;

        public const string WxFlag = "MicroMessenger";

        public const string PhoneFlag = "Phone";

        private static readonly string ProxyUrl = ConfigurationManager.AppSettings["ProxyUrl"];

        public static string HttpGet(string url)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;

            if (request == null)
                throw new ArgumentException();
            request.Method = "GET";
            request.Timeout = TIME_OUT;

#if(!DEBUG)
            var proxy = new WebProxy(ProxyUrl, 8080) { UseDefaultCredentials = false };
            WebRequest.DefaultWebProxy = proxy;
            request.Proxy = proxy;
#endif

            var res = (HttpWebResponse)request.GetResponse();
            if (res.StatusCode != HttpStatusCode.OK)
                throw new WebException("code" + res.StatusCode);

            using (Stream responseStream = res.GetResponseStream())
            {
                using (var responseReader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    string result = responseReader.ReadToEnd();
                    return result;
                }
            }
        }

        public static string HttpPost(string url, string content)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;

            if (request == null)
                throw new ArgumentException();
            byte[] postBytes = Encoding.UTF8.GetBytes(content);
            request.Method = "POST";
            request.Timeout = TIME_OUT;
            request.ContentType = "application/json; charset=utf-8";
            request.ContentLength = postBytes.Length;

#if(!DEBUG)
            var proxy = new WebProxy(ProxyUrl, 8080) { UseDefaultCredentials = false };
            WebRequest.DefaultWebProxy = proxy;
            request.Proxy = proxy;
#endif

            Stream stream = request.GetRequestStream();
            stream.Write(postBytes, 0, postBytes.Length);
            stream.Close();

            var res = (HttpWebResponse)request.GetResponse();
            if (res.StatusCode != HttpStatusCode.OK)
                throw new WebException("code" + res.StatusCode);


            using (Stream responseStream = res.GetResponseStream())
            {
                using (var responseReader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    string result = responseReader.ReadToEnd();
                    return result;
                }
            }
        }

        public static bool IsWeChatClientRequest(this HttpContextBase httpContext)
        {
            var userAgent = httpContext.Request.UserAgent;
            return userAgent != null
                && (userAgent.Contains(WxFlag) || userAgent.Contains(PhoneFlag));
        }

        public static string HtmlEncode(this string html)
        {
            return HttpUtility.HtmlEncode(html);
        }

        public static string HtmlDecode(this string html)
        {
            return HttpUtility.HtmlDecode(html);
        }

        public static string UrlEncode(this string url)
        {
            return HttpUtility.UrlEncode(url);
        }

        public static string UrlDecode(this string url)
        {
            return HttpUtility.UrlDecode(url);
        }
    }
}