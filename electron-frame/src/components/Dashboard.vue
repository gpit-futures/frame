<template>
<v-content>
      <v-container fluid>
        <v-layout flex justify-center wrap row>
          <v-flex xs12>
              <v-card xs6 color="white" class="black--text" align-content-center d-flex>
                <v-card-title primary-title>
                  <div style="width:100%;">
                    <v-data-table
                      :headers="headers"
                      :items="recentPatients"
                      hide-actions
                      class="elevation-1">
                      <template slot="items" slot-scope="props">
                        <tr @click="selectPatient(props.item)">
                          <td><span class="is-uppercase">{{props.item.name[0].family}}</span>, {{props.item.name[0].given[0]}} ({{props.item.name[0].prefix[0]}})</td>
                          <td class="text-xs-left">{{ props.item.birthDate | dateTimeFormat}}</td>
                          <td class="text-xs-left">{{ props.item.gender }}</td>
                          <td class="text-xs-left">{{ props.item.identifier[0].value | nhsNumberFormat}}</td>
                          <td class="text-xs-left"><v-btn icon><v-icon>keyboard_arrow_right</v-icon></v-btn></td>
                        </tr>
                      </template>
                  </v-data-table>
                  </div>
                </v-card-title>
                <v-card-actions>
                </v-card-actions>
              </v-card>
          </v-flex>
          <v-flex xs12>Recent Modules:</v-flex>
          <v-flex xs2 v-for="module of recentModules" :key="module.id">
              <v-card color="white" class="black--text" hover d-flex @click="selectModule(module)">
                <v-card-title primary-title @click="selectModule(module)">
                  <v-icon v-if="module.applicationName == 'Core'">fas fa-globe</v-icon>
                  <v-icon v-else-if="module.applicationName == 'INR'">fas fa-syringe</v-icon>
                  <v-icon v-else-if="module.applicationName == 'Appointments'">fas fa-calendar-alt</v-icon>
                  <v-icon v-else-if="module.applicationName == 'Document Management'">fas fa-folder-open</v-icon>
                  <v-icon v-else-if="module.applicationName == 'Prescribing'">fas fa-prescription-bottle-alt</v-icon>
                  <v-icon v-else-if="module.applicationName == 'Workflow'">fas fa-list-alt</v-icon>
                  <v-icon v-else-if="module.applicationName == 'Telecare'">fas fa-comments</v-icon>
                  <v-icon v-else-if="module.applicationName == 'Caseload Management'">fas fa-briefcase-medical</v-icon>
                  <v-icon v-else-if="module.applicationName == 'Stock Management'">fas fa-box-open</v-icon>
                   {{module.applicationName}}
                </v-card-title>
                <v-card-actions>
                </v-card-actions>
              </v-card>
          </v-flex>
        </v-layout>
      </v-container>

      <v-dialog v-model="dialog" max-width="400">
        <v-card>
          <v-card-title class="blue darken-4 title white--text">Patient Access Disclaimer</v-card-title>
          <v-divider></v-divider>
          <v-card-text class="has-text-center">Selecting <span class="has-text-weight-bold">{{patient}}</span> will change the patient context and you may lose any unsaved work.
          Please ensure that you have the correct permission to view these records before proceeding</v-card-text>
          <v-card-text class="has-text-center">Would you like to go to <span class="has-text-weight-bold">'{{patient}}'</span>?</v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="grey darken-1" flat="flat" @click.native="dialog = false">Cancel</v-btn>
            <v-btn color="green darken-1" flat="flat" @click="triggerPatientContext" @click.native="dialog = false">Go to patient</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>

    </v-content>
</template>

<script>
import { triggerPatientContextEvent } from "../utilities/client-manager";
import { PatientService } from "../services/patient-service";
import { CatalogueService } from "../services/catalogue-service";
import mutators from "../store/mutators";

export default {
  name: "dashboard",
  components: {},
  data() {
    return {
      // Some mock data to fill the page
      headers: [
        {
          text: "Recent Patients",
          align: "left",
          sortable: true,
          value: "name"
        },
        { text: "DOB", value: "birthDate", sortable: true},
        { text: "Gender", value: "gender", sortable: true },
        { text: "NHS Number", value: "identifier", sortable: true },
        { text: "Actions", value: "actions", sortable: true }
      ],
      dialog: false,
      selectedPatient: null
    };
  },
  methods: {
    selectPatient(patient) {
      this.dialog = true
      this.selectedPatient = patient
    },
    triggerPatientContext() {
      triggerPatientContextEvent("patient-context:changed", this.selectedPatient);
    },
    selectModule(client) {
      this.$store.commit(mutators.SET_SHOW_DASHBOARD, false);
      this.$store.commit(mutators.SET_SELECTED_MODULE, client.id);
      this.$store.commit(
        mutators.SET_SELECTED_MODULE_TITLE,
        client.applicationName
      );

      this.$store.commit(mutators.ADD_RECENT_MODULE, client);
      let catalogueService = new CatalogueService();
      catalogueService.updateRecentClientList(this.token, this.$store.state.recentModules)
    },
  },
  computed: {
    recentPatients() {
      return this.$store.state.recentPatients;
    },
    recentModules() {
      return this.$store.state.recentModules;
    },
    token() {
      return this.$store.state.token.access_token
    },
    patient() {
      if (this.selectedPatient) {
        return this.selectedPatient.name[0].given[0] + " " + this.selectedPatient.name[0].family
      } else {
        return "No Patient Selected"
      }
    }
  },
  async mounted() {
    let patientService = new PatientService();
    let patientList = await patientService.getPatientList(this.token)
    this.$store.commit(mutators.SET_RECENT_PATIENT, patientList);

    let catalogueService = new CatalogueService();
    let recentModuleList = await catalogueService.getRecentClients(this.token)
    this.$store.commit(mutators.SET_RECENT_MODULE, recentModuleList);
  }
};
</script>
