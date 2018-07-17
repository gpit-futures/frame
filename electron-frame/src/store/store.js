import Vue from 'vue'
import Vuex from 'vuex'
import mutators from './mutators'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        user: null,
        patient: null,
        patientContext: false,
        selected: null,
        selectedTitle: null,
        clients: [],
        token: null,
        showDrawer: false,
        showNotifications: false,
        showDashboard: true,
        notifications: []

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
        [mutators.SET_SHOW_DRAWER](state, showDrawer) {
            if (!null) {
                state.showDrawer = showDrawer
            }
        },
        [mutators.SET_SHOW_NOTIFICATIONS](state, showNotifications) {
            if (!null) {
                state.showNotifications = showNotifications
            }
        },
        [mutators.SET_SHOW_DASHBOARD](state, showDashboard) {
            if (!null) {
                state.showDashboard = showDashboard
            }
        },
        [mutators.SET_CLIENTS](state, clients) {

            let localCore = {  
                "id":"168be560-ab86-4020-a41e-a30e51dbbab2",
                "applicationName":"CoreLocal",
                "publisher":"Core GP Sys",
                "sourceUrl":"http://localhost:3101/#/",
                "eventsOfInterest":[  
                ]
             }
             let localInr = {  
                "id":"168be560-ab86-4020-a41e-a30e51dbbab7",
                "applicationName":"InrLocal",
                "publisher":"Core GP Sys",
                "sourceUrl":"http://localhost:3102/#/",
                "eventsOfInterest":[  
                    "patient-context:changed",
                    "patient-context:ended"
                ]
             }
             let localGPConnect = {  
                "id":"168be560-ab86-4054-a41e-a30e51dbbab9",
                "applicationName":"Appointments",
                "publisher":"Core GP Sys",
                "sourceUrl":"http://ec2-35-176-230-224.eu-west-2.compute.amazonaws.com/#/",
                "eventsOfInterest":[  
                    "patient-context:changed",
                    "patient-context:ended"
                ]
             }
             let localGPConnect2 = {  
                "id":"168ce3460-ab86-4054-b41e-a30e51dbbab1",
                "applicationName":"Appointments2",
                "publisher":"Core GP Sys",
                "sourceUrl":"http://ec2-34-246-89-210.eu-west-1.compute.amazonaws.com/#/",
                "eventsOfInterest":[  
                    "patient-context:changed",
                    "patient-context:ended"
                ]
             }

             let localGPConnect3 = {  
                "id":"168ce3460-ab86-7854-b41e-a67e51dbbab1",
                "applicationName":"Appointments3",
                "publisher":"Core GP Sys",
                "sourceUrl":"http://localhost:9000/#/",
                "eventsOfInterest":[  
                    "patient-context:changed",
                    "patient-context:ended"
                ]
             }
             

            if (clients == null) {
                state.clients = []
            } else {
                state.clients = clients
                // state.clients.push(localInr)
                // state.clients.push(localGPConnect2)
                // state.clients.push(localGPConnect3)
            }
        },
        [mutators.LOGOUT](state) {
            state.user = null,
            state.patient = null,
            state.patientContext = false,
            state.selected = null,
            state.selectedTitle = null,
            state.clients = [],
            state.showDashboard = true
        },
        [mutators.SET_NOTIFICATIONS](state, notifications) {
            if (!null) {
                state.notifications = notifications
            }
        },
    },
    getters: {
    },
    actions: {
    }
})
