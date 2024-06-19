<template>
    <v-list> 
        <v-list-group>
          <template v-slot:activator="{ props }">
            <v-list-item v-bind="props" prepend-icon="mdi-home" class="tile">
                <v-list-item-title>{{ name }}</v-list-item-title>
            <!-- <template v-slot:prepend>
              <v-icon class="text-black">fa-solid fa-house</v-icon>
            </template> -->
            </v-list-item>
          </template>    
        <ul class="subMenu" v-if="subMenu.length" >
            <v-list-item v-for="(item, index) in subMenu" :key="index">
                <recursive-menu
                    v-bind="{
                    name: item.name,
                    subMenu: item.subMenu,
                    }"
                />
            </v-list-item>
        </ul>
      </v-list-group>
    </v-list> 
</template>


  <script >
  export default {
    name: "recursive-menu",
    props: {
      name: {
        type: String,
        required: true,
      },
      subMenu: {
        type: Array,
        default: () => [],
      },
    },
  };
  </script>
  
<style>
.v-list-group {
    --prepend-width: 0!important;
}
.v-list-item__content {
    overflow: inherit!important;
    width: 100%;
}
.v-list-item-title {
    white-space: inherit!important;
}
</style>