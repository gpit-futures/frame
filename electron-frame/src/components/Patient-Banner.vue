<template>
<!-- <div class="dw-patient-banner dw-patient-banner-layout">
  <div class="column columns is-gapless">
    <div class="column has-text-weight-bold is-three-fifths">
      <span class="is-uppercase">{{patient.name[0].family}}</span>, {{patient.name[0].given[0]}} ({{patient.name[0].prefix[0]}})
    </div>
    <div class="column has-text-right">
      Born <span class="has-text-weight-bold">{{patient.birthDate | dateTimeFormat}} {{patient.birthDate | ageToday}}</span>
    </div>
    <div class="column has-text-right">
      Gender <span class="has-text-weight-bold">{{patient.gender}}</span>
    </div>
    <div class="column has-text-right">
      NHS No. <span class="has-text-weight-bold">{{patient.identifier[0].value | nhsNumberFormat}}</span>
    </div>
  </div>
</div> -->
<div>
    <v-layout row>
      <v-flex xs3>
        <v-card dark tile flat xs2 class="amber darken-3">
          <v-card-text><span class="is-uppercase">{{patient.name[0].family}}</span>, {{patient.name[0].given[0]}} ({{patient.name[0].prefix[0]}})</v-card-text>
        </v-card>
      </v-flex>
      <v-flex xs3>
        <v-card dark tile flat xs2 class="amber darken-3">
          <v-card-text>Born: <span class="has-text-weight-bold">{{patient.birthDate | dateTimeFormat}} {{patient.birthDate | ageToday}}</span></v-card-text>
        </v-card>
      </v-flex>
      <v-flex xs3>
        <v-card dark tile flat xs2 class="amber darken-3">
          <v-card-text>Gender: <span class="has-text-weight-bold">{{patient.gender}}</span></v-card-text>
        </v-card>
      </v-flex>
      <v-flex xs3>
        <v-card dark tile flat xs2 class="amber darken-3">
          <v-card-text>NHS No: <span class="has-text-weight-bold">{{patient.identifier[0].value | nhsNumberFormat}}</span></v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
</div>
</template>


<script>
import moment from "moment"

export default {
  name: "patient-banner",
  props: [],
  components: {},
  data() {
    return {
      // Some mock data to fill the page
      patientBannerTitle: "patientBanner"
    };
  },
  computed: {
    patient() {
      return this.$store.state.patient;
    }
  },
  filters: {
    dateTimeFormat(date) {
      return moment(date).format("DD-MMM-YYYY");
    },
    nhsNumberFormat(nhsNumber) {
      const tidy = nhsNumber.trim();
      if (tidy.length < 10) {
        return "UNKNOWN";
      }
      return `${tidy.substr(0, 3)} ${tidy.substr(3, 3)} ${tidy.substr(6)}`;
    },
    ageToday(dob) {
      let template = "(%v)"
      const today = moment();
      const date = moment(dob);

      const months = today.diff(date, "months");
      let result = "";

      if (months > 24) {
        result = `${today.diff(date, "years")}y`;
      } else {
        // todo: when to show age in weeks/days?
        result = `${months}m`;
      }

      return template.replace("%v", result);
    }
  }
};
</script>

