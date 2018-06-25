<template>
<v-toolbar
      :clipped-left="$vuetify.breakpoint.lgAndUp"
      color="blue darken-3"
      dark
      app
      fixed
    >
      <v-toolbar-title style="width: 300px" class="ml-0 pl-3">
        <v-toolbar-side-icon @click.stop="toggleDrawer" class="noDrag" v-if="user"></v-toolbar-side-icon>
        <v-btn icon @click="home()" class="noDrag">
          <v-icon dark>home</v-icon>
        </v-btn>
        <span class="hidden-sm-and-down noDrag">{{infoBarTitle}} {{selectedTitle}}</span>
      </v-toolbar-title>
      <v-select
        flat
        solo-inverted
        autocomplete
        prepend-icon="search"
        label="Search"
        class="hidden-sm-and-down noDrag"
        v-if="user"
        :items="users"
        attach
      ></v-select>
      <v-spacer v-if="!user"></v-spacer>
      <v-btn icon v-if="user">
        <v-icon dark></v-icon>
      </v-btn>
      <v-btn icon @click="toggleNotifications()" class="noDrag" v-if="user">
        <v-icon dark>notification_important</v-icon>
      </v-btn>
      <v-menu offset-y class="noDrag" v-if="user">
        <v-btn icon slot="activator"><v-icon dark>account_circle</v-icon></v-btn>
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
        <v-icon dark>open_in_new</v-icon>
      </v-btn>
      <v-btn icon @click="close()" class="noDrag">
        <v-icon dark>close</v-icon>
      </v-btn>
    </v-toolbar>
</template>

<script>
import moment from "moment";
import mutators from "../store/mutators";
import { remote } from "electron";

export default {
  name: "info-bar",
  components: {},
  data() {
    return {
      // Some mock data to fill the page
      infoBarTitle: "PCaaP: ",
      users: [
        "Aengus O'Connor - 19-Mar-1938 - Male - 999 999 9024",
        "Aiko Haney - 29-Aug-1954 - Female - 999 999 9088",
        "Alexis Greer - 23-Apr-1941 - Female - 999 999 9097",
        "Aretha Hobbs - 29-Jan-1923 - Male - 999 999 9059",
        "Blair Wells - 03-Mar-1969 - Female - 999 999 9007",
        "Bryar Hendricks - 17-Sep-1976 - Female - 999 999 9038",
        "Carol Griffith - 03-Aug-1927 - Male - 999 999 9049",
        "Cassandra Workman - 05-Apr-1948 - Male - 999 999 9037",
        "Cian Magee - 26-Jul-1938 - Male - 999 999 9008",
        "Clio Cooke - 10-Apr-1964 - Female - 999 999 9087"
      ]
    };
  },
  methods: {
    logout() {
      this.$store.commit(mutators.LOGOUT);
      this.$router.push({ name: "Login" });
    },
    toggleDrawer() {
      if (this.$store.state.showDrawer) {
        this.$store.commit(mutators.SET_SHOW_DRAWER, false);
      } else {
        this.$store.commit(mutators.SET_SHOW_DRAWER, true);
      }
      console.log(this.$store.state.showDrawer);
    },
    toggleNotifications() {
      if (this.$store.state.showNotifications) {
        this.$store.commit(mutators.SET_SHOW_NOTIFICATIONS, false);
      } else {
        this.$store.commit(mutators.SET_SHOW_NOTIFICATIONS, true);
      }
      console.log(this.$store.state.showNotifications);
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
        remote.getCurrentWindow().unmaximize();
      } else {
        remote.getCurrentWindow().maximize();
      }
    },
    close() {
      remote.getCurrentWindow().close();
    },
    home() {
      this.$store.commit(mutators.SET_SHOW_DASHBOARD, true);
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
    practitionerName() {
      return (
        this.user.title + " " + this.user.firstName + " " + this.user.lastName
      );
    }
  }
};
</script>
