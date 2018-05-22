import { autoinject } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";

@autoinject
export class CatalogueService {
  constructor(private http: HttpClient) { }

  private clientList: IClientList;

  async getClients(id: string): Promise<IClientMetadata[]> {
    if (!this.clientList) {
      const gist = await this.http
        .fetch("https://api.github.com/gists/808ce2353c0ca20e85968d33d9c76944")
        .then(response => response.json());
      
      const rawUrl = gist.files["client-list.json"].raw_url;

      this.clientList = await this.http
        .fetch(rawUrl)
        .then(response => response.json());
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
