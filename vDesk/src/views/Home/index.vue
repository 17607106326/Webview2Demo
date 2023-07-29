<template>
  <div class="home-container">
    <div class="left">
      <div class="hello">{{ HelloText }}</div>
      <div class="info1">
        <div class="weather"></div>
        <div class="disk"></div>
      </div>
    </div>
    <div class="right"></div>
  </div>
</template>

<script lang="ts" setup>
import { useHome } from './composables/useHome';
const { HelloText } = useHome();

</script>

<style lang="scss" scoped>
.home-container {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: row;

  .left {
    flex: 2;

    .hello {
      width: 50%;
      height: 100px;
      font-size: 44px;
      line-height: 100px;
      text-align: center;
    }

    .info1 {
      width: calc(100% - 20px);
      height: 150px;
      margin: 10px;
      border-radius: 10px;
      display: flex;
      flex-direction: row;

      .weather {
        width: 60%;
        height: 100%;
        border-radius: 10px;
        background-color: rgb(128, 84, 27);
      }

      .disk {
        width: calc(40% - 10px);
        margin-left: 10px;
        height: 100%;
        border-radius: 10px;
        background-color: rgb(128, 84, 27);
      }
    }
  }

  .right {
    flex: 1;
    background-color: rgb(24, 16, 6);
  }
}
</style>
