import axios from "axios";


export class CatalogueService {
  constructor() { }

  private clientList: IClientMetadata[];
  private recentClientList: IClientMetadata[];

  async getClients(id: string): Promise<IClientMetadata[]> {
    this.clientList = await axios.get('http://ec2-18-130-26-44.eu-west-2.compute.amazonaws.com:8080/api/client-lists/' + id)
      .then(response => response.data);
    return this.clientList;
  }

  async updateClientList(token: string, id: string, orderedClientList: IClientMetadata[]) {
    await axios.post('http://ec2-18-130-26-44.eu-west-2.compute.amazonaws.com:8080/api/client-lists/' + id, orderedClientList, {
      headers: {
        "Authorization": 'Bearer ' + token
      }
    })
      .then(response => response.data);
  }

  async getRecentClients(token: string): Promise<IClientMetadata[]> {
    this.recentClientList = await axios.get('http://ec2-18-130-26-44.eu-west-2.compute.amazonaws.com:8080/api/dashboard/recent-modules', {
      headers: {
        "Authorization": 'Bearer ' + token
      }
    })
      .then(response => response.data);
    return this.recentClientList;
  }

  async updateRecentClientList(token: string, recentClientList: IClientMetadata[]) {
    await axios.post('http://ec2-18-130-26-44.eu-west-2.compute.amazonaws.com:8080/api/dashboard/recent-modules', recentClientList, {
      headers: {
        "Authorization": 'Bearer ' + token
      }
    })
      .then(response => response.data);
  }
}

export interface IClientMetadata {
  id: string;
  applicationName: string;
  publisher: string;
  sourceUrl: string;
  eventsOfInterest: string[];
}
