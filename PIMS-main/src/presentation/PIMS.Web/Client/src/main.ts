import './assets/main.css'
import '@fortawesome/fontawesome-free/css/all.css'
import '@fortawesome/fontawesome-free/js/all.js'
import 'vuetify/styles'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

// @ts-ignore
import App from './App.vue'
import router from './router'

import { createVuetify, type ThemeDefinition } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import { aliases, fa } from 'vuetify/iconsets/fa'
import { ru } from 'vuetify/locale'

const CrownTheme: ThemeDefinition = {
  dark: false,
  colors: {
      background: "#f6f7f8",
      primary: "#976fbf",
      secondary: "#FFFFFF",
      title_article:"#535257",
      desc_article:"#717075",
      error: "#F26D6D",
      info: "#2196F3",
      success: "#038C7F",
      warning: "#f77f00",
  }
}

const vuetify = createVuetify({
  components,
  directives,
  icons: {
    defaultSet: 'fa',
    aliases,
    sets: {
      fa
    }
  },
  locale: {
    locale: 'ru',
    fallback: 'en',
    messages: { ru }
  },
  theme: {
    defaultTheme: 'CrownTheme',
    themes: {
      CrownTheme
    }
  }
})

const pinia = createPinia();
const app = createApp(App)

app.use(pinia)
app.use(router)
app.use(vuetify)

app.mount('#app')
