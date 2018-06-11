<template>
  <div class="dw-container">
    <!-- sidebar component -->
    <sidebar class="dw-sidebar"></sidebar>
    <div class="dw-main">
      <!-- patient banner component -->
      <patient-banner v-if="patientContext"></patient-banner>
        <!-- patient banner suplement component -->
      <patient-banner-supplement v-if="patientContext"
        class="dw-patient-banner-supplement columns"
        :expanded=false></patient-banner-supplement>
        <!-- frame component -->
      <frames class="dw-frames"></frames>
    </div>
  </div>
</template>

<script>
// components
import Frames from "./Frames.vue";
import InfoBar from "./Info-Bar.vue";
import PatientBannerSupplement from "./Patient-Banner-Supplement.vue";
import PatientBanner from "./Patient-Banner.vue";
import Sidebar from "./Sidebar.vue";

// services
import {
  CatalogueService
} from "../services/catalogue-service";
import { UserService } from "../services/user-service";
import mutators from '../store/mutators'

export default {
  name: "home",
  components: {
    Frames,
    InfoBar,
    PatientBannerSupplement,
    PatientBanner,
    Sidebar
  },
  data() {
    return {
      // Some mock data to fill the page
    };
  },
  computed: {
    user() {
      return this.$store.state.user
    },
    patient() {
      return this.$store.state.patient
    },
    patientContext() {
      return this.$store.state.patientContext
    },
    selected() {
      return this.$store.state.selected
    },
    clients() {
      return this.$store.state.clients
    },
    practitionerName() {
      return (
        this.user.title + " " + this.user.firstName + " " + this.user.lastName
      );
    }
  },
  methods: {
  },
  async mounted() {
    let userService = new UserService();
    this.$store.commit(mutators.SET_USER, await userService.getUser(this.user.id))
    let catalogueService = new CatalogueService();
    this.$store.commit(mutators.SET_CLIENTS, await catalogueService.getClients(this.user.id))

    if (this.clients.length > 0) {
      // this.clientManager.set(this.clients);
      this.clientManager = this.clients;
      this.$store.commit(mutators.SET_SELECTED_MODULE, this.clients[0].id)
      this.$store.commit(mutators.SET_SELECTED_MODULE_TITLE, this.clients[0].applicationName)
    }
  }
};
</script>
