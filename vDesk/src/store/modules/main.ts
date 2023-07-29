/**
 * 存储全局配置信息
 */
import { defineStore } from 'pinia'
import { useRouter } from 'vue-router'
import { pinia } from '../index'
const router = useRouter()

interface appState {
  tabs: []
}

const storageName = 'main'
export const appStore = defineStore({
  id: storageName,
  state: (): appState => ({}),
  getters: {

  },
  actions: {

  },
})

export function useAppStore() {
  return appStore(pinia)
}
