import { useNavTabsStore } from "~/store/NavTabs";
import { TabsLinks } from "~/types/navTabs";

export const useNavPanel = (() => {
  const navTabsStore = useNavTabsStore();
  //拖拽开始的事件
  const onStart = () => {
    console.log('开始拖拽：' + JSON.stringify(navTabsStore.getNavTabsData));
  };

  //拖拽结束的事件
  const onEnd = () => {
    console.log('结束拖拽');
  };

  // #region 定义右键菜单
  const contextMenuParams = ref<TabsLinks>({} as any);
  const contextMenuEv = ref<MouseEvent>({} as any);
  const ShowContextMenu = ref<boolean>(false);

  /**右键菜单事件（拖拽之后右键传递的参数不对，所以这里单独写右键菜单） */
  const ShowContextMenuFn = ($event: MouseEvent, item: TabsLinks) => {

    $event.preventDefault();
    $event.stopPropagation();
    if(ShowContextMenu.value){
      ShowContextMenu.value = false;
    }
    contextMenuEv.value = $event;
    contextMenuParams.value = item;
    ShowContextMenu.value = true;
  };

  onMounted(() => {
    window.addEventListener('click', () => {
      ShowContextMenu.value = false;
    });
  });
  // #endregion

  return { onStart, onEnd, contextMenuParams, ShowContextMenuFn,contextMenuEv ,ShowContextMenu};
})