<template>
  <div class="container">
    <div class="left-bar">
      <LeftBar @JumpTo="JumpTo"></LeftBar>
    </div>
    <div class="content">
      <RouterView v-slot="{ Component, route }">
        <keep-alive>
          <component :is="Component" :key="route.path" />
        </keep-alive>
      </RouterView>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { useRouter } from 'vue-router';
import LeftBar from './LeftBar/LeftBar.vue';
const router = useRouter()
const JumpTo = (item: any) => {
  router.push({
    path: item.path,
  })
}
</script>

<style lang="scss" scoped>
.container {
  width: calc(100% - 20px);
  height: calc(100vh - 20px);
  background-color: #F6F8FA;
  padding: 10px;
  display: flex;
  flex-direction: row;

  .left-bar {
    width: 60px;
    height: 100%;
    border-radius: 10px;
  }

  .content {
    width: calc(100% - 60px - 10px);
    margin-left: 10px;
    height: calc(100vh - 20px);
    background-color: #e7edf3;
    border-radius: 10px;
  }
}
</style>
