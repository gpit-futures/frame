<template>
<v-navigation-drawer
      :clipped="$vuetify.breakpoint.lgAndUp"
      class="blue darken-3" dark
      temporary
      absolute
      app
      :value="showDrawer"
      @input="menuClosed"
    >
      <v-toolbar flat class="blue darken-4">
        <v-list>
          <v-list-tile>
            <v-list-tile-title class="title white--text">
              Modules:
            </v-list-tile-title>
          </v-list-tile>
        </v-list>
      </v-toolbar>
      <v-list dense>
        <template v-for="client of clients">
          <v-list-tile :key="client.id" @click="select(client)">
            <v-list-tile-action>
              <v-icon>apps</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title>
                {{ client.applicationName }}
              </v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
        </template>
      </v-list>
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
      return this.$store.state.showDrawer
    }
  },
  methods: {
    select (client) {
      console.log(this.selected)
      this.$store.commit(mutators.SET_SHOW_DRAWER, false);
      this.$store.commit(mutators.SET_SHOW_DASHBOARD, false)
      this.$store.commit(mutators.SET_SELECTED_MODULE, client.id)
      this.$store.commit(mutators.SET_SELECTED_MODULE_TITLE, client.applicationName)
    },
    menuClosed(state){
      console.log("State: " + state)
        this.$store.commit(mutators.SET_SHOW_DRAWER, state);
    },
  }
}
</script>