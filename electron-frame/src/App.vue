<template>
  <div id="app" class="dw">
    <div class="header-space"></div>
    <!-- info bar component -->
      <info-bar id="title-bar" class="dw-info-bar columns"></info-bar>
    <div class="router-view">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
import InfoBar from "./components/Info-Bar";

export default {
  name: "App",
  components: {
    InfoBar
  },
  data() {
    return {
      text: ""
    };
  }
};
</script>