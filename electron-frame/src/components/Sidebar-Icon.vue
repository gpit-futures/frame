<template>
    <div @click="select(client.id)">
    {{client.applicationName}}
    </div>
</template>

<script>
import mutators from '../store/mutators'

export default {
  name: 'sidebar-icon',
  props: ['client'],
  components: { },
  data () {
    return { // Some mock data to fill the page
    }
  },
  computed: {
    selected() {
      return this.$store.state.selected
    }
  },
  methods: {
    select (clientId) {
      console.log(this.selected)
      this.$store.commit(mutators.SET_SELECTED_MODULE, clientId)
      this.$store.commit(mutators.SET_SELECTED_MODULE_TITLE, this.client.applicationName)
    }
  }
}
</script>