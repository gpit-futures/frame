<template>
<div @click="toggle()">
  <div class="column" v-bind:class="{'has-text-truncated': !!expanded}">
    address[0] 
    <div v-if="expanded">
      <div class="has-text-weight-bold">{{patient.address[0].line[0]}}</div>
      <div class="has-text-weight-bold">{{patient.address[0].line[1]}}</div>
      <div class="has-text-weight-bold">{{patient.address[0].city}}</div>
      <div class="has-text-weight-bold">{{patient.address[0].district}}</div>
      <div class="has-text-weight-bold">{{patient.address[0].postalCode}}</div>
    </div>
    <span v-else class="has-text-weight-bold">{{patient.address[0].line[0]}} {{patient.address[0].line[1]}} {{patient.address[0].city}} {{patient.address[0].district}} {{patient.address[0].postalCode}}</span>
  </div>
  <div class="column" v-bind:class="{'has-text-truncated': !!expanded}">
    Contact 
    <div v-if="expanded">
      <div class="has-text-weight-bold">{{patient.telecom[0].value}}</div>
    </div>
    <span v-else class="has-text-weight-bold">{{patient.telecom[0].value}}</span>
  </div>
  <div class="column">

  </div>
  <div class="column">
    
  </div>
</div>
</template>

<script>

export default {
  name: 'patient-banner-supplement',
  props: ['expanded'],
  components: { },
  data () {
    return { // Some mock data to fill the page
      patientBannerSupplementTitle: 'patientBannerSupplement'
    }
  },
  computed: {
    patient() {
      return this.$store.state.patient;
    }
  },
  methods: {
    toggle() {
      this.expanded = !this.expanded
    }
  }
}
</script>
