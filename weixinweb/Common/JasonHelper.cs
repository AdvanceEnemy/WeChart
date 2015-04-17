using System;
using Newtonsoft;
using Newtonsoft.Json;

namespace weixinweb.Common
{
    public class JasonHelper
    {
        ///// <summary>
        ///// 转换Json字符串到具体的对象
        ///// </summary>
        ///// <param name="url">返回Json数据的链接地址</param>
        ///// <param name="postData">POST提交的数据</param>
        ///// <returns></returns>
        //public static T ConvertJson(string url, string postData)
        //{
        //    HttpHelper helper = new HttpHelper();
        //    string content = helper.GetHtml(url, postData, true);
        //    VerifyErrorCode(content);

        //    T result = JsonConvert.DeserializeObject<T>(content);
        //    return result;
        //}

        ///// <summary>
        ///// 通用的操作结果
        ///// </summary>
        ///// <param name="url">网页地址</param>
        ///// <param name="postData">提交的数据内容</param>
        ///// <returns></returns>
        //public static CommonResult GetExecuteResult(string url, string postData = null)
        //{
            //CommonResult success = new CommonResult();
            //try
            //{
            //    ErrorJsonResult result;
            //    if (postData != null)
            //    {
            //        result = JsonHelper<ErrorJsonResult>.ConvertJson(url, postData);
            //    }
            //    else
            //    {
            //        result = JsonHelper<ErrorJsonResult>.ConvertJson(url);
            //    }

            //    if (result != null)
            //    {
            //        success.Success = (result.errcode == ReturnCode.请求成功);
            //        success.ErrorMessage = result.errmsg;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    success.ErrorMessage = ex.Message;
            //}

    //        //return success;
    //    } 
    }

    class CommonResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}