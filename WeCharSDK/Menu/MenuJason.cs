using System.Collections.Generic;

namespace weixinweb.Menu
{

    /// <summary>
    /// 菜单的Json字符串对象
    /// </summary>
    public class MenuJson
    {
        public List<MenuInfo> Button { get; set; }

        public MenuJson()
        {
            Button = new List<MenuInfo>();
        }
    }

    /// <summary>
    /// 菜单列表的Json对象
    /// </summary>
    public class MenuListJson
    {
        public MenuJson Menu { get; set; }
    }
}