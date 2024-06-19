// @ts-check
import { defineStore, acceptHMRUpdate } from 'pinia'
import { testAccess } from '@/common/api/common.api.service'

const testAccessStore = defineStore({
  id: 'testAccess',
  state: () => ({
    testMessage: ''
  }),

  actions: {
    clearTestMessage() {
      this.$patch({
        testMessage: ''
      })

      // we could do other stuff like redirecting the user
    },

    /**
     * Attempt to login a user
     */
    async sendTestMessage(testMessage: string): Promise<string> {
      const testAnswerResult = await testAccess(testMessage)

      this.$patch({
        testMessage: testAnswerResult.data
      })
      return testAnswerResult.data
    }
  }
})
export default testAccessStore

if (import.meta.hot) {
  import.meta.hot.accept(acceptHMRUpdate(testAccessStore, import.meta.hot))
}
