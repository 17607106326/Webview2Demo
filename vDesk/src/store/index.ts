import type { App } from 'vue'
import { createPinia } from 'pinia'
// import createPersistedState from 'pinia-plugin-persistedstate'
import { createPersistedStatePlugin } from 'pinia-plugin-persistedstate-2'
import localforage from 'localforage'
const pinia = createPinia()
// pinia.use(createPersistedState)
const installPersistedStatePlugin = createPersistedStatePlugin()
pinia.use(
  createPersistedStatePlugin({
    storage: {
      getItem: async (key) => {
        return localforage.getItem(key)
      },
      setItem: async (key, value) => {
        return localforage.setItem(key, value)
      },
      removeItem: async (key) => {
        return localforage.removeItem(key)
      },
    },
  })
)
export function setupStore(app: App<Element>) {
  app.use(pinia)
}

export { pinia }
