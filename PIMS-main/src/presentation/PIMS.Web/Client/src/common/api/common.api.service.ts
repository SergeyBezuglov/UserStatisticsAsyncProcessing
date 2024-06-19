import api from '@/api/api-base-hub'
import type { AxiosResponse } from 'axios'
export async function testAccess(testMessage: string): Promise<AxiosResponse<any, any>> {
  return await api.post(`AccessTest/testpage?TestRequestMessage=${testMessage}`)
}
