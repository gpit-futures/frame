<template>
<div>
    <webview v-for="client of clients" :key="client.id" class="frames"
        v-bind:class="{'hide-webview': selected !== client.id}"
        :id="client.id"
        :src="client.sourceUrl"
        :ref="client.applicationName"
        preload="./utilities/client-connector-preload.js"></webview>
</div>
</template>

<script>
import { setupListeners } from "../utilities/client-manager";

export default {
  name: "frames",
  components: {},
  data() {
    return {
      // Some mock data to fill the page
      framesTitle: "Frame"
    };
  },
  computed: {
    selected() {
      return this.$store.state.selected;
    },
    clients() {
      return this.$store.state.clients;
    },
  },
  mounted() {
    setTimeout(() => {
      let webviews = this.$refs;
      setupListeners(webviews);
    }, 1000);
  }
};
</script>
