using weixinweb.Menu;

namespace weixinweb.API
{
    public interface IMenuApi
    {
        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        MenuJson GetMenu(string accessToken);

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="menuJson">菜单对象</param>
        /// <returns></returns>
      //  CommonResult CreateMenu(string accessToken, MenuJson menuJson);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
       // CommonResult DeleteMenu(string accessToken);
    }
}