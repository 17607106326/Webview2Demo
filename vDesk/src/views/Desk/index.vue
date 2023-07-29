<template>
  <div class="nav-tabs-container">
    <div @click="RunExe">123132</div>
  </div>
</template>

<script lang="ts" setup>
import { WV2Call, WV2Model } from '@/utils/webview2';
import { computed, reactive, ref } from 'vue';
import { useDesk } from './composables/useDesk';

const ExeInfos = ref([] as any);
const { RunExe } = useDesk(ExeInfos);


</script>

<style lang="scss" scoped></style>
