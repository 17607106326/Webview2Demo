<template>
  <div class="left-bar">
    <div class="center-menu">
      <div class="menu-item" v-for="(item, index) in leftBar" :key="index" @click="JumpTo(item)">
        <Symbol :font-name="item.icon" font-size="28" :color="curIndex === item.id ? 'red' : 'blue'"></Symbol>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup name="LeftBar">
import { ref } from 'vue'

const leftBar = [
  { id: 1, label: '首页', icon: 'icon-shouye', path: 'Home' },
  { id: 2, label: '桌面', icon: 'icon-shouye', path: 'Desk' },
  { id: 3, label: '笔记', icon: 'icon-shouye', path: 'Note' },
]

const emits = defineEmits(['JumpTo']);
const curIndex = ref(1);
const JumpTo = (item: any) => {
  curIndex.value = item.id
  emits('JumpTo', item);
}
</script>

<style lang="scss" scoped>
.left-bar {
  width: 100%;
  height: 100%;

  .center-menu {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    margin-top: 100px;

    .menu-item {
      width: 80%;
      height: 60px;
      line-height: 60px;
      text-align: center;
      border-radius: 60px;
      cursor: pointer;

      &:not(:first-child) {
        margin-top: 10px;
      }
    }
  }
}
</style>
