// defines the methods that the clients should have access to when comunicating with the client.

// in preload scripts, we have access to node.js and electron APIs
// the remote web app will not, so this is safe
const { ipcRenderer: ipc, remote } = require('electron');
init();

function init() {
  attachIPCListeners();
  console.log('webview script preloaded');

  // Expose a bridging API to remote app's window.
  // We'll add methods to it here first, and when the remote web app loads,
  // it'll add some additional methods as well.
  //
  // !CAREFUL! do not expose any functionality or APIs that could compromise the
  // user's computer. E.g. don't directly expose core Electron (even IPC) or node.js modules.
  window.Bridge = {
    setPatientContext,
    sendWarningToFrame
  };
}

function attachIPCListeners() {
  ipc.on('patient-context:changed', (event, patient) => {
    console.log('patient context changed triggered' + patient)
    window.Bridge.updatePatientContext(patient);
  });
  ipc.on('patient-context:ended', (event, patient) => {
    console.log('patient context ended triggered' + patient)
    window.Bridge.endPatientContext(patient);
  });
  ipc.on('token-context:changed', (event, token) => {
    console.log('token context changed triggered' + token)
    window.Bridge.updateTokenContext(token);
  });
}

// called when the patient context changes in core
function setPatientContext(message) {
  console.log(message);
  if( message == null ) {
    ipc.sendToHost('patient-context:ended', message)
  } else {
    ipc.sendToHost('patient-context:changed', message)
  }
}

// send warning
function sendWarningToFrame(message) {
  ipc.sendToHost('warning-message:sent', message)
}

