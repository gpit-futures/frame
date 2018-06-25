// define a class to deal with managing client comunication.
//   - import vuex (to allow it to manipulate the state - need "clients" to work out which events they are subscribed to)
//   - work out which clients subscribe to which events and fire the needed event
import Vue from 'vue';
import Vuex from 'vuex'
import mutators from '../store/mutators';
import store from '../store/store';
import { init } from 'electron-compile';
import { Notifier, NotifierType } from "../utilities/notifier";

let modules;
let clients;
let testVar;
let context;
let patientContextSub;
let patientEndedSub;
let pub;
let notifier;

start();
// setup connection to rabbitMQ server - and listen for 'patient-context:changed' events
function start() {
    notifier = new Notifier()

    context = require('rabbit.js').createContext('amqp://localhost:5672');
    patientContextSub = context.socket('SUBSCRIBE', { routing: 'direct', persistent: true });
    patientContextSub.connect('patient.exchange', 'patient.context.changed');
    patientContextSub.setEncoding('utf8');
    patientContextSub.on('data', function (data) {
        triggerPatientContextEvent('patient-context:changed', JSON.parse(data))
        console.log("Data: ", data);
    })

    patientEndedSub = context.socket('SUBSCRIBE', { routing: 'direct', persistent: true });
    patientEndedSub.connect('patient.exchange', 'patient.context.ended');
    patientEndedSub.setEncoding('utf8');
    patientEndedSub.on('data', function (data) {
        triggerPatientContextEvent('patient-context:ended', null)
        console.log("Data: ", data);
    })
}

// adds an event listener to each webview and decodes/routes the event when triggered
export function setupListeners(webviews) {
    modules = webviews
    let module;
    for (module in modules) {
        console.log(modules[module])
        modules[module][0].addEventListener("ipc-message", event => {
            if (event.channel == 'warning-message:sent') {
                notifier.show(
                    "Danger",
                    event.args[0],
                    NotifierType.Danger,
                    8000
                  );
            } else (
                triggerPatientContextEvent(event.channel, event.args[0])
            )
        });
        modules[module][0].addEventListener('did-stop-loading', triggerTokenContextEvent)
    }
    // modules.Core[0].openDevTools();
    // modules.INR[0].openDevTools();
    // modules.InrLocal[0].openDevTools();
    // modules.CoreLocal[0].openDevTools();
}

// sends patient context change event to subscribed clients
function triggerPatientContextEvent(eventChannel, patient) {
    console.log(patient);
    store.commit(mutators.SET_PATIENT, patient)
    store.commit(mutators.SET_PATIENT_CONTEXT, patient == null ? false:true)

    sendToThickClient(eventChannel, patient)

    let interestedClients = getInterestedClients(eventChannel)

    let interestedClient
    for (interestedClient in interestedClients) {
        modules[interestedClients[interestedClient].applicationName][0].send(eventChannel, patient);
    }
}

// get a list of clients that are intrested in the event
function getInterestedClients(event) {
    return store.state.clients.filter(client =>
        client.eventsOfInterest.filter(x => x === event).length > 0
    );
}

// send token to frames on login.
function triggerTokenContextEvent() {
    let module;
    for (module in modules) {
        console.log(store.state.token)
        modules[module][0].send('token-context:changed', store.state.token);
    }
}

// send to thick client
function sendToThickClient(eventChannel, patient) {
    pub = context.socket('PUSH', { routing: 'direct', persistent: true });

    pub.connect(eventChannel.endsWith('ended') ? 'patient-context-ended-queue' : 'patient-context-changed-queue', function () {
        console.log("writeing to queue")
        pub.write(JSON.stringify(patient), 'utf8');
    })
}