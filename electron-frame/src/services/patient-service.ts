import axios from "axios";

export class PatientService {
  constructor() {
    this.frameworkBackendUrl = "http://localhost:8080"
   }

  private patientList: any;
  private frameworkBackendUrl : string;

  async savePatientList(token: string, patientList: {}) {
    await axios.post(`${this.frameworkBackendUrl}/api/dashboard/recent-patients`, patientList,
      {
        headers: {
          "Authorization": 'Bearer ' + token
        }
      })
      .then(response => response.data);
  }

  async getPatientList(token: string) {
    this.patientList = await axios.get(`${this.frameworkBackendUrl}/api/dashboard/recent-patients`,
      {
        headers: {
          "Authorization": 'Bearer ' + token
        }
      })
      .then(response => response.data);

    return this.patientList;
  }

}