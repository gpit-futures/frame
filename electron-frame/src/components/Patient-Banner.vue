<template>
<div>
    <v-layout row transition="scale-transition">
      <v-flex xs6>
        <v-card dark tile flat xs2 class="patient-banner-main">
          <v-card-text><span class="is-uppercase has-text-weight-bold patient-name">{{patient.name[0].family}}, {{patient.name[0].given[0]}} ({{patient.name[0].prefix[0]}})</span></v-card-text>
        </v-card>
      </v-flex>
      <v-flex xs2>
        <v-card dark tile flat xs2 class="patient-banner-main">
          <v-card-text>Born: <span class="has-text-weight-bold">{{patient.birthDate | dateTimeFormat}} {{patient.birthDate | ageToday}}</span></v-card-text>
        </v-card>
      </v-flex>
      <v-flex xs2>
        <v-card dark tile flat xs2 class="patient-banner-main">
          <v-card-text>Gender: <span class="has-text-weight-bold">{{patient.gender}}</span></v-card-text>
        </v-card>
      </v-flex>
      <v-flex xs2>
        <v-card dark tile flat xs2 class="patient-banner-main">
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
      patientBannerTitle: "patientBanner"
    };
  },
  computed: {
    patient() {
      return this.$store.state.patient;
    }
  },
  filters: {
    ageToday(dob) {
      let template = "(%v)"
      const today = moment();
      const date = moment(dob);

      const months = today.diff(date, "months");
      let result = "";

      if (months > 24) {
        result = `${today.diff(date, "years")}y`;
      } else {
        result = `${months}m`;
      }

      return template.replace("%v", result);
    }
  }
};
</script>

