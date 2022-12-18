<template>
  <v-app>
    <v-app-bar
        id="grs-navbar"
        density="compact"
    >
      <template v-slot:prepend>
        <v-app-bar-nav-icon @click="store.changeSideBarVisibility()"></v-app-bar-nav-icon>
      </template>

      <v-spacer/>
      <v-app-bar-title>
        <v-img src="./assets/logo.png" width="100" class="mx-auto"></v-img>
        <img alt="Vue logo" src="./assets/logo.png">
      </v-app-bar-title>
      <v-spacer/>

      <template v-slot:append>
        <v-icon>mdi-account-circle</v-icon>
      </template>
    </v-app-bar>

    <v-navigation-drawer v-model="store.sideBar" absolute class="dark-transparent">
      <router-link to="/" class="reset-a" @click="store.changeSideBarVisibility()">
        <v-list-item prepend-icon="mdi-home">
          Home
        </v-list-item>
      </router-link>
      <router-link to="/devices" class="reset-a" @click="store.changeSideBarVisibility()">
        <v-list-item prepend-icon="mdi-forum">
          Urządzenia
        </v-list-item>
      </router-link>
      <router-link to="/map" class="reset-a" @click="store.changeSideBarVisibility()">
        <v-list-item prepend-icon="mdi-map">
          Mapa
        </v-list-item>
      </router-link>
    </v-navigation-drawer>
    <v-main class="body">
      <router-view/>
    </v-main>
    <v-footer app="true" width="auto" style="max-height: 50px;" id="grs-footer">
      <v-row>
        <v-col>
          LoraMeasurements © 2022 Norbert Lewicz
        </v-col>
        <v-col cols="auto" class="float-right align-content-end">
        </v-col>
      </v-row>
    </v-footer>
  </v-app>
</template>

<script setup lang="ts">
import {useMainStore} from "./stores/main";

const store = useMainStore();
</script>

<style>
html {
  position: relative;
  min-height: 100%;
  font-size: 14px;
}

@media (min-width: 768px) {
  html {
    font-size: 16px;
  }
}

.body {
  font-family: 'Poppins', Tahoma, Geneva, Verdana, sans-serif;
  background-size: cover;
  backdrop-filter: blur(20px);
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.reset-a, .reset-a:visited, .reset-a:focus, .reset-a:active {
  text-decoration: none;
  color: inherit;
  outline: 0;
}

#grs-navbar {
  background-color: #063970;
  color: white;
}

#grs-footer {
  background-color: #063970;
  color: white;
}

.reset-a:hover {
  color: white !important;
}

.v-list-item:hover{
  background-color: #063970;
}

.dark-transparent {
      background-color: #98c3f5 !important;
  }
</style>