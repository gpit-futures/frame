import { autoinject, computedFrom, observable } from "aurelia-framework";

import { AppRouter } from "../../routers/app-router";
import { ClientStateManager, Dispatcher, IEvent, createEventDispatcher } from "../../utilities/bootstrapper";
import { Notifier, NotifierType } from "../../utilities/notifier";

import { CatalogueService, IClientMetadata } from "../../services/catalogue-service";
import { UserService, IUser } from "../../services/user-service";

@autoinject
export class HomeLayout {
  constructor(
    private router: AppRouter, 
    private catalogue: CatalogueService,
    private users: UserService,
    private notifier: Notifier, 
    private clientManager: ClientStateManager) { }

  private userId: string;
  private user: IUser;
  
  selected: string;

  @observable clients: IClientMetadata[] = [];
  @observable patientContext: boolean;
  @observable patient;

  @computedFrom("user")
  get practitionerName(): string {
    if (!this.user) {
      return "";
    }

    return `${this.user.title} ${this.user.firstName} ${this.user.lastName}`;
  }

  @computedFrom("user")
  get logout(): Function {
    return () => {
      this.clients = [];
      this.user = undefined;
      this.router.navigateToLogin();
    };
  }

  activate(params) {
    this.userId = params.user;
  }

  async attached() {
    const dispatcher = createEventDispatcher();
    this.createEventListener(dispatcher);
    
    this.clients = await this.catalogue.getClients(this.userId);
    this.user = await this.users.getUser(this.userId);
    
    if (this.clients.length > 0) {
      this.clientManager.set(this.clients);
      this.selected = this.clients[0].id;
    }
  }

  private createEventListener(dispatcher: Dispatcher) {
    window.addEventListener("message", (message: MessageEvent) => {
      const event = message.data as IEvent;

      if (event.name === "client-connector:client:loaded") {
        const client = this.clientManager.register(event.data.origin, message.ports[0]);
        const clientId = client.id;
        
        message.ports[0].onmessage = (e: MessageEvent) => {
          dispatcher(clientId, e.data.name, e.data.data, this.clientManager);
          this.processFrameworkEvent(e.data.name, e.data.data);
        };

        return;
      }
    });
  }

  private processFrameworkEvent(eventName: string, data: any) {
    switch(eventName) {
      case "patient-context:changed":
        this.patient = data;
        this.patientContext = true;
        break;
      case "patient-context:ended":
        this.patientContext = false;
        this.patient = null;
        break;
    }
  }
}
