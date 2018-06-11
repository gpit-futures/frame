<template>
<div>
  <div class="dw-sidebar-header">
      Logo
  </div>
  <sidebar-icon v-for="client of clients" :key="client.id"
    v-bind:client="client" 
    class="sidebar-icons"
    v-bind:class="{'is-active': selected === client.id}"></sidebar-icon>
</div>
</template>


<script>
import SidebarIcon from './Sidebar-Icon.vue'

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
    }
  }
}
</script>
