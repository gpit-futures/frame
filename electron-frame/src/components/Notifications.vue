<template>
<v-navigation-drawer
      :clipped="$vuetify.breakpoint.lgAndUp"
      class="blue darken-3" dark
      temporary
      absolute
      right
      app
      :value="showDrawer"
      @input="menuClosed"
    >
      <v-toolbar flat class="blue darken-4" dark>
        <v-list>
          <v-list-tile>
            <v-list-tile-title class="title white--text">
              Notifications:
            </v-list-tile-title>
          </v-list-tile>
        </v-list>
      </v-toolbar>

      <v-list three-line>
        <template v-for="client of clients">
          <v-list-group
            :key="client.id">

            <v-list-tile slot="activator" :key="client.id">
            <v-list-tile-action>
              <v-icon>event_available</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title class="notification-title" @click="select(client)">
              SMITH, John. (Mr) - 999 999 9049
              </v-list-tile-title>
              <v-list-tile-sub-title>Appointment Cancelled</v-list-tile-sub-title>
              <v-list-tile-sub-title>{{"2018-07-17T14:02:38.489" | readableDate}}</v-list-tile-sub-title>
            </v-list-tile-content>
          </v-list-tile>

            <v-list-tile>
              <v-list-tile-action>
                <v-icon></v-icon>
              </v-list-tile-action>
              <v-list-tile-content>
              Consultation with Dr. H. Marshall to discuss inflammation of the patient's ear which has persisted for over 2 weeks since last visit.
              </v-list-tile-content>
            </v-list-tile>
            
          </v-list-group>
        </template>
      </v-list>

      <v-dialog v-model="dialog" max-width="400">
        <v-card>
          <v-card-text class="has-text-center">Selecting <span class="has-text-weight-bold">SMITH, John. (Mr) - 999 999 9049</span> will change the patient context and you may lose any unsaved work.</v-card-text>
          <v-card-text class="has-text-center">Would you like to go to 'John Smith'?</v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="grey darken-1" flat="flat" @click.native="dialog = false">Cancel</v-btn>
            <v-btn color="green darken-1" flat="flat" @click.native="dialog = false">Go to John Smith</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-navigation-drawer>
</template>


<script>
import SidebarIcon from "./Sidebar-Icon.vue";
import mutators from "../store/mutators";
import moment from "moment";

export default {
  name: "sidebar",
  components: { SidebarIcon },
  data() {
    return {
      // Some mock data to fill the page
      dialog: false,
      selectedNotification: null
    };
  },
  computed: {
    selected() {
      return this.$store.state.selected;
    },
    clients() {
      return this.$store.state.clients;
    },
    showDrawer() {
      return this.$store.state.showNotifications;
    },
    notifications() {
      return this.$store.state.notifications;
    }
  },
  methods: {
    select(notification) {
      console.log(this.selected);
      this.dialog = true;
      this.$store.commit(mutators.SET_SHOW_NOTIFICATIONS, false);
      selectedNotification = notification;
    },
    menuClosed(state) {
      console.log("State: " + state);
      this.$store.commit(mutators.SET_SHOW_NOTIFICATIONS, state);
    }
  },
  filters: {
  readableDate: function (value) {
    // 2018-07-05T14:02:38.489
    return moment(value, "YYYY-MM-DD, h:mm:ss").fromNow();
  }
}
};
</script>