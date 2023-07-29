<template>
  <div class="nav-header-bar">
    <div class="time-box">
      <div class="time-box-time">
        <div class="text">{{ time }}</div>
      </div>
      <div class="time-box-date">{{ date }} {{ week }}</div>
    </div>
    <div class="search-box">
      <div class="search-input">
        <div class="select" @click="selectEngine">
          <div class="select-icon"></div>
          <div class="select-sx">
            <SvgIcon v-if="selectEngineIcon" name="shangla"></SvgIcon>
            <SvgIcon v-else name="xiala"></SvgIcon>
          </div>
        </div>
        <div class="input">
          <input type="text" placeholder="请输入搜索内容" v-model="searchContent" />
          <div class="input-close" v-show="closeTextShow" @click="resetText">
            <SvgIcon name="guanbi" :fontSize="18"></SvgIcon>
          </div>
        </div>
        <div class="search">
          <SvgIcon name="sousuo" :fontSize="24"></SvgIcon>
        </div>
      </div>
    </div>
  </div>

  <Dialog :visible="selectEngineIcon" :width="810" :top="260" :draggable="true" :modal="true" :header="false"
    :footer="false" @update:visible="selectEngineIcon = !selectEngineIcon">
    <div class="selectengine">
      <div class="item" v-for="(item, index) in 8">
        {{ index }}
      </div>
    </div>
  </Dialog>
</template>

<script lang="ts" setup name="NavHeader">
/**当前时间 */
const { date, time, week, GetRealTime } = useDateTime();
onMounted(() => {
  // GetRealTime();
});

/**搜索框的输入文本 */
const searchContent = ref('');

/**选择引擎的图标状态 */
const selectEngineIcon = ref(false);
/**选择搜索引擎 */
const selectEngine = () => {
  selectEngineIcon.value = !selectEngineIcon.value;
};

/**是否展示清除文本图标 */
const closeTextShow = ref(false);

watch(
  () => searchContent.value,
  () => {
    if (searchContent.value.length > 0) {
      closeTextShow.value = true;
    } else {
      closeTextShow.value = false;
    }
  },
  { immediate: true },
);

/**清空文本 */
const resetText = () => {
  searchContent.value = '';
}
</script>

<style lang="scss" scoped>
.nav-header-bar {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  user-select: none;

  .time-box {
    width: 100%;
    height: 160px;
    display: flex;
    flex-direction: column;
    color: white;

    &-time {
      width: 100%;
      height: 140px;

      .text {
        height: 80px;
        text-align: center;
        line-height: 80px;
        margin-top: 60px;
        font-size: 80px;
        font-family: 'Digiface Regular';
      }
    }

    &-date {
      text-align: center;
      font-size: 20px;
      height: 20px;
      line-height: 20px;
    }
  }

  .search-box {
    width: 100%;
    height: 70px;
    display: flex;
    justify-content: center;
    align-items: last baseline;

    .search-input {
      width: 700px;
      height: 50px;
      // background-color: rgba(255, 255, 255, 0.7);
      // backdrop-filter: blur(18px);
      box-shadow: 0 0 10px 3px #d4cfcf1a;
      display: flex;
      border-radius: 50px;

      .select {
        width: 50px;
        border-radius: 50px 0 0 50px;
        display: flex;

        &:hover {
          background-color: #d4cfcf1a;
        }

        &-icon {
          width: 35px;
        }

        &-sx {
          display: flex;
          justify-content: center;
          align-items: center;
          width: 15px;
        }
      }

      .input {
        width: calc(100% - 50px - 50px);
        display: flex;

        input {
          width: calc(100% - 5px - 5px - 30px);
          border: none;
          margin-left: 5px;
          height: 50px;
          font-size: 16px;
          box-sizing: border-box;
          background-color: transparent;
          line-height: 50px;
          caret-color: antiquewhite;
          color: aliceblue;

          &:focus {
            outline: none;
            border: none;
          }
        }

        .input-close {
          width: 30px;
          display: flex;
          justify-content: center;
          align-items: center;
          transition: transform 0.2s;
          cursor: pointer;
        }
      }

      .search {
        width: 50px;
        border-radius: 0 50px 50px 0;
        display: flex;
        justify-content: center;
        align-items: center;
        transition: transform 0.2s;

        &:hover {
          background-color: #d4cfcf1a;
        }
      }
    }
  }
}

.selectengine {
  display: flex;
  .item{
    width: 100px;
    height: 100px;
    background-color: #b421211a;
  }
}
</style>