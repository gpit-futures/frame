import axios from "axios";

export class PatientService {
  constructor() { }

  private patientList: any;

  async savePatientList(token: string, patientList: {}) {
    await axios.post('http://ec2-18-130-26-44.eu-west-2.compute.amazonaws.com:8080/api/dashboard/recent-patients', patientList,
      {
        headers: {
          "Authorization": 'Bearer ' + token
        }
      })
      .then(response => response.data);
  }

  async getPatientList(token: string) {
    this.patientList = await axios.get('http://ec2-18-130-26-44.eu-west-2.compute.amazonaws.com:8080/api/dashboard/recent-patients',
      {
        headers: {
          "Authorization": 'Bearer ' + token
        }
      })
      .then(response => response.data);

    return this.patientList;
  }

}