import { createRouter, createWebHistory } from 'vue-router'
import { authGuard } from '@/auth/auth.guard'
import { DefaultRouteSettings } from '@/router/common/router.service'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: DefaultRouteSettings.Search.Path,
      name: DefaultRouteSettings.Search.Name,
      component: () => import('../views/SearchView.vue'),
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue'),
      meta: {
        requiresAuth: false
      }
    },
    {
      path: DefaultRouteSettings.Error.Path,
      name: DefaultRouteSettings.Error.Name,
      component: () => import('../views/errors/ErrorView.vue'),
      meta: {
        requiresAuth: false
      }
    },
    {
      path: DefaultRouteSettings.LoginForms.Path,
      name: DefaultRouteSettings.LoginForms.Name,
      component: () => import('../views/auth/LoginView.vue'),
      meta: {
        requiresAuth: false
      }
    },
    {
      path: DefaultRouteSettings.RegisterForms.Path,
      name: DefaultRouteSettings.RegisterForms.Name,
      component: () => import('../views/auth/RegisterView.vue'),
      meta: {
        requiresAuth: false
      }
    },
    {
      path: '/testdoc',
      name: 'testdoc',
      component: import('../views/test/DocumentTest.vue'),
      meta: {
        requiresAuth: false
      }
    }
  ]
})

router.beforeEach(authGuard)

export default router
