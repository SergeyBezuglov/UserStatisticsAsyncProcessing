// @ts-check
import { defineStore, acceptHMRUpdate } from 'pinia'
import { DefaultRouteNames, DefaultRoutePathes } from '@/router/common/router.service'
import * as jose from 'jose'
import {
  challengeAuthFromServer,
  type FormsLoginRequest,
  type AuthenticationResponse,
  ChallengeResponseType,
  isTokenFromLocalStorageValid,
  getToken,
  authFromAD,
  authFromForms,
  type FormsRegisterRequest,
  registerFromForms,
} from '../../auth/auth.service'

type signInState = {
  userName: string | null
  exp: Date
  sub: string | null
  token: string | null
}
type challengeAuthResult = {
  path: string,
  name:string,
  authenticated: boolean
}
export type UserInfo = {
  UserName: string
  FirstName: string
  MiddleName: string
  LastName: string
  Email: string
}
export type AuthState = {
  signInState: signInState
  user: UserInfo | null
  isAuthenticated: boolean
}
function mapAuthenticationResponse(authResponse: AuthenticationResponse): UserInfo {
  const userInfo: UserInfo = {
    UserName: authResponse.UserName,
    FirstName: authResponse.FirstName,
    MiddleName: authResponse.MiddleName,
    LastName: authResponse.LastName,
    Email: authResponse.Email
  }

  return userInfo
}
function generateEmptyState(): AuthState {
  const emptyItem: AuthState = {
    user: null,
    signInState: {
      userName: '',
      exp: new Date(),
      sub: '',
      token: ''
    },
    isAuthenticated: false
  }
  return emptyItem
}

const authStore = defineStore({
  id: 'authStore',
  state: (): AuthState => generateEmptyState(),

  actions: {
    async challengeAuth(): Promise<challengeAuthResult> {
      const { data } = await challengeAuthFromServer()
      this.$patch(generateEmptyState())
      const challengeAuthResult: challengeAuthResult = {
        path: '',
        name:'',
        authenticated: false
      }
      if (data.AuthType == ChallengeResponseType.AD) {
        const { data } = await authFromAD()
        this.$patch({
          user: mapAuthenticationResponse(data)
        })
        this.updateToken(data.Token)
        challengeAuthResult.authenticated = true
      }
      if (data.AuthType == ChallengeResponseType.JWT) {
        challengeAuthResult.name = DefaultRouteNames.LoginForms
        challengeAuthResult.path=DefaultRoutePathes.LoginForms
        challengeAuthResult.authenticated = false
      }
      return challengeAuthResult
    },
    async authUsingForms(login:FormsLoginRequest): Promise<challengeAuthResult> {      
      const { data } = await authFromForms(login);
      this.$patch(generateEmptyState())
      const challengeAuthResult: challengeAuthResult = {
        path: '',
        name:'',
        authenticated: false
      }
      this.$patch({
        user: mapAuthenticationResponse(data)
      })
      this.updateToken(data.Token)
      challengeAuthResult.authenticated = true
      return challengeAuthResult
    },
    async registerUsingForms(register:FormsRegisterRequest): Promise<challengeAuthResult> {      
      const { data } = await registerFromForms(register);
      this.$patch(generateEmptyState())
      const challengeAuthResult: challengeAuthResult = {
        path: '',
        name:'',
        authenticated: false
      }
      this.$patch({
        user: mapAuthenticationResponse(data)
      })
      this.updateToken(data.Token)
      challengeAuthResult.authenticated = true
      return challengeAuthResult
    },
    async useLocalStorageTokenToSignInAction(): Promise<boolean> {
      if (!isTokenFromLocalStorageValid()) {
        return false
      }
      const tokenValue = getToken()
      this.$patch({
        signInState: {
          token: tokenValue
        }
      })
      this.updateToken(tokenValue ?? '')
      return true
    },
    updateToken(token: string) {
      const loginClaim: any = jose.decodeJwt(token ?? '')
      this.$patch({
        signInState: {
          userName: loginClaim.name,
          exp: loginClaim.exp,
          sub: loginClaim.sub
        }
      })
      localStorage.setItem('token', token ?? '')
    }
  }
})
export default authStore

if (import.meta.hot) {
  import.meta.hot.accept(acceptHMRUpdate(authStore, import.meta.hot))
}
