import Vue from 'vue'
import Vuex from 'vuex'
import mutators from './mutators'
import router from '../router'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        user: null,
        patient: null,
        patientContext: false,
        selected: null,
        selectedTitle: null,
        clients: null,
        token: null

    },
    mutations: {
        [mutators.SET_USER](state, user) {
            if (!null) {
                state.user = user
            }
        },
        [mutators.SET_PATIENT](state, patient) {
            if (!null) {
                state.patient = patient
            }
        },
        [mutators.SET_PATIENT_CONTEXT](state, patientContext) {
            if (!null) {
                state.patientContext = patientContext
            }
        },
        [mutators.SET_SELECTED_MODULE](state, selected) {
            if (!null) {
                state.selected = selected
            }
        },
        [mutators.SET_SELECTED_MODULE_TITLE](state, selectedTitle) {
            if (!null) {
                state.selectedTitle = selectedTitle
            }
        },
        [mutators.SET_TOKEN](state, token) {
            if (!null) {
                state.token = token
            }
        },
        [mutators.SET_CLIENTS](state, clients) {

            // let localCore = {  
            //     "id":"168be560-ab86-4020-a41e-a30e51dbbab2",
            //     "applicationName":"CoreLocal",
            //     "publisher":"Core GP Sys",
            //     "sourceUrl":"http://localhost:3101/#/",
            //     "eventsOfInterest":[  
            //     ]
            //  }
            //  let localInr = {  
            //     "id":"168be560-ab86-4020-a41e-a30e51dbbab7",
            //     "applicationName":"InrLocal",
            //     "publisher":"Core GP Sys",
            //     "sourceUrl":"http://localhost:3102/#/",
            //     "eventsOfInterest":[  
            //         "patient-context:changed",
            //         "patient-context:ended"
            //     ]
            //  }
             

            if (clients == null) {
                state.clients = clients
            } else {
                state.clients = clients
                // state.clients.push(localCore)
                // state.clients.push(localInr)
            }
        },
        [mutators.LOGOUT](state) {
            state.user = null,
            state.patient = null,
            state.patientContext = false,
            state.selected = null,
            state.selectedTitle = null,
            state.clients = null
        },
    },
    getters: {
    },
    actions: {
    }
})
