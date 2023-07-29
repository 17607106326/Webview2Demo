import { IAppSettings } from "~/types/app";
import { MainTabPositionEnum } from "~/enums/appEnums";

/**全局默认设置 */
export const AppSettings: IAppSettings = {
  ShowMainTab: false,
  MainTabPosition: MainTabPositionEnum.LEFT,
  MainTabs: [
    { path: '/navTabs', title: '首页', icon: 'daohang' },
    { path: '/collect', title: '收藏', icon: 'daohang' },
    { path: '/docs', title: '文档', icon: 'daohang' },
    { path: '/tasks', title: '任务', icon: 'daohang' },
    { path: '/links', title: '链接', icon: 'daohang' },
    { path: '/notes', title: '笔记', icon: 'daohang' },
    { path: '/bugs', title: 'BUGS', icon: 'daohang' },
  ],
  /**选中的 MainTabs */
  MainTabsActive: { path: '/navTabs', title: '标签页', icon: 'daohang' },
}