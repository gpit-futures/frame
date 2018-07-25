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
        <transition-group name="list">
        <template v-for="notification of notifications">
          <v-list-group
            :key="notification.id">
            <v-list-tile slot="activator" :key="notification.id">
              <v-icon @click="deleteNotification(notification)" class="notification-close" >close</v-icon>
            <v-list-tile-action>
              <!-- <v-icon>event_available</v-icon> -->
              <v-icon v-if="notification.type == 'Appointment'">event_available</v-icon>
              <v-icon v-else-if="notification.type == 'Observation'">notes</v-icon>
              <v-icon v-else-if="notification.type == 'Patient'">person_pin</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <!-- <v-list-tile-title class="notification-title" @click="select(notification)">{{notification.nhsNumber}} - {{notification.type}}</v-list-tile-title> -->
              <v-list-tile-title class="notification-title" @click="">{{notification.nhsNumber}} - {{notification.type}}</v-list-tile-title>
              <v-list-tile-sub-title>{{notification.system}}</v-list-tile-sub-title>
              <v-list-tile-sub-title>{{notification.dateCreated | readableDate}}</v-list-tile-sub-title>
            </v-list-tile-content>
          </v-list-tile>

            <v-list-tile>
              <v-list-tile-action>
                <v-icon></v-icon>
              </v-list-tile-action>
              <v-list-tile-content>
              {{notification.details}}
              </v-list-tile-content>
            </v-list-tile>
            
          </v-list-group>
        </template>
        </transition-group>
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
import { NotificationService } from "../services/notification-service";
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
    },
    deleteNotification(notification) {
      this.$store.commit(mutators.REMOVE_NOTIFICATION, notification);
      let notificationService = new NotificationService();
      notificationService.removeNotification(this.$store.state.token.access_token, notification.id)
    }
  },
  filters: {
    readableDate: function (value) {
      // 2018-07-05T14:02:38.489
      moment.relativeTimeThreshold('s', 59);
      moment.relativeTimeThreshold('m', 59);
      moment.relativeTimeThreshold('h', 23);
      moment.relativeTimeThreshold('d', 28);
      moment.relativeTimeThreshold('M', 12);

      return moment.utc(value, "YYYY-MM-DD, hh:mm:ss").fromNow();
    }
  }
};
</script>