<template>
<v-content>
      <v-container fluid>
        <v-layout flex justify-center wrap row>
          <v-flex xs2></v-flex>
          <v-flex xs8 class="headline widget-title"><v-icon>fas fa-history</v-icon> Recent Patients:</v-flex>
          <v-flex xs2></v-flex>
          <v-flex xs2></v-flex>
          <v-flex xs8>
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
          </v-flex>
          <v-flex xs2></v-flex>
          <v-flex xs2></v-flex>
          <v-flex xs8 class="headline widget-title"><v-icon>fas fa-retweet</v-icon> Most Visited:</v-flex>
          <v-flex xs2></v-flex>
          <v-flex xs2></v-flex>
          <v-flex xs8>
            <v-layout flex justify-center wrap row>
            <v-flex xs4 v-for="module of recentModules" :key="module.id">
              <v-card color="white" class="black--text" width="300" style="margin: 5px auto;" hover>
              <v-layout>
                <v-flex xs3 class="text-md-center red-module" v-if="module.applicationName == 'Core'">
                  <v-icon x-large style="margin: 13px auto;">fas fa-globe</v-icon>
                </v-flex>
                <v-flex xs3 class="text-md-center yellow-module" v-if="module.applicationName == 'INR'">
                  <v-icon x-large style="margin: 13px auto;">fas fa-syringe</v-icon>
                </v-flex>
                <v-flex xs3 class="text-md-center green-module" v-if="module.applicationName == 'Appointments'">
                  <v-icon x-large style="margin: 13px auto;">fas fa-calendar-alt</v-icon>
                </v-flex>
                <v-flex xs3 class="text-md-center purple-module" v-if="module.applicationName == 'Document Management'">
                  <v-icon x-large style="margin: 13px auto;">fas fa-folder-open</v-icon>
                </v-flex>
                <v-flex xs3 class="text-md-center orange-module" v-if="module.applicationName == 'Prescribing'">
                  <v-icon x-large style="margin: 13px auto;">fas fa-prescription-bottle-alt</v-icon>
                </v-flex>
                <v-flex xs3 class="text-md-center blue-module" v-if="module.applicationName == 'Workflow'">
                  <v-icon x-large style="margin: 13px auto;">fas fa-list-alt</v-icon>
                </v-flex>
                <v-flex xs3 class="text-md-center light-blue-module" v-if="module.applicationName == 'Telecare'">
                  <v-icon x-large style="margin: 13px auto;">fas fa-comments</v-icon>
                </v-flex>
                <v-flex xs3 class="text-md-center lighter-blue-module" v-if="module.applicationName == 'Caseload Management'">
                  <v-icon x-large style="margin: 13px auto;">fas fa-box-briefcase-medical</v-icon>
                </v-flex>
                <v-flex xs3 class="text-md-center pale-blue-module" v-if="module.applicationName == 'Stock Management'">
                  <v-icon x-large style="margin: 13px auto;">fas fa-box-open</v-icon>
                </v-flex>
                <v-flex xs9>
                  <v-card-title primary-title @click="selectModule(module)">
                    <div class="text-overflow">
                      <div class="headline text-overflow">{{module.applicationName}}</div>
                    </div>
                  </v-card-title>
                </v-flex>
              </v-layout>
              </v-card>
            </v-flex>
            </v-layout>
          </v-flex>
          <v-flex xs2></v-flex>
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
      headers: [
        {
          text: "Recent Patients",
          align: "left",
          sortable: true,
          value: "name"
        },
        { text: "DOB", value: "birthDate", sortable: true },
        { text: "Gender", value: "gender", sortable: true },
        { text: "NHS Number", value: "identifier", sortable: true },
        { text: "Actions", value: "actions", sortable: true }
      ],
      dialog: false,
      selectedPatient: null,
      patientService: new PatientService(),
      catalogueService: new CatalogueService()
    };
  },
  methods: {
    selectPatient(patient) {
      this.dialog = true;
      this.selectedPatient = patient;
    },
    triggerPatientContext() {
      triggerPatientContextEvent(
        "patient-context:changed",
        this.selectedPatient
      );
    },
    selectModule(client) {
      this.$store.commit(mutators.SET_SHOW_DASHBOARD, false);
      this.$store.commit(mutators.SET_SELECTED_MODULE, client.id);
      this.$store.commit(mutators.SET_SELECTED_MODULE_TITLE, client.applicationName);
      this.$store.commit(mutators.ADD_RECENT_MODULE, client);
      this.catalogueService.updateRecentClientList(this.token, this.$store.state.recentModules);
    }
  },
  computed: {
    recentPatients() {
      return this.$store.state.recentPatients;
    },
    recentModules() {
      return this.$store.state.recentModules;
    },
    token() {
      return this.$store.state.token.access_token;
    },
    patient() {
      if (this.selectedPatient) {
        return 
          `${this.selectedPatient.name[0].family}, ${this.selectedPatient.name[0].given[0]} (${this.selectedPatient.name[0].prefix[0]})`
        ;
      } else {
        return "No Patient Selected";
      }
    }
  },
  async mounted() {
    let patientList = await this.patientService.getPatientList(this.token);
    this.$store.commit(mutators.SET_RECENT_PATIENT, patientList);
    
    let recentModuleList = await this.catalogueService.getRecentClients(this.token);
    this.$store.commit(mutators.SET_RECENT_MODULE, recentModuleList);
  }
};
</script>
