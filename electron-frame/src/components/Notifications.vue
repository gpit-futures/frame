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
      <v-list dense>
        <template v-for="client of clients" v-bind:class="{'is-active': selected === client.id}">
          <v-list-tile :key="client.id" @click="select(client)">
            <v-list-tile-action>
              <v-icon>event_available</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title>
                Appointment Notification
              </v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
        </template>
      </v-list>
      <v-dialog v-model="dialog" max-width="290">
            <v-card>
              <v-card-title class="headline"><v-icon>event_available</v-icon>Appointment Notification</v-card-title>
              <v-card-text>A new appointment for patient(Carol Griffith, 999 999 9049) has been created.</v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="green darken-1" flat="flat" @click.native="dialog = false">close</v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
    </v-navigation-drawer>
</template>


<script>
import SidebarIcon from './Sidebar-Icon.vue'
import mutators from '../store/mutators'

export default {
  name: 'sidebar',
  components: { SidebarIcon },
  data () {
    return { // Some mock data to fill the page
    dialog: false
    }
  },
  computed: {
    selected() {
      return this.$store.state.selected
    },
    clients() {
      return this.$store.state.clients
    },
    showDrawer() {
      return this.$store.state.showNotifications
    }
  },
  methods: {
    select (client) {
      console.log(this.selected)
      this.dialog = true
      this.$store.commit(mutators.SET_SHOW_NOTIFICATIONS, false);
    },
    menuClosed(state){
      console.log("State: " + state)
        this.$store.commit(mutators.SET_SHOW_NOTIFICATIONS, state);
    },
  }
}
</script>