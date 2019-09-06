import axios from "axios";

export class SearchService {
    constructor() { }

    private searchResults: IPatientResult;
    private patient: any;
    // Changes for single machine: Patient search hits the Core module, so redirect to that backend port.  Note on AWS instances, Core ran on same server as Frame but on port 80
    async getSearchResults(token: string, searchString: string): Promise<IPatientResult> {
        this.searchResults = await axios.post('http://localhost:3101/api/search/patients', { "searchString": searchString }, {
            headers: {
                "Authorization": 'Bearer ' + token
            }
        })
            .then(response => response.data);

        return this.searchResults;
    }

    async getPatient(token: string, nhsNumber: string) {
        this.patient = await axios.get('http://localhost:3101/api/search/patients/' + nhsNumber, {
            headers: {
                "Authorization": 'Bearer ' + token
            }
        })
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