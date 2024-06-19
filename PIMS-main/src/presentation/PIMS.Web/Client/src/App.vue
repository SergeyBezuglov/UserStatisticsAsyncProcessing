<script  setup>
  import SearchComponent from './components/SearchComponent.vue';
  
</script>

<template>
  <v-app>
    <!-- <SearchComponent/> -->
    
    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<style>
html {
  -ms-overflow-style: none; /* IE and Edge */
  scrollbar-width: none; /* Firefox */
}
html::-webkit-scrollbar {
  display: none;
}
</style>
