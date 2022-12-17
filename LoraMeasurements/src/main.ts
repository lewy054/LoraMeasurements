import { createApp } from 'vue'
import App from './App.vue'
import { aliases, mdi } from 'vuetify/iconsets/mdi'
import {createPinia} from 'pinia'
import '@mdi/font/css/materialdesignicons.css'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import router from "@/router/router";

const vuetify = createVuetify({
    components,
    directives,
    icons: {
        defaultSet: 'mdi',
        aliases,
        sets: {
            mdi,
        }
    },
})
const pinia = createPinia();

createApp(App)
    .use(vuetify)
    .use(pinia)
    .use(router)
    .mount('#app')