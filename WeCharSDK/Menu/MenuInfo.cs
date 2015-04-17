
using System.Collections.Generic;
using Newtonsoft.Json;
using weixinweb.Common;

namespace weixinweb.Menu
{
    public class MenuInfo
    {
        private const string CreateMenuUrl = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={uUgEMMZ_Cag7NSIsGoCG-NyatrZVn9ay8bOOCuqPeinZELTvju9YqMCwPC2CsPtFL1iEBelEtlr43H4eOwzU6gUCedSYdQvJYTXIn2_rc8E}";
        #region construct
        /// <summary>
        /// 参数化构造函数
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <param name="buttonType">菜单按钮类型</param>
        /// <param name="value">按钮的键值（Click)，或者连接URL(View)</param>
        public MenuInfo(string name, ButtonType buttonType, string value)
        {
            this.Name = name;
            this.Type = buttonType.ToString();

            if (buttonType == ButtonType.Click)
            {
                this.Key = value;
            }
            else if (buttonType == ButtonType.View)
            {
                this.Url = value;
            }
        }

        /// <summary>
        /// 参数化构造函数,用于构造子菜单
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <param name="subButton">子菜单集合</param>
        public MenuInfo(string name, IEnumerable<MenuInfo> subButton)
        {
            this.Name = name;
            this.SubButton = new List<MenuInfo>();
            this.SubButton.AddRange(subButton);
        }
        #endregion

        #region property
        /// <summary>
        /// 按钮描述，既按钮名字，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        
        public string Name { get; set; }

        /// <summary>
        /// 按钮类型（click或view）
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        /// 按钮KEY值，用于消息接口(event类型)推送，不超过128字节
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }

        /// <summary>
        /// 网页链接，用户点击按钮可打开链接，不超过256字节
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        /// <summary>
        /// 子按钮数组，按钮个数应为2~5个
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MenuInfo> SubButton { get; set; }
        #endregion

        public static string CreateMenu()
        {
            string menu = string.Empty;
            menu =
                " {" +
    " \"button\":[" +
     "{" +
         " \"type\":\"click\"," +
         " \"name\":\"今日歌曲\"," +
         "\"key\":\"V1001_TODAY_MUSIC\"" +
      "}]" +
            " }";

            menu = JsonConvert.SerializeObject(menu);
            HttpHelper.HttpPost(CreateMenuUrl,menu);
            return menu;
        }
    }
       

}