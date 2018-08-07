<template>
<v-navigation-drawer
      :clipped="$vuetify.breakpoint.lgAndUp"
      :mini-variant.sync="showDrawer"
      class="blue darken-3 menu-sidebar" dark
      hide-overlay
      app
      v-if="!clients.length == 0"
      temporary
      stateless
      :value="true"
    >
      <v-list>
        <draggable :list="clients" class="dragArea" @change="updateList">
        <template v-for="client of clients">
          <v-list-tile :key="client.id" @click="select(client)" v-on:mouseover="mouseOver" v-on:mouseout="mouseOut"
          v-bind:class="{'sidebar-is-active': selected === client.id}"
          class="sidebar-items">

            <v-list-tile-action>
              <v-icon v-if="client.applicationName == 'Core'">fas fa-globe</v-icon>
              <v-icon v-else-if="client.applicationName == 'INR'">fas fa-syringe</v-icon>
              <v-icon v-else-if="client.applicationName == 'Appointments'">fas fa-calendar-alt</v-icon>
              <v-icon v-else-if="client.applicationName == 'Document Management'">fas fa-folder-open</v-icon>
              <v-icon v-else-if="client.applicationName == 'Prescribing'">fas fa-prescription-bottle-alt</v-icon>
              <v-icon v-else-if="client.applicationName == 'Workflow'">fas fa-list-alt</v-icon>
              <v-icon v-else-if="client.applicationName == 'Telecare'">fas fa-comments</v-icon>
              <v-icon v-else-if="client.applicationName == 'Caseload Management'">fas fa-briefcase-medical</v-icon>
              <v-icon v-else-if="client.applicationName == 'Stock Management'">fas fa-box-open</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title>
                {{ client.applicationName }}
              </v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
        </template>
        </draggable>
      </v-list>
    </v-navigation-drawer>
</template>


<script>
import mutators from "../store/mutators";
import draggable from 'vuedraggable'
import { CatalogueService } from "../services/catalogue-service";

export default {
  name: "sidebar",
  components: {
    draggable
  },
  data() {
    return {
      hovered: null,
      timer: null,
      catalogueService: new CatalogueService()
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
      return this.$store.state.showDrawer;
    },
    token() {
      return this.$store.state.token.access_token
    },
    userId() {
      return this.$store.state.user.id
    }
  },
  methods: {
    select(client) {
      this.$store.commit(mutators.SET_SHOW_DASHBOARD, false);
      this.$store.commit(mutators.SET_SELECTED_MODULE, client.id);
      this.$store.commit(mutators.SET_SELECTED_MODULE_TITLE, client.applicationName);

      this.$store.commit(mutators.ADD_RECENT_MODULE, client);
      this.catalogueService.updateRecentClientList(this.token, this.$store.state.recentModules)
    },
    mouseOver() {
      this.hovered = true;
      clearTimeout(this.timer);
      this.timer = setTimeout(() => {
        if (this.hovered) {
          this.$store.commit(mutators.SET_SHOW_DRAWER, false);
        }
      }, 1000);
    },
    mouseOut() {
      this.hovered = false;
      clearTimeout(this.timer);
      this.timer = setTimeout(() => {
        if (!this.hovered) {
          this.$store.commit(mutators.SET_SHOW_DRAWER, true);
        }
      }, 1000);
    },
    async updateList() {
      await this.catalogueService.updateClientList(this.token, this.userId, this.clients)
    }
  }
};
</script>