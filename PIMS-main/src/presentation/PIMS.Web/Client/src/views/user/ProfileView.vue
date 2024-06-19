<template>
  <v-row justify="center">
    <v-col col="3" sm="3" md="3" lg="3" xl="3"></v-col>
    <v-col class="d-flex justify-center" align-self="center" col="6" sm="6" md="6" lg="6" xl="6">
      <v-sheet elevation="4" class="ml-10 ml-sm-0 ml-md-0 ml-lg-0 ml-xl-0 ml-xxl-0 px-4 py-4">
        <h2>Добро пожаловать, {{ authStoreContainer.$state.user?.FirstName }}!</h2>
      </v-sheet>
    </v-col>
    <v-col col="3" sm="3" md="3" lg="3" xl="3"></v-col>
  </v-row>
</template>
<script setup lang="ts">
import authStore from '../../stores/auth/authStore'

const authStoreContainer = authStore()
</script>
