<template>
  <v-navigation-drawer expand-on-hover permanent rail>
      <div class="d-flex justify-center main-logo-only-image">
          <v-avatar>
            <v-img class="main-logo-img mx-1" src="/images/company/Logo.svg" alt="РСП"></v-img>
          </v-avatar>
      </div>
        <div class="d-flex justify-center main-logo-image-with-text">
          <v-list-item title="Работа с персоналом" subtitle="АО «СО ЕЭС»">
            <template v-slot:prepend>
              <v-avatar>
                <v-img class="main-logo-img mx-1" src="/images/company/Logo.svg" alt="РСП"></v-img>
              </v-avatar>
            </template>
          </v-list-item>
        </div>
        <v-divider class="mt-2"></v-divider>
        <ul v-for="(item, index) in menu" :key="index" class="menu">
          <MenuContext
            v-bind="{
            name: item.name,
            subMenu: item.subMenu,
            }"
          />
        </ul>
        <template v-slot:append>
          <v-divider class="mb-0 mt-2"></v-divider>
          <v-list density="compact" class="mt-0 pt-2" nav>
            <v-list-item title="Иванов Иван Иванович" subtitle="Начальник" value="profile">
              <template v-slot:prepend>
                <v-badge content="2" color="error">
                  <v-avatar>
                    <v-icon class="text-black">fa-regular fa-user</v-icon>
                  </v-avatar>
                </v-badge>
              </template>
            </v-list-item>
          </v-list>
        </template>
  </v-navigation-drawer>
</template>

<script>
import MenuContext from "./MenuContext.vue";

export default {
  data: () => ({
    menu: [
      { name: "Личный кабинет" },

      {
        name: "Графики",
        subMenu: [
          {
            name: "График сменности",
            subMenu: [
              { name: "Проект графика сменности" },
              { name: "Утверждённый график сменности" },
              { name: "График сменности (Форма №174)" },
            ],
          },
          { name: "Мониторинг активности" },
          { name: "График рабочего времени" },
          { name: "График отпусков" },
          { name: "Календари" },
        ],
      },
      { name: "Тренировки" },
      { name: "Спецподготовка" },
      { name: "Инструктажи" },
      { name: "Проверка знаний" },
      { 
        name: "Распоряжения",
        subMenu: [
          {
            name: "Журнал",
            subMenu: [
              { name: "Проекты распоряжений" },
              { name: "Распоряжения" },
            ]
          }
        ]
      },
      { name: "Повышение квалификации" },
      { name: "Подготовка по новой должности" },
      { name: "Программа периодического n" },

    ],
  }),

  components: {
    MenuContext,
  },
};
</script>

<style> 
.main-logo-img img {
  width: 30px !important;
  margin-left: 4px;
}
.v-navigation-drawer--is-hovering .main-logo-only-image {
  visibility: hidden !important;
  display: none !important;
}
.main-logo-image-with-text {
  visibility: hidden !important;
  display: none !important;
}
.v-navigation-drawer--is-hovering .main-logo-image-with-text {
  visibility: visible !important;
  display: flex !important;
}
</style>