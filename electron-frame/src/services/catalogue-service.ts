import axios from "axios";


export class CatalogueService {
  constructor() { }

  private clientList: IClientList;

  async getClients(id: string): Promise<IClientMetadata[]> {
    if (!this.clientList) {
      const gist = await axios.get("https://api.github.com/gists/808ce2353c0ca20e85968d33d9c76944?client_id=0b5c1b8619519671add3&client_secret=8d460cba9c0febc51eabf627d9936fbb654892a1")
      .then(response => response.data);
      
      const rawUrl = gist.files["client-list.json"].raw_url;

      this.clientList = await axios.get(rawUrl + '?client_id=0b5c1b8619519671add3&client_secret=8d460cba9c0febc51eabf627d9936fbb654892a1')
      .then(response => response.data);
    }
    return this.clientList[id];
  }
}

interface IClientList {
  [key: string]: IClientMetadata[];
}

export interface IClientMetadata {
  id: string;
  applicationName: string;
  publisher: string;
  sourceUrl: string;
  eventsOfInterest: string[];
}
