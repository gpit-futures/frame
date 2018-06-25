<template>
  <v-app id="inspire">
    <div id="app" class="dw">
      <div class="header-space"></div>
      <!-- sidebar component -->
      <sidebar></sidebar>
      <!-- info bar component -->
      <info-bar id="title-bar"></info-bar>
      <div class="router-view">
        <router-view></router-view>
      </div>
      <notifications></notifications>
    </div>
  </v-app>
</template>

<script>
import InfoBar from "./components/Info-Bar";
import Sidebar from "./components/Sidebar.vue";
import Notifications from "./components/Notifications.vue";

export default {
  name: "App",
  components: {
    InfoBar,
    Sidebar,
    Notifications
  },
  data() {
    return {
      text: ""
    };
  }
};
</script>