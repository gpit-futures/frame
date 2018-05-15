export namespace Catalogue {

  export interface IClientMetadata {
    id: string;
    applicationName: string;
    publisher: string;
    sourceUrl: string;
    eventsOfInterest: string[];
  }

  const clientList: IClientList = {
    "366f6345-3407-4f6c-9c77-108a37543064" : [
      {
        id: "168be560-ab86-4020-a41e-a30e51dbbab8",
        applicationName: "Core",
        publisher: "CoreGpSys",
        sourceUrl: "http://gov.uk",
        eventsOfInterest: []
      }
      // {
      //   id: "168be560-ab86-4020-a41e-a30e51dbbab8",
      //   applicationName: "Core",
      //   publisher: "CoreGpSys",
      //   sourceUrl: "http://localhost:3101",
      //   eventsOfInterest: []
      // },
      // {
      //   id: "34480cf8-7ada-4f81-9fd5-643b6fe269d0",
      //   applicationName: "INR",
      //   publisher: "GpSystems",
      //   sourceUrl: "http://localhost:3102",
      //   eventsOfInterest: [
      //     "patient-context:changed",
      //     "patient-context:ended"
      //   ]
      // }
    ],
    "d3ea56ce-87d2-4a9a-afb9-e8d820299bf1": []
  };

  export async function getClients(id: string): Promise<IClientMetadata[]> {
    return clientList[id];
  }

  interface IClientList {
    [key: string]: IClientMetadata[];
  }
}
