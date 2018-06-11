<template>
<div>
  <div class="column">
    <span v-if="user">Welcome</span> <span v-if="user" class="has-text-weight-bold">{{practitionerName}}</span>
  </div>
  <div class="column has-text-centered">
    {{infoBarTitle}} {{selectedTitle}}
  </div>
  <div id="title-bar-btns" class="column has-text-right">
    {{time}}  
    <span class="divider"> | </span>
    <button v-if="user" id="min-btn"><i class="fas fa-search"></i></button>
    <button v-if="user" id="min-btn"><i class="far fa-bell"></i></button>
    <button v-if="user" id="min-btn" @click="logout()"><i class="fas fa-user"></i></button>
    <span v-if="user" class="divider"> | </span>
    <button id="min-btn" @click="min()"><i class="far fa-window-minimize"></i></button>
    <button id="max-btn" @click="max()"><i class="far fa-window-restore"></i></button>
    <button id="close-btn" @click="close()"><i class="far fa-window-close"></i></button>
  </div>
</div>
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
      infoBarTitle: "PCaaP: "
    };
  },
  methods: {
    logout() {
      this.$store.commit(mutators.LOGOUT);
      this.$router.push({ name: "Login" });
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
