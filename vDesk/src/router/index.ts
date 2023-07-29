import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
export const LAYOUT = () => import('@/layouts/index.vue');

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'Index',
    component: LAYOUT,
    meta: {
      title: 'vDesk',
    },
    redirect:'Home',
    children: [
      {
        path: '/Home',
        name: 'Home',
        component: () => import('@/views/Home/index.vue'),
        meta: {
          title: 'Home',
        },
      },
      {
        path: '/Desk',
        name: 'Desk',
        component: () => import('@/views/Desk/index.vue'),
        meta: {
          title: 'Desk',
        },
      },
      {
        path: '/Note',
        name: 'Note',
        component: () => import('@/views/Note/index.vue'),
        meta: {
          title: 'Note',
        },
      },
    ]
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

// 全局解析守卫
router.beforeResolve((to, from, next) => {
  // 动态设置标签页标题
  document.title = to.meta.title as string
  next()
})
export default router
