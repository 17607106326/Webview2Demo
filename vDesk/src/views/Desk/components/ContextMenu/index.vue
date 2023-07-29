<template>
  <div class="context-menu" :style="style">
    <div v-for="(item, index) in props.menus" :key="index">
      <div v-if="item.type === MenuItemTypeMenu.ROW || item.type === undefined" class="item-menu-row" @click="handleMenuItemClick(item)">
        <div class="item-menu-row-icon"><SvgIcon :name="item.icon"></SvgIcon></div>
        <div class="item-menu-row-title">{{ item.name }}</div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ContextMenuModel, MenuItemTypeMenu } from './types';
const props = defineProps({
  /**右键菜单数据 */
  menus: {
    type: Array as PropType<Array<ContextMenuModel>>,
    require: true,
    default: [],
  },
  mouseEvent: {
    type: Object as PropType<MouseEvent>,
    default: null,
  },
});

const style = ref('');
const mouseEventValue = toRef(props, 'mouseEvent');
watch(
  () => mouseEventValue.value,
  () => {
    const top = props.mouseEvent.clientY;
    const left = props.mouseEvent.clientX;
    style.value = `position:absolute;z-index: 999; top:${top}px; left:${left}px;`;
  },
);

const handleMenuItemClick = (item: ContextMenuModel) => {
  if (item.disabled) return;
  if (item.click && typeof item.click === 'function') {
    item.click();
  }
};
</script>

<style lang="scss" scoped>
.context-menu {
  user-select: none;
  padding: 10px;
  border-radius: 5px;
  background-color: #ffffff;

  .item-menu-row {
    padding: 5px;
    border-radius: 3px;
    display: flex;
    flex-direction: row;
    cursor: pointer;
    &:nth-child(n + 2) {
      margin-top: 2px;
    }
    &-icon {
      margin: 0 5px 0 0;
    }
    &-title {
      font-size: 14px;
      color: #000000;
      margin-right: 3px;
    }
    &:hover {
      background-color: #968888;
    }
  }
}
</style>