<template>
  <div class="nav-panel-container">
    <div class="nav-panel-left">
      <div class="item-ul">
        <div :class="['item-li', activeTab === item.TabsId ? 'active' : '']" v-for="(item, index) in navTabsData"
          :key="index" @click="tabItemClick(item)">
          <div class="item-li-img">
            <SvgIcon :name="item.TabsIcon"></SvgIcon>
          </div>
          <div class="item-li-title"> {{ item.TabsId }}/{{ activeTab }}</div>
        </div>
      </div>
    </div>
    <div class="nav-panel-center" ref="draggableContainerRef" @scroll="OrderScroll($event)">
      <draggable :list="tabsLinks" ref="draggableRef" ghost-class="ghost" chosen-class="chosenClass" animation="200"
        class="navs-grid" @start="onStart" @end="onEnd" :style="{ height: style.navsGridHeight }">
        <template #item="{ element }">
          <div class="grid-item" :style="{
            gridRow: `span ${element.gcspan}`,
            gridColumn: `span ${element.grspan}`,
          }" :span="`${element.gcspan}/${element.grspan}`" @contextmenu="ShowContextMenuFn($event, element)">
            {{ element.name }}
          </div>
        </template>
      </draggable>
    </div>
    <div class="nav-panel-right"></div>

    <!-- 右键菜单 -->
    <ContextMenu :menus="menus" :mouseEvent="contextMenuEv" v-show="ShowContextMenu"></ContextMenu>
  </div>
</template>

<script lang="ts" setup name="NavPanel">
import draggable from 'vuedraggable';
import { useNavPanel } from './composables/useNavPanel';
import { useNavTabsStore } from '@/store/desk';
import { NavTabsData, TabsLinks } from '@/types/navTabs';
import ContextMenu from '../ContextMenu/index';
import { storeToRefs } from 'pinia';
import { nextTick, onBeforeUnmount, onUpdated, reactive, ref, watch } from 'vue';

const navTabsStore = useNavTabsStore();
const draggableRef = ref();
const draggableContainerRef = ref();
const style = reactive({
  /**高度 */
  navsGridHeight: '100%',
});

onUpdated(() => {
  GetNavsGridHeight();
});

/**navtabs 数据集合 find((d) => d.TabsId === navTabsStore.getCurrentTabIndex) */
const navTabsData = ref<NavTabsData[]>(navTabsStore.getNavTabsData);

const activeTab = ref(navTabsStore.getCurrentTabIndex);
const tabsLinks = ref<TabsLinks[]>(navTabsStore.getNavTabsData[navTabsStore.getCurrentTabIndex].TabsLink);
watch(
  () => navTabsStore.getCurrentTabIndex,
  (newValue, oldValue) => {
    activeTab.value = newValue;
    tabsLinks.value = navTabsStore.getNavTabsData[navTabsStore.getCurrentTabIndex].TabsLink;
  },
  { deep: true },
);

const tabItemClick = (item: NavTabsData) => {
  navTabsStore.setCurrentTabIndex(item.TabsId);
  const links = navTabsStore.getNavTabsData.find((d) => {
    return d.TabsId === item.TabsId;
  })?.TabsLink!;
  tabsLinks.value = links;
};

const { onStart, onEnd, contextMenuParams, ShowContextMenuFn, contextMenuEv, ShowContextMenu } = useNavPanel();

/**高度计算 */
const GetNavsGridHeight = () => {
  nextTick(() => {
    const children = draggableRef.value.$el.children;
    // 找到最靠下面的一个
    let targetItem: HTMLElement = null as any;
    children.forEach((item: HTMLElement) => {
      if (targetItem !== null) {
        const itemRow = GetSpanRow(item);
        const targetItemRow = GetSpanRow(targetItem);
        if (item.offsetTop + itemRow > targetItem.offsetTop + targetItemRow) {
          targetItem = item;
        }
      } else {
        targetItem = item;
      }
    });
    const lastSpan = GetSpanRow(targetItem);
    const height = targetItem!.offsetTop + lastSpan * 90;
    style.navsGridHeight = `${height}px`;
  });
};

const GetSpanRow = (item: HTMLElement): number => {
  const attrs = item.attributes as any;
  const span = attrs['span'].nodeValue;
  const col = span.split('/')[0];
  const row = span.split('/')[1];
  return row;
};

watch(
  () => tabsLinks.value,
  () => {
    GetNavsGridHeight();
  },
  { deep: true, immediate: true },
);

const OrderScroll = (ev: any) => {
  //获取dom滚动距离
  const scrollTop = ev.target.scrollTop;
  //获取可视区高度
  const offsetHeight = ev.target.offsetHeight;
  //获取滚动条总高度
  const scrollHeight = ev.target.scrollHeight;

  console.log('scrollTop:' + scrollTop + ',offsetHeight:' + offsetHeight + ',scrollHeight:' + ev.target.scrollHeight);

  if (scrollTop + offsetHeight >= scrollHeight) {
    navTabsStore.setCurrentTabIndex(navTabsStore.getCurrentTabIndex + 1);
    console.log('scrollTop111111111111');
    draggableContainerRef.value.removeEventListener('scroll', () => { });
  }
}

onBeforeUnmount(() => {
  // 离开当前组件别忘记移除事件监听
  draggableContainerRef.value.removeEventListener('scroll', () => { });
});

// 右键菜单的结构及事件
const menus = [
  {
    id: 1,
    name: '变大',
    icon: 'daohang',
    click: () => {
      const cs = navTabsData.value[navTabsStore.getCurrentTabIndex].TabsLink.filter((d) => {
        return d.id == contextMenuParams.value.id;
      });
      cs[0].gcspan = 2;
      cs[0].grspan = 2;
      // GetNavsGridHeight();
    },
  },
  {
    id: 1,
    name: '变小',
    icon: 'daohang',
    click: () => {
      const cs = navTabsData.value[navTabsStore.getCurrentTabIndex].TabsLink.filter((d) => {
        return d.id == contextMenuParams.value.id;
      });
      cs[0].gcspan = 1;
      cs[0].grspan = 1;
      GetNavsGridHeight();
    },
  },
  {
    id: 1,
    name: '编辑编辑编辑',
    icon: 'daohang',
    click: () => { },
  },
] as ContextMenuModel[];
</script>

<style lang="scss" scoped>
.nav-panel-container {
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;

  .nav-panel-left {
    width: 60px;
    max-height: calc(100% - 60px);
    border-radius: 30px;
    margin-top: 30px;
    background-color: rgba(227, 230, 235, 0.1);

    &:hover {
      background-color: rgba(227, 230, 235, 0.3);
    }

    .item-ul {
      width: 100%;
      list-style: none;
      height: calc(100% - 80px);
      display: flex;
      flex-direction: column;
      align-items: center;
      padding: 40px 0;

      .item-li {
        width: 100%;
        height: 50px;
        cursor: pointer;
        text-align: center;

        &.active {
          background-color: #8b5fbf;
        }

        &-img {
          font-size: 20px;
          padding: 5px 0 0 0;
        }

        &-title {
          font-size: 10px;
          padding: 0 0 5px 0;
        }
      }
    }
  }

  .nav-panel-center {
    width: 100%;
    overflow: auto;

    &::-webkit-scrollbar {
      width: 0;
    }

    .navs-grid {
      padding-top: 20px;
      width: calc(100%);
      // 最小高度，视口高度 - 顶部 - 底部
      min-height: calc(100vh - 25vh - 48px);
      // background-color: antiquewhite;
      position: relative;
      display: grid;
      grid-auto-flow: row dense;
      grid-template-columns: repeat(auto-fill, 60px);
      grid-template-rows: repeat(auto-fill, 60px);
      grid-gap: 30px 30px;
      -webkit-user-select: none;
      -moz-user-select: none;
      user-select: none;
      box-sizing: border-box;
      justify-content: center;

      .grid-item {
        background-color: aquamarine;
        cursor: pointer;
        text-align: center;
        // &:hover {
        //   cursor: move;
        // }
      }

      &.ghost {
        border: solid 1px rgb(19, 41, 239);
      }

      &.chosenClass {
        background-color: #f1f1f1;
      }
    }
  }

  .nav-panel-right {
    width: 60px;
    height: 100%;
    // background-color: rgb(185, 122, 39);
  }
}
</style>
