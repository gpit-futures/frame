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
        selectedTitle: "Login",
        clients: [],
        token: null,
        showDrawer: false,
        showNotifications: false,
        showDashboard: true,
        notifications: [],
        recentPatients: [],
        recentModules: [],
        gp: null

    },
    mutations: {
        [mutators.SET_USER](state, user) {
                state.user = user
        },
        [mutators.SET_PATIENT](state, patient) {
                state.patient = patient
        },
        [mutators.SET_PATIENT_CONTEXT](state, patientContext) {
                state.patientContext = patientContext
        },
        [mutators.SET_SELECTED_MODULE](state, selected) {
                state.selected = selected
        },
        [mutators.SET_SELECTED_MODULE_TITLE](state, selectedTitle) {
                state.selectedTitle = selectedTitle
        },
        [mutators.SET_TOKEN](state, token) {
                state.token = token
        },
        [mutators.SET_SHOW_DRAWER](state, showDrawer) {
                state.showDrawer = showDrawer
        },
        [mutators.SET_SHOW_NOTIFICATIONS](state, showNotifications) {
                state.showNotifications = showNotifications
        },
        [mutators.SET_SHOW_DASHBOARD](state, showDashboard) {
                state.showDashboard = showDashboard
        },
        [mutators.SET_CLIENTS](state, clients) {
            if (clients == null) {
                state.clients = []
            } else {
                state.clients = clients
            }
        },
        [mutators.LOGOUT](state) {
            state.user = null,
            state.patient = null,
            state.patientContext = false,
            state.selected = null,
            state.selectedTitle = "Login",
            state.clients = [],
            state.showDashboard = true
        },
        [mutators.SET_NOTIFICATIONS](state, notifications) {
                state.notifications = notifications
        },
        [mutators.ADD_NOTIFICATION](state, notification) {
                state.notifications.unshift(notification)
        },
        [mutators.REMOVE_NOTIFICATION](state, notification) {
                state.notifications.splice(state.notifications.indexOf(notification),1)
        },
        [mutators.SET_RECENT_PATIENT](state, patients) {
            state.recentPatients = patients
        },
        [mutators.ADD_RECENT_PATIENT](state, patient) {
            if (state.recentPatients && state.recentPatients.find( recentPatient => recentPatient.id === patient.id ) === undefined) {
                if (state.recentPatients.length == 6) {
                    state.recentPatients.pop();
                    state.recentPatients.unshift(patient);
                } else {
                    state.recentPatients.unshift(patient);
                }
            }
        },
        [mutators.SET_RECENT_MODULE](state, modules) {
            state.recentModules = modules
        },
        [mutators.ADD_RECENT_MODULE](state, module) {
            if (state.recentModules && state.recentModules.find( recentModule => recentModule.id === module.id ) === undefined) {
                if (state.recentModules.length == 6) {
                    state.recentModules.pop();
                    state.recentModules.unshift(module);
                } else {
                    state.recentModules.unshift(module);
                }
            }
        },
        [mutators.SET_GP](state, gp) {
            state.gp = gp
        },
    },
    getters: {
    },
    actions: {
    }
})
