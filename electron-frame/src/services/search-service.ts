import axios from "axios";

export class SearchService {
    constructor() { }

    private searchResults: IPatientResult;
    private patient: any;

    async getSearchResults(token:string,searchString: string): Promise<IPatientResult> {
        this.searchResults = await axios.post('http://ec2-18-130-26-44.eu-west-2.compute.amazonaws.com/api/search/patients', { "searchString": searchString },{headers: {
            "Authorization" : 'Bearer ' + token
          }})
      .then(response => response.data);

      return this.searchResults;
    }

    async getPatient(token:string,nhsNumber: string) {
        this.patient = await axios.get('http://ec2-18-130-26-44.eu-west-2.compute.amazonaws.com/api/search/patients/' + nhsNumber,{headers: {
            "Authorization" : 'Bearer ' + token
          }})
      .then(response => response.data);

      return this.patient;
    }
}

export interface IPatientResults {
    totalPatients: string;
    patients: IPatientResult[];
}

export interface IPatientResult {
    id: string;
    address: string;
    dateOfBirth: string;
    department: string;
    gender: string;
    gpAddress: string;
    gpName: string;
    name: string;
    nhsNumber: string;
    pasNo: string;
    phone: string;
}