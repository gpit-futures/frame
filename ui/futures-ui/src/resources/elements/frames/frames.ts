import { bindable, observable } from "aurelia-framework";

import { Catalogue } from "../../../fake-app-catalogue";

export class FramesCustomElement {
  
  @bindable selected: string;
  @bindable clients: Catalogue.IClientMetadata[];

  clientsChanged() {
    if (!this.clients || this.clients.length === 0) {
      return;
    }

    this.selected = this.clients[0].id;
  }
}
