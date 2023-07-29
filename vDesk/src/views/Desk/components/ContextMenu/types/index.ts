export type MenuCallback<T = any> = () => T
export interface ContextMenuModel {
  id: number;
  /**显示文本 */
  name: string;
  /**显示图片 */
  icon: string;
  /**点击事件 */
  click?: MenuCallback;
  /**字节点 */
  children?: Array<ContextMenuModel>;
  /**提示文本 */
  tip?: string | ((key?: string) => string);
  /**是否隐藏 */
  hidden?: boolean | ((key?: string) => boolean);
  /**类型,普通横向，图片，按钮等 */
  type?: MenuItemTypeMenu;
  /**是否禁用 */
  disabled:boolean | ((key?: string) => boolean);
}

export enum MenuItemTypeMenu {
  ROW
}