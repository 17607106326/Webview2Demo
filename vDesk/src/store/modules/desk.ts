import { defineStore } from "pinia";
import { NavTabsSettings } from "@/settings/navTabsSetting";
import { INavTabsSettings, NavTabsData } from "@/types/navTabs";

export const useNavTabsStore = defineStore('NavTabs', {
  state: (): INavTabsSettings => ({
    /**是否展示侧边菜单栏 */
    OccupyWidth: NavTabsSettings.OccupyWidth,
    NavTabsData: NavTabsSettings.NavTabsData,
    CurrentTabIndex: NavTabsSettings.CurrentTabIndex,
  }),
  getters: {
    /**获取中间nvatabs的宽度 */
    getOccupyWidth: (state): number => state.OccupyWidth,
    /**获取 NavTabsData数据集合 */
    getNavTabsData: (state): NavTabsData[] => state.NavTabsData,
    /**当前选中的tabs */
     getCurrentTabIndex: (state): number => state.CurrentTabIndex,
  },
  actions: {
    /**设置中间nvatabs的宽度 */
    setOccupyWidth(occupyWidth: number): void {
      this.OccupyWidth = occupyWidth;
    },
    /**设置中间nvatabs的宽度 */
    setNavTabsData(occupyWidth: number): void {
      this.OccupyWidth = occupyWidth;
    },
    /**设置当前选中的tabs */
    setCurrentTabIndex(currentTabIndex: number): void {
        this.CurrentTabIndex = currentTabIndex;
    },
  },
  persist: {
    storage: persistedState.sessionStorage,
  },
});
