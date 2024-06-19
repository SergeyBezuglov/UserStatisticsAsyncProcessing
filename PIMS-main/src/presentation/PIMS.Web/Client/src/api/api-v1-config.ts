import axios from 'axios'
import { interceptorsInit } from '@/api/interceptors'
import { type AxiosInstance } from 'axios'
export default function apiInit(mode: string): AxiosInstance {
  const debug = mode !== 'production'
  const baseURL = debug ? 'api/v1.0/' : 'api/v1.0/'
  const api = interceptorsInit(axios.create({ baseURL }))
  return api
}
