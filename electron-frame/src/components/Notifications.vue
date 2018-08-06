<template>
<v-navigation-drawer
      :clipped="$vuetify.breakpoint.lgAndUp"
      class="notifications-sidebar" light
      temporary
      absolute
      right
      app
      width="600"
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
              <v-icon v-if="notification.type == 'Appointment'">fas fa-calendar-alt</v-icon>
              <v-icon v-else-if="notification.type == 'Observation'">fas fa-notes-medical</v-icon>
              <v-icon v-else-if="notification.type == 'Patient'">fas fa-user-plus</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title class="notification-title" @click="select(notification)">{{notification.patientName}} - {{notification.type}}</v-list-tile-title>
              <v-list-tile-sub-title>{{notification.system}}</v-list-tile-sub-title>
              <v-list-tile-sub-title>{{notification.dateCreated | readableDate}}</v-list-tile-sub-title>
            </v-list-tile-content>
          </v-list-tile>

            <v-list-tile>
              <v-list-tile-action>
                <v-icon></v-icon>
              </v-list-tile-action>
              <v-list-tile-content>{{notification.summary}}: {{notification.details}}
              <span v-if="notification.type == 'Appointment'">{{notification.json | appointmentDates}}</span>
              </v-list-tile-content>
            </v-list-tile>
            
          </v-list-group>
        </template>
        </transition-group>
      </v-list>

      <v-dialog v-model="dialog" max-width="400">
        <v-card>
          <v-card-title class="blue darken-4 title white--text">Patient Access Disclaimer</v-card-title>
          <v-divider></v-divider>
          <v-card-text class="has-text-center">Selecting <span class="has-text-weight-bold">{{selectedNotification.patientName}}</span> will change the patient context and you may lose any unsaved work.
          Please ensure that you have the correct permission to view these records before proceeding</v-card-text>
          <v-card-text class="has-text-center">Would you like to go to <span class="has-text-weight-bold">'{{selectedNotification.patientName}}'</span>?</v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="grey darken-1" flat="flat" @click.native="dialog = false">Cancel</v-btn>
            <v-btn color="green darken-1" flat="flat" @click="changePatientContext" @click.native="dialog = false">Go to patient</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-navigation-drawer>
</template>


<script>
import mutators from "../store/mutators";
import { NotificationService } from "../services/notification-service";
import moment from "moment";
import { SearchService } from "../services/search-service";
import { triggerPatientContextEvent } from "../utilities/client-manager";

export default {
  name: "sidebar",
  data() {
    return {
      // Some mock data to fill the page
      dialog: false,
      selectedNotification: {nhsNumber:"0000000000"},
      notificationService: new NotificationService(),
      searchService: new SearchService()
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
      this.$store.commit(mutators.SET_SHOW_NOTIFICATIONS, false);
      this.selectedNotification = notification;
      this.dialog = true;
    },
    menuClosed(state) {
      this.$store.commit(mutators.SET_SHOW_NOTIFICATIONS, state);
    },
    deleteNotification(notification) {
      this.$store.commit(mutators.REMOVE_NOTIFICATION, notification);
      this.notificationService.removeNotification(this.$store.state.token.access_token, notification.id)
    },
    async changePatientContext() {
      if (this.selectedNotification.nhsNumber != null){
          triggerPatientContextEvent(
          "patient-context:changed",
          await this.searchService.getPatient(this.$store.state.token.access_token,this.selectedNotification.nhsNumber)
        );
      }
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
    },
    appointmentDates: function (value) {
      let json = JSON.parse(value);
      return "Appointment Date: " + moment(json.start).format('YYYY-MM-DD, hh:mm') + " - " +   moment(json.end).format('hh:mm');
    }
  }
};
</script>