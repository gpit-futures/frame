import { bindable, bindingMode } from "aurelia-framework";

import { Catalogue } from "../../../fake-app-catalogue";

export class SidebarCustomElement {
  @bindable({ defaultBindingMode: bindingMode.twoWay }) selected: string;
  @bindable clients: Catalogue.IClientMetadata[];

  select(clientId: string) {
    this.selected = clientId;
  }
}
