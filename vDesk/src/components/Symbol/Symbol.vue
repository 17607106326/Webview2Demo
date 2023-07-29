<template>
  <svg class="icon svg-icon" aria-hidden="true">
    <use :xlink:href="localData.fontName" :fill="props.color"></use>
  </svg>
</template>

<script lang="ts" setup>
import { watch, reactive } from 'vue'
const props = defineProps({
  /**
   * 字体大小,带 px
   */
  fontSize: {
    type: String,
    default: '16px',
  },
  /**
   * 颜色
   */
  color: {
    type: String,
    default: '',
  },
  /**
   * 字体名称 带 #
   */
  fontName: {
    type: String,
    default: '',
  },
})

const localData = reactive({
  fontName: '#' + props.fontName,
  fontSize: props.fontSize + 'px',
})

watch(
  () => props.fontName,
  (newValue, oldValue) => {
    localData.fontName = '#' + newValue
  }
)

watch(
  () => props.fontSize,
  (newValue, oldValue) => {
    localData.fontSize = newValue + 'px'
  }
)
</script>

<style lang="scss" scoped>
/* Symbol字体公共样式 */
.icon {
  width: 1em;
  height: 1em;
  vertical-align: -0.15em;
  fill: currentColor;
  overflow: hidden;
  align-items: center;
  font-size: v-bind('localData.fontSize');
}
</style>
