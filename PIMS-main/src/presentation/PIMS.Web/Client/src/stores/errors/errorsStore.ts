// @ts-check
import { defineStore, acceptHMRUpdate } from 'pinia'

export type errorInfo = {
  type: string | null
  title: string | null
  status: number | null
  traceId: string | null
  errorCodes: string[]
}

function mapError(
  type: string,
  title: string,
  status: number,
  traceId: string,
  errorCodes: string[]
): errorInfo {
  const error: errorInfo = {
    type: type,
    title: title,
    status: status,
    traceId: traceId,
    errorCodes: errorCodes
  }

  return error
}

const errorsStore = defineStore({
  id: 'errorsStore',
  state: (): errorInfo => ({
    type: '',
    title: '',
    status: 0,
    traceId: '',
    errorCodes: []
  }),

  actions: {
    async writeError(
      type: string,
      title: string,
      status: number,
      traceId: string,
      errorCodes: string[]
    ) {
      this.$patch(mapError(type, title, status, traceId, errorCodes))
    }
  }
})
export default errorsStore

if (import.meta.hot) {
  import.meta.hot.accept(acceptHMRUpdate(errorsStore, import.meta.hot))
}
