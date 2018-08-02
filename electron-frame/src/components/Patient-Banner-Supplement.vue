<template>

<div @click="toggle()">
    <v-layout row>
      <v-flex xs3 class="white black--text">
        <v-card v-if="expanded" dark tile flat xs9 class="white black--text">
          <v-card-text class="text-overflow">
            Address: <span class="has-text-weight-bold">{{patient.address[0].line[0]}} {{patient.address[0].line[1]}} {{patient.address[0].city}} {{patient.address[0].district}} {{patient.address[0].postalCode}}</span>
          </v-card-text>
        </v-card>
        <v-card v-else dark tile flat xs9 class="white black--text">
          <v-card-text>
            <div>Address: </div>
            <div class="has-text-weight-bold">{{patient.address[0].line[0]}}</div>
            <div class="has-text-weight-bold">{{patient.address[0].line[1]}}</div>
            <div class="has-text-weight-bold">{{patient.address[0].city}}</div>
            <div class="has-text-weight-bold">{{patient.address[0].district}}</div>
            <div class="has-text-weight-bold">{{patient.address[0].postalCode}}</div>
          </v-card-text>
        </v-card>
      </v-flex>
      <v-flex xs9 class="white black--text">
        <v-card v-if="expanded" dark tile flat xs3 class="white black--text">
          <v-card-text>Contact: <span class="has-text-weight-bold">{{patient.telecom[0].value}}</span><v-icon light right>keyboard_arrow_down</v-icon></v-card-text>
        </v-card>
        <v-card v-else dark tile flat xs3 class="white black--text">
          <v-card-text>
            <div>Contact: <v-icon light right>keyboard_arrow_up</v-icon></div>
            <div class="has-text-weight-bold">{{patient.telecom[0].value}}</div>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
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
