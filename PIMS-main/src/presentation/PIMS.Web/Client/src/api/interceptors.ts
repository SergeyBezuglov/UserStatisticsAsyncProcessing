import { getToken } from '@/auth/auth.service'
import errorsStore from '@/stores/errors/errorsStore'
import { DefaultRouteSettings} from '@/router/common/router.service'
import { type AxiosInstance } from 'axios'
import router from '@/router'
export function interceptorsInit(axiosInstance: AxiosInstance): AxiosInstance {
  axiosInstance.interceptors.request.use(function (options) {
    const jwtToken = getToken()
    if (jwtToken) {
      options.headers['Authorization'] = `Bearer ${jwtToken}`
    }
    return options
  })

  axiosInstance.interceptors.response.use(
    (response) => response,
    (error) => {
      const errorsStoreContainer = errorsStore()
      if (error.response && error.response.data) {
        errorsStoreContainer.writeError(
          error.response.data.Type,
          error.response.data.Title,
          error.response.data.Status,
          error.response.data.traceId,
          error.response.data.errorCodes
        )
      }
      router.push({ name: DefaultRouteSettings.Error.Name })
    }
  )

  return axiosInstance
}
