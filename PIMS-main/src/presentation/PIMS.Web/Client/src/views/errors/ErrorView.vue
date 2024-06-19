<!-- eslint-disable vue/require-v-for-key -->
<template>
  <main>
    <h1>В процессе выполнения возникла ошибка</h1>
    <div>Тип: {{ errorsStoreContainer.type }}</div>
    <div>Заголовок: {{ errorsStoreContainer.title }}</div>
    <div>Номер : {{ errorsStoreContainer.status }}</div>
    <div>Trace Id : {{ errorsStoreContainer.traceId }}</div>
    <ul id="example-1">
      <li v-for="error in errorsStoreContainer.errorCodes">
        {{ error }}
      </li>
    </ul>
  </main>
</template>
<script setup lang="ts">
import errorsStore from '../../stores/errors/errorsStore'
const errorsStoreContainer = errorsStore()
// @ts-ignore
window.stores = { errorsStoreContainer }
</script>
