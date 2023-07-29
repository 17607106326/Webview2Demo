import { MainTabPositionEnum } from "~/enums/appEnums";

/**全局设置接口 */
export interface IAppSettings {
  /**是否展示侧边菜单栏 */
  ShowMainTab: boolean,
  /**侧边栏位置 */
  MainTabPosition: MainTabPositionEnum,
  /**侧边栏数据 */
  MainTabs: MainTabsConfig[],
  /**选中的 MainTabs */
  MainTabsActive: MainTabsConfig,
}

/**侧边栏数据 */
export interface MainTabsConfig {
  /**路由地址 */
  path: string,
  /**标题，显示用 */
  title: string,
  icon: string;
}