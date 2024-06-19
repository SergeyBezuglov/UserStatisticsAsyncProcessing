<template>
  <v-breadcrumbs :items="['Распоряжения', 'Журналы', 'Проекты']" class="main-breadcrumbs" ></v-breadcrumbs>
  <v-card>
    <v-layout>
      <v-main class="d-flex" style="height: 900px">
        <div class="column">
          <v-card width="600" height="800" title="Проект распоряжения">
            <div>
              <div class="text-subtitle-1 text-medium-emphasis">Account</div>
              <v-text-field
                density="compact"
                placeholder="Email address"
                variant="outlined"
              ></v-text-field>
              <div
                class="text-subtitle-1 text-medium-emphasis d-flex align-center justify-space-between"
              >
                Password
              </div>
              <v-text-field
                density="compact"
                placeholder="Enter your password"
                variant="outlined"
                @click:append-inner="visible = !visible"
              ></v-text-field>
              <v-btn
                block
                class="mb-8"
                color="primary"
                size="large"
                variant="tonal"
                >Accept</v-btn
              >
              <v-btn block class="mb-8" color="" size="large" variant="tonal"
                >Cancel</v-btn
              >
            </div>
          </v-card>
        </div>
      </v-main>
    </v-layout>
  </v-card>
</template>

<script lang="ts">
export default {
  data: () => ({
    visible: false,
  }),
};
</script>
