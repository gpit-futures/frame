<template>

<v-content>
    <!-- sidebar component -->
    <div class="dw-main">
      <!-- patient banner component -->
      <transition name="slide-fade">
      <patient-banner v-if="patientContext"
      class="patient-banner-main"></patient-banner>
        <!-- patient banner suplement component -->
      </transition>
      <transition name="slide-fade">
      <patient-banner-supplement v-if="patientContext"
        class="patient-banner-sub"
        :expanded=true></patient-banner-supplement>
      </transition>
        <!-- frame component -->
      <frames class="dw-frames"></frames>
    </div>
    </v-content>
</template>

<script>
// components
import Frames from "./Frames.vue";
import InfoBar from "./Info-Bar.vue";
import PatientBannerSupplement from "./Patient-Banner-Supplement.vue";
import PatientBanner from "./Patient-Banner.vue";

// services
import { CatalogueService } from "../services/catalogue-service";
import { UserService } from "../services/user-service";
import mutators from "../store/mutators";

export default {
  name: "home",
  components: {
    Frames,
    InfoBar,
    PatientBannerSupplement,
    PatientBanner
  },
  data() {
    return {
      userService: new UserService(),
      catalogueService: new CatalogueService()
    };
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    patient() {
      return this.$store.state.patient;
    },
    patientContext() {
      return this.$store.state.patientContext;
    },
    selected() {
      return this.$store.state.selected;
    },
    clients() {
      return this.$store.state.clients;
    },
    practitionerName() {
      return `${this.user.title} ${this.user.firstName} ${this.user.lastName}`;
    }
  },
  methods: {},
  async mounted() {
    this.$store.commit(mutators.SET_USER, await this.userService.getUser(this.user.id));
    this.$store.commit(mutators.SET_CLIENTS, await this.catalogueService.getClients(this.user.id));
    if (this.clients.length > 0) {
      this.clientManager = this.clients;
      this.$store.commit(mutators.SET_SELECTED_MODULE, this.clients[0].id);
      this.$store.commit(mutators.SET_SELECTED_MODULE_TITLE, "Home");
    }
  }
};
</script>
