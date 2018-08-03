<template>
<v-toolbar
      :clipped-left="$vuetify.breakpoint.lgAndUp"
      color="blue darken-3"
      dark
      app
      fixed
    >

    <v-flex xs3>
      <v-toolbar-title style="margin-left: 0px;">
        <v-btn icon @click="home()" class="noDrag">
          <v-icon dark>fas fa-home</v-icon>
        </v-btn>
        <span class="hidden-sm-and-down noDrag">{{infoBarTitle}} {{selectedTitle}}</span>
      </v-toolbar-title>
    </v-flex>

    <v-flex xs6>
      <v-layout align-center justify-center row fill-height>
      <v-flex xs8>
        <v-select
          solo-inverted
          autocomplete
          prepend-icon="search"
          content-class="search-dropdown"
          label="Search"
          class="hidden-sm-and-down noDrag"
          v-if="user"
          :items="patients"
          item-text="Description"
          v-model="selectedPatient"
          attach
          :search-input.sync="search"
          @input="selectPatient"
        ></v-select>
      </v-flex>
      <v-flex xs4>
        <v-btn round @click="clearPatient()" class="noDrag" v-if="user" :disabled="!patientContext">
          <v-icon left dark>fas fa-user</v-icon> <span style="max-width:165px;width:165px;" class="text-overflow">{{patient}}</span> <v-icon right dark>close</v-icon>
        </v-btn>
      </v-flex>
      </v-layout>
      <v-spacer v-if="!user"></v-spacer>
    </v-flex>

    <v-flex xs3 style="text-align: right;">
      <v-btn icon @click="toggleNotifications()" class="noDrag" v-if="user">
        <v-icon dark>fas fa-bell</v-icon>
      </v-btn>
      <v-menu offset-y class="noDrag" v-if="user">
        <v-btn icon slot="activator"><v-icon dark>fas fa-user</v-icon></v-btn>
        <v-list>
          <v-list-tile @click="logout()">
            <v-list-tile-title>Logout</v-list-tile-title>
          </v-list-tile>
        </v-list>
      </v-menu>
      <v-btn icon @click="min()" class="noDrag">
        <v-icon dark>minimize</v-icon>
      </v-btn>
      <v-btn icon @click="max()" class="noDrag">
        <v-icon dark>{{icon}}</v-icon>
      </v-btn>
      <v-btn icon @click="close()" class="noDrag">
        <v-icon dark>close</v-icon>
      </v-btn>
    </v-flex>

    <v-dialog v-model="dialog" max-width="400">
        <v-card>
          <v-card-title class="blue darken-4 title white--text">Patient Access Disclaimer</v-card-title>
          <v-divider></v-divider>
          <v-card-text class="has-text-center">Selecting <span class="has-text-weight-bold">{{patientName}}</span> will change the patient context and you may lose any unsaved work.
          Please ensure that you have the correct permission to view these records before proceeding</v-card-text>
          <v-card-text class="has-text-center">Would you like to go to <span class="has-text-weight-bold">'{{patientName}}'</span>?</v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="grey darken-1" flat="flat" @click.native="dialog = false, selectedPatient = null">Cancel</v-btn>
            <v-btn color="green darken-1" flat="flat" @click="triggerPatientContext" @click.native="dialog = false">Go to patient</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>

    </v-toolbar>
</template>

<script>
import moment from "moment";
import mutators from "../store/mutators";
import { SearchService } from "../services/search-service";
import { remote } from "electron";
import { triggerPatientContextEvent } from "../utilities/client-manager";

export default {
  name: "info-bar",
  components: {},
  data() {
    return {
      infoBarTitle: "PCaaP: ",
      selectedPatient: null,
      search: null,
      patients: [],
      icon: "fullscreen_exit",
      dialog: false
    };
  },
  methods: {
    logout() {
      this.$store.commit(mutators.LOGOUT);
      this.$root.$emit("notifications");
      this.$router.push({ name: "Login" });
    },
    toggleDrawer() {
      if (this.$store.state.showDrawer) {
        this.$store.commit(mutators.SET_SHOW_DRAWER, false);
      } else {
        this.$store.commit(mutators.SET_SHOW_DRAWER, true);
      }
    },
    toggleNotifications() {
      if (this.$store.state.showNotifications) {
        this.$store.commit(mutators.SET_SHOW_NOTIFICATIONS, false);
      } else {
        this.$store.commit(mutators.SET_SHOW_NOTIFICATIONS, true);
      }
    },
    min() {
      if (remote.getCurrentWindow().isMinimized()) {
        remote.getCurrentWindow().restore();
      } else {
        remote.getCurrentWindow().minimize();
      }
    },
    max() {
      if (remote.getCurrentWindow().isMaximized()) {
        remote.getCurrentWindow().setSize(1600, 800);
        this.icon = "fullscreen";
      } else {
        remote.getCurrentWindow().maximize();
        this.icon = "fullscreen_exit";
      }
    },
    close() {
      remote.getCurrentWindow().close();
    },
    home() {
      this.$store.commit(mutators.SET_SHOW_DASHBOARD, true);
      this.$store.commit(mutators.SET_SELECTED_MODULE, null);
      this.$store.commit(mutators.SET_SELECTED_MODULE_TITLE, "Home");
    },
    createPatientList(patientList) {
      console.log(patientList);
      return patientList.map(entry => {
        const Description =
          entry.title + " " + entry.firstName + " " + entry.lastName +
          " - " +
          moment.utc(entry.dateOfBirth).format('DD-MMM-YYYY') +
          " - " +
          entry.gender +
          " - " +
          // entry.nhsNumber;
          this.nhsNumberFormat(entry.nhsNumber);
        return Object.assign({}, entry, { Description });
      });
    },
    selectPatient() {
      if (this.selectedPatient != null) {
        this.dialog = true
      }
    },
    async triggerPatientContext() {
      let searchService = new SearchService();
      if (this.selectedPatient != null) {
          triggerPatientContextEvent(
          "patient-context:changed",
          await searchService.getPatient(this.token,this.selectedPatient.nhsNumber)
        );
      } else {
        this.clearPatient();
      }
    },
    clearPatient() {
      this.selectedPatient = null;
      triggerPatientContextEvent("patient-context:ended", null);
    },
    nhsNumberFormat(nhsNumber) {
      const tidy = nhsNumber.trim();
      if (tidy.length < 10) {
          return "UNKNOWN";
        }
      return `${tidy.substr(0, 3)} ${tidy.substr(3, 3)} ${tidy.substr(6)}`;
    }
  },
  computed: {
    time() {
      return moment().format("dddd MMM DD YYYY | hh:mm");
    },
    selectedTitle() {
      return this.$store.state.selectedTitle;
    },
    user() {
      return this.$store.state.user;
    },
    patientContext() {
      return this.$store.state.patientContext;
    },
    practitionerName() {
      return (
        this.user.title + " " + this.user.firstName + " " + this.user.lastName
      );
    },
    token() {
      return this.$store.state.token.access_token
    },
    patient() {
      if (this.$store.state.patient) {
        return this.$store.state.patient.name[0].family + ", " + this.$store.state.patient.name[0].given[0] +" (" + this.$store.state.patient.name[0].prefix[0] + ")"
      } else {
        return "No Patient Selected"
      }
    },
    patientName() {
      console.log(this.selectedPatient)
      if (this.selectedPatient) {
        return this.selectedPatient.lastName + ", " + this.selectedPatient.firstName +" (" + this.selectedPatient.title + ")"
      } else {
        return "No Patient Selected"
      }
    }
  },
  watch: {
    async search(val) {

      // Check if search value is nhsNumber
      let searchService = new SearchService();
      var re = new RegExp(/\d{3}\s{1}\d{3}\s{1}\d{4}/g);
      if(re.test(val)) {
        let searchResult = await searchService.getSearchResults(this.token,val.replace(/\s/g, ''));
        this.patients = this.createPatientList(searchResult.patients);
      } else {
        let searchResult = await searchService.getSearchResults(this.token,val);
        this.patients = this.createPatientList(searchResult.patients);
      }
    }
  }
};
</script>
