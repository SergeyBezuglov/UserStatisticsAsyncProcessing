<template>
  <v-row justify="center">
    <v-col col="3" sm="3" md="3" lg="3" xl="3"></v-col>
    <v-col class="d-flex justify-center" align-self="center" col="6" sm="6" md="6" lg="6" xl="6">
      <v-sheet
        elevation="4"
        min-width="300"
        max-width="500"
        class="ml-10 ml-sm-0 ml-md-0 ml-lg-0 ml-xl-0 ml-xxl-0 px-4 py-4"
      >
        <v-form ref="form">
          <v-text-field
            v-model="register.userName"
            :counter="commonValidatorsCore.UserName.Length"
            :rules="commonValidatorsCore.UserName.Rules"
            label="Имя пользователя"
            required
          ></v-text-field>
          <v-text-field
            v-model="register.password"
            :rules="commonValidatorsCore.Required.Rules"
            type="password"
            label="Укажите пароль"
            counter
          ></v-text-field>
          <v-text-field
            v-model="register.lastName"
            :counter="commonValidatorsCore.LastName.Length"
            :rules="commonValidatorsCore.LastName.Rules"
            label="Фамилия"
            required
          ></v-text-field>
          <v-text-field
            v-model="register.firstName"
            :counter="commonValidatorsCore.FirstName.Length"
            :rules="commonValidatorsCore.FirstName.Rules"
            label="Имя"
            required
          ></v-text-field>
          <v-text-field
            v-model="register.middleName"
            :counter="commonValidatorsCore.MiddleName.Length"
            :rules="commonValidatorsCore.MiddleName.Rules"
            label="Отчество"
            required
          ></v-text-field>
          <v-text-field
            v-model="register.email"
            :counter="commonValidatorsCore.Email.Length"
            :rules="commonValidatorsCore.Email.Rules"
            label="E-mail"
            type="email"
            required
          ></v-text-field>
        </v-form>

        <div>
          <v-btn block type="submit" class="mb-8 bg-primary" size="large" @click="onLogin">
            Войти
          </v-btn>
        </div>
      </v-sheet>
    </v-col>
    <v-col col="3" sm="3" md="3" lg="3" xl="3"></v-col>
  </v-row>
</template>
<script setup lang="ts">
import { ref } from 'vue'
import authStore from '../../stores/auth/authStore'
import { DefaultRouteSettings } from '../../router/common/router.service'
import { commonValidatorsCore } from '../../validators/commonValidators'
import router from '../../router'
const register = ref({
  firstName: '',
  middleName: '',
  lastName: '',
  email: '',
  userName: '',
  password: ''
})
const authStoreContainer = authStore()

async function onLogin() {
  const registerResult = await authStoreContainer.registerUsingForms({
    UserName: register.value.userName,
    Password: register.value.password,
    FirstName: register.value.firstName,
    MiddleName: register.value.middleName,
    LastName: register.value.lastName,
    Email: register.value.email
  })
  if (registerResult.authenticated) {
    router.push({ name: DefaultRouteSettings.Search.Name })
  }
}
</script>
