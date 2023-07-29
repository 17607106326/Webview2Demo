/**NavTabs设置接口 */
export interface INavTabsSettings {
  /**中间navtabs的宽度 */
  OccupyWidth: number;
  /**navtabs 数据集合 */
  NavTabsData: NavTabsData[];
  /**记录当前选中的标签 */
  CurrentTabIndex:number;
}

/**navtabs 数据 */
export interface NavTabsData {
  /**分类名称 */
  TabsName: string;
  /**分类索引 */
  TabsId: number;
  /**显示图标 */
  TabsIcon: string;
  /**该分类下的链接标签集合 */
  TabsLink: TabsLinks[];
}

/**链接标签集合 */
export interface TabsLinks {
  /**显示名称 */
  name: string;
  /**id */
  id: number;
  /**占几列 */
  gcspan: number;
  /**占几行 */
  grspan: number;
}