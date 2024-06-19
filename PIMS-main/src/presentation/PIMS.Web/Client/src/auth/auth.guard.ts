import authStore from '@/stores/auth/authStore'
import { type RouteLocationNormalized, type NavigationGuardNext } from 'vue-router'

import { isTokenFromLocalStorageValid } from './auth.service'
import { DefaultRouteNames } from '@/router/common/router.service'

export const authGuard = async (
  to: RouteLocationNormalized,
  from: RouteLocationNormalized,
  next: NavigationGuardNext
) => {
  console.log('authGuard')
  const authRequired = to.matched.some((record: any) => record.meta.requiresAuth)
  if (authRequired) {
    const authStoreContainer = authStore()
    if (authStoreContainer.$state.signInState.token) {
      next()
      return
    } else if (isTokenFromLocalStorageValid()) {      
      next()
      return
    }
    // const authResult = await authStoreContainer.challengeAuth()
    // if (!authResult.authenticated) {
    //   next(authResult.path)
    // }
    next({ name: DefaultRouteNames.LoginForms })
  }

  next()
}
