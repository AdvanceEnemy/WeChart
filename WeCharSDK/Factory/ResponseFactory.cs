using System;
using weixinweb.MessageType;

namespace weixinweb.Factory
{
    public class ResponseFactory
    {
        public static T GetResponseMsg<T>(RequstMessage request) where T : ResponseMessage
        {
            try
            {
                var instance = Activator.CreateInstance<T>();
                instance.ToUserName = request.FromUserName;
                instance.FromUserName = request.ToUserName;
                instance.CreateTime = DateTime.Now;
                instance.MsgType = request.MsgType;
                return instance;
            }
            catch (Exception)
            {
                throw new Exception("创建回复实力失败！");
            }
        }
    }
}
