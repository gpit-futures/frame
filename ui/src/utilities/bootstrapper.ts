import { IClientMetadata } from "../services/catalogue-service";

export interface IEvent {
  name: string;
  data: any;
}

export interface IClient {
  id: string;
  name: string;
  metadata: IClientMetadata;
  registered: boolean;
  port: MessagePort;
}

 export class ClientStateManager {
  get() {
      return this.clients;
  }

  set(clients: IClientMetadata[]) {
      this.clients =
          clients.map(x => {
              return {
                  id: x.id,
                  name: x.applicationName,
                  metadata: x,
                  registered: false,
                  port: undefined
              };
          });
  }

  register(origin: string, port: MessagePort) {
      const client = this.clients.find(x => x.metadata.sourceUrl === origin);
      client.registered = true;
      client.port = port;
      return client;
  }

  getInterestedClients(event: string) {
      return this.clients.filter(client => 
          client.metadata.eventsOfInterest.filter(x => x === event).length > 0
      );
  }

  private clients: IClient[] = [];
}

export declare type Dispatcher = (clientId: string, event: any, data: any, clientManager: ClientStateManager) => void;

export function createEventDispatcher(): Dispatcher {
  return (clientId: string, event: any, data: any, clientManager: ClientStateManager) => {
    clientManager
      .getInterestedClients(event)
      .map(client => {
        if (client.id === clientId || client.metadata.eventsOfInterest.indexOf(event) === -1) {
          return;
        }
        
        client.port.postMessage({
          name: event,
          version: "0.0.1",
          data: data
        });
      });
  };
}
